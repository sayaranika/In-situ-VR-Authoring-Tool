using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Input;

public class CaptureRightHandData : MonoBehaviour
{
    [SerializeField]
    private GameObject menuOptions;
    [SerializeField]
    private GameObject statusLog;
    [SerializeField]
    private TextMeshPro title;
    [SerializeField]
    private TextMeshPro trackingStatus;
    [SerializeField]
    private TextMeshPro trackingConfidence;
    [SerializeField]
    private TextMeshPro countdown;
    [SerializeField] string filename;
    [SerializeField]
    private GameObject saveDialog;


    [SerializeField]
    private GameObject index1, index2, index3;
    [SerializeField]
    private GameObject thumb0, thumb1, thumb2, thumb3;
    [SerializeField]
    private GameObject middle1, middle2, middle3;
    [SerializeField]
    private GameObject ring1, ring2, ring3;
    [SerializeField]
    private GameObject pinky0, pinky1, pinky2, pinky3;
    [SerializeField]
    private GameObject wrist, forearmStub;


    private GameObject rightHandAnchor;
    private Transform rightHand;

    private OVRHand ovrHandRight;
    private OVRSkeleton ovrSkeletonRight;
    private List<OVRBone> fingerBonesRight;

    Quaternion wristRotation;
    List<HandPose> handPoseEntries = new List<HandPose>();

    private int countdownTime = 3;
    private int i = 0;
    private int isSession = 0;

    private void Start()
    {
        gameObject.SetActive(false);
        statusLog.SetActive(false);
        saveDialog.SetActive(false);
    }

    private void OnEnable()
    {
        if(i!=0)
        {
            initializeRightHand();
            Debug.Log(i.ToString());
            PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOff);
            menuOptions.SetActive(false);
            statusLog.SetActive(true);
            title.text = "Capturing Right Hand Data On The Count of Three";
            countdown.text = countdownTime.ToString();
            StartCoroutine(CountdownToCapture());
        }
        i++;
    }

    private void Update()
    {
        if(isSession == 1)
        {
            if (ovrHandRight.IsTracked == true)
                trackingStatus.text = "Tracking Status: Ok";
            else trackingStatus.text = "Tracking Status: Lost tracking";

            if (ovrHandRight.HandConfidence == OVRHand.TrackingConfidence.High)
                trackingConfidence.text = "Data Confidence: High";
            else trackingConfidence.text = "Data Confidence: Low";
        }
    }

    void initializeRightHand()
    {
        rightHandAnchor = GameObject.Find("RightHandAnchor");
        if (rightHandAnchor != null)
        {
            rightHand = rightHandAnchor.transform.Find("OVRHandPrefab_Right");
            if (rightHand != null)
            {
                ovrHandRight = rightHand.GetComponent<OVRHand>();
                ovrSkeletonRight = rightHand.GetComponent<OVRSkeleton>();
                isSession = 1;
            }
        }
        else
            Debug.Log("Not Found Right Hand");
    }

    IEnumerator CountdownToCapture()
    {
        
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownTime--;
            countdown.text = countdownTime.ToString();
        }

        yield return new WaitForSeconds(1f);

        if (ovrSkeletonRight.IsInitialized && ovrHandRight.IsTracked)
        {
            string name = "temp";
            fingerBonesRight = new List<OVRBone>(ovrSkeletonRight.Bones);
            List<Vector3> data = new List<Vector3>();
            foreach (var bone in fingerBonesRight)
            {
                data.Add(ovrSkeletonRight.transform.InverseTransformPoint(bone.Transform.position));
            }
            Quaternion wristRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.rotation;
            HandPose entry = new HandPose(name, data, wristRotation, false, true);

            saveDialog.SetActive(true);
            statusLog.SetActive(false);
            PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOn);
            mapDataToHandModel();

            //handPoseEntries.Add(entry);
            //nameInput.text = "";

            //FileHandler.SaveToJSON<HandPose>(handPoseEntries, filename);
            Debug.Log("Saved R");

        }

        countdownTime = 3;

    }

    public void changeRotation()
    {
        Debug.Log("Called");
    }


    void mapDataToHandModel()
    {
        wrist.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.localPosition + new Vector3(0.01f,-0.1f,0);
        wrist.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.localRotation;

        index1.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index1].Transform.localPosition;
        index2.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index2].Transform.localPosition;
        index3.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index3].Transform.localPosition;
        index1.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index1].Transform.localRotation;
        index2.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index2].Transform.localRotation;
        index3.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index3].Transform.localRotation;

        thumb0.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb0].Transform.localPosition;
        thumb1.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb1].Transform.localPosition;
        thumb2.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb2].Transform.localPosition;
        thumb3.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb3].Transform.localPosition;
        thumb0.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb0].Transform.localRotation;
        thumb1.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb1].Transform.localRotation;
        thumb2.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb2].Transform.localRotation;
        thumb3.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb3].Transform.localRotation;

        middle1.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Middle1].Transform.localPosition;
        middle2.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Middle2].Transform.localPosition;
        middle3.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Middle3].Transform.localPosition;
        middle1.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Middle1].Transform.localRotation;
        middle2.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Middle2].Transform.localRotation;
        middle3.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Middle3].Transform.localRotation;

        ring1.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Ring1].Transform.localPosition;
        ring2.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Ring2].Transform.localPosition;
        ring3.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Ring3].Transform.localPosition;
        ring1.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Ring1].Transform.localRotation;
        ring2.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Ring2].Transform.localRotation;
        ring3.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Ring3].Transform.localRotation;

        pinky0.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky0].Transform.localPosition;
        pinky1.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky1].Transform.localPosition;
        pinky2.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky2].Transform.localPosition;
        pinky3.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky3].Transform.localPosition;
        pinky0.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky0].Transform.localRotation;
        pinky1.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky1].Transform.localRotation;
        pinky2.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky2].Transform.localRotation;
        pinky3.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky3].Transform.localRotation;

        
        forearmStub.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_ForearmStub].Transform.localPosition;
        
        wrist.transform.rotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.rotation;
        forearmStub.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_ForearmStub].Transform.localRotation;
    }
}
