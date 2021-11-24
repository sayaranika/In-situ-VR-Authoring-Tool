using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaptureHandData : MonoBehaviour
{
    private GameObject rightHandAnchor;
    private Transform rightHand;
    private OVRHand ovrHandRight;
    private OVRSkeleton ovrSkeletonRight;
    private List<OVRBone> fingerBonesRight;

    private GameObject leftHandAnchor;
    private Transform leftHand;
    private OVRHand ovrHandLeft;
    private OVRSkeleton ovrSkeletonLeft;
    private List<OVRBone> fingerBonesLeft;

    private bool isRightHandSessionInProgress = false;
    private bool isLeftHandSessionInProgress = false;
    private bool isCoroutineRunning = false;
    private int countdownTime = 3;
    private int totalHandPoseDataRecorded = 0;
    private List<HandPoseSensor> handPoseEntries;

    [SerializeField] private Text trackingText;
    [SerializeField] private Text warningText;
    [SerializeField] private Text countdownText;


    private void Awake()
    {
        FileHandler.SaveToJSON<HandPoseSensor>(new List<HandPoseSensor>(), "HandPoseLibrary.json");
    }
    void Start()
    {
        FileHandler.SaveToJSON<HandPoseSensor>(new List<HandPoseSensor>(), "HandPoseLibrary.json");
    }

    public void captureData(bool isRightHandSession)
    {
        trackingText.text = "Initializing";
        warningText.text = "";
        countdownText.text = "";
        
        PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOff);
        if (isRightHandSession)
        {
            while (isRightHandInitialized())
            {
                Debug.Log("waiting for initialization");
            }
            isRightHandSessionInProgress = true;
            isLeftHandSessionInProgress = false;
        }
        else
        {
            while (isLeftHandInitialized())
            {
                trackingText.GetComponent<TextMeshPro>().text = "Initializing";
                warningText.GetComponent<TextMeshPro>().text = "";
                countdownText.GetComponent<TextMeshPro>().text = "";
            }
            isRightHandSessionInProgress = false;
            isLeftHandSessionInProgress = true;
        }
    }

    private void Update()
    {
        if (isRightHandSessionInProgress)
        {
            Debug.Log("Inside Update");
            if (ovrHandRight.IsTracked == true)
            {
                trackingText.text = "Tracking Status: Ok";
                if(isCoroutineRunning == false)
                {
                    isCoroutineRunning = true;
                    StartCoroutine(CountdownToCaptureRight());
                    
                }
            }
            else
            {
                trackingText.text = "";
                warningText.text = "Right hand is not being tracked";
                if(isCoroutineRunning == true)
                {
                    StopCoroutine(CountdownToCaptureRight());
                    
                    countdownText.text = "";
                    isCoroutineRunning = false;
                    countdownTime = 3;
                }
            }
        }

        if (isLeftHandSessionInProgress)
        {
            if (ovrHandLeft.IsTracked == true)
            {
                trackingText.GetComponent<TextMeshPro>().text = "Tracking Status: Ok";
            }
            else
            {
                trackingText.GetComponent<TextMeshPro>().text = "";
                warningText.GetComponent<TextMeshPro>().text = "Left hand is not being tracked";
            }
        }
    }


    IEnumerator CountdownToCaptureRight()
    {
        Debug.Log("Inside coroutine");
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownTime--;
            countdownText.text = countdownTime.ToString();

        }

        yield return new WaitForSeconds(1f);

        if (ovrSkeletonRight.IsInitialized && ovrHandRight.IsTracked)
        {
            handPoseEntries = FileHandler.ReadListFromJSON<HandPoseSensor>("HandPoseLibrary.json");
            fingerBonesRight = new List<OVRBone>(ovrSkeletonRight.Bones);
            List<Vector3> data = new List<Vector3>();
            foreach (var bone in fingerBonesRight)
            {
                data.Add(ovrSkeletonRight.transform.InverseTransformPoint(bone.Transform.position));
            }
            Quaternion wristRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.rotation;
            totalHandPoseDataRecorded++;
            HandPoseSensor entry = new HandPoseSensor(totalHandPoseDataRecorded, data, wristRotation, false, true);

            PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOn);

            handPoseEntries.Add(entry);
            

            FileHandler.SaveToJSON<HandPoseSensor>(handPoseEntries, "HandPoseLibrary.json");
        }

        countdownTime = 3;
        isCoroutineRunning = false;
        isRightHandSessionInProgress = false;
        isLeftHandSessionInProgress = false;
    
}

    bool isRightHandInitialized()
    {
        rightHandAnchor = GameObject.Find("RightHandAnchor");
        if (rightHandAnchor != null)
        {
            rightHand = rightHandAnchor.transform.Find("OVRHandPrefab_Right");
            if (rightHand != null)
            {
                ovrHandRight = rightHand.GetComponent<OVRHand>();
                ovrSkeletonRight = rightHand.GetComponent<OVRSkeleton>();
            }
        }

        if (ovrHandRight != null && ovrSkeletonRight != null)
            return true;
        else return false;
    }

    bool isLeftHandInitialized()
    {
        leftHandAnchor = GameObject.Find("LeftHandAnchor");
        if (leftHandAnchor != null)
        {
            leftHand = leftHandAnchor.transform.Find("OVRHandPrefab_Left");
            if (leftHand != null)
            {
                ovrHandLeft = leftHand.GetComponent<OVRHand>();
                ovrSkeletonLeft = leftHand.GetComponent<OVRSkeleton>();
            }
        }

        if (ovrHandLeft != null && ovrSkeletonLeft != null)
            return true;
        else return false;
    }
}
