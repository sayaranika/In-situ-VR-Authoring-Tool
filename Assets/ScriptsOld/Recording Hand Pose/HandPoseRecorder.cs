using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public struct HandPoser
{
    public string name; //name of the pose
    public List<Vector3> fingerData;
    public UnityEvent onRecognized;
}

public class HandPoseRecorder : MonoBehaviour
{
    //hand objects
    [SerializeField]
    private GameObject leftHand;

    [SerializeField]
    private GameObject rightHand;

    private OVRHand ovrHandLeft;
    private OVRSkeleton ovrSkeletonLeft;
    private List<OVRBone> fingerBonesLeft;


    private OVRHand ovrHandRight;
    private OVRSkeleton ovrSkeletonRight;
    private List<OVRBone> fingerBonesRight;

    public List<HandPoser> handPosesRight;


    private HandPoser previousHandPose;
    //threshold for pinching to start record
    public float threshold = 0.6f;



    //countdown and debug texts
    [SerializeField]
    private Text systemStatusText;

    [SerializeField]
    private Text leftHandStatusText;

    [SerializeField]
    private Text rightHandStatusText;

    private int countdownTime = 3;
    private bool isCoroutineNotRunning = true;
    int count = 0;

    private void Awake()
    {

        ovrHandLeft = leftHand.GetComponent<OVRHand>();
        ovrSkeletonLeft = leftHand.GetComponent<OVRSkeleton>();
        ovrHandRight = rightHand.GetComponent<OVRHand>();
        ovrSkeletonRight = rightHand.GetComponent<OVRSkeleton>();

        handPosesRight = new List<HandPoser>();


        systemStatusText.gameObject.SetActive(true);
        rightHandStatusText.gameObject.SetActive(true);
        leftHandStatusText.gameObject.SetActive(true);
        systemStatusText.text = "Pinch with both hands to start recording";
        rightHandStatusText.text = "Status(Left): NA";
        leftHandStatusText.text = "Status(Right): NA";

    }
    void Start()
    {
        //fingerBonesRight = new List<OVRBone>(ovrSkeletonRight.Bones);
        previousHandPose = new HandPoser();
    }

    void Update()
    {
        if (checkPinchState() && isCoroutineNotRunning) StartCoroutine(CountdownToCapture());
        HandPoser currentHandPose = Recognize();
        bool hasRecognized = !currentHandPose.Equals(new HandPoser());

        if (hasRecognized && !currentHandPose.Equals(previousHandPose))
        {
            Debug.Log("New Gesture found" + currentHandPose.name);
            systemStatusText.text = "Found " + currentHandPose.name;
            previousHandPose = currentHandPose;
            currentHandPose.onRecognized.Invoke();
        }

    }

    bool checkPinchState()
    {
        bool isIndexFingerPinchingLeft = ovrHandLeft.GetFingerIsPinching(OVRHand.HandFinger.Index);
        float indexFingerPinchStrengthLeft = ovrHandLeft.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        bool isIndexFingerPinchingRight = ovrHandRight.GetFingerIsPinching(OVRHand.HandFinger.Index);
        float indexFingerPinchStrengthRight = ovrHandRight.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        if (isIndexFingerPinchingLeft && isIndexFingerPinchingRight)
        {
            return true;
        }
        return false;
    }

    IEnumerator CountdownToCapture()
    {
        isCoroutineNotRunning = false;

        while (countdownTime > 0)
        {

            systemStatusText.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);
            countdownTime--;
        }


        systemStatusText.text = "Recording Data";

        yield return new WaitForSeconds(1f);

        if (ovrSkeletonLeft.IsInitialized) fingerBonesLeft = new List<OVRBone>(ovrSkeletonLeft.Bones);

        if (ovrSkeletonRight.IsInitialized)
        {
            fingerBonesRight = new List<OVRBone>(ovrSkeletonRight.Bones);
            Debug.Log("Number of bones: " + fingerBonesRight.Count);
            Save();
        }



        isCoroutineNotRunning = true;
        countdownTime = 3;
        systemStatusText.text = "Pinch with both hands to start recording";
        rightHandStatusText.text = "Status(Left): Success";
        leftHandStatusText.text = "Status(Right): Success";
    }


    void Save()
    {
        HandPoser handPoseRight = new HandPoser();
        handPoseRight.name = "New Gesture" + count.ToString();
        List<Vector3> data = new List<Vector3>();
        Debug.Log("Saving following data:");
        foreach (var bone in fingerBonesRight)
        {
            data.Add(ovrSkeletonRight.transform.InverseTransformPoint(bone.Transform.position));

        }



        handPoseRight.fingerData = data;
        handPosesRight.Add(handPoseRight);

        count++;

        /*string JsonString = JsonUtility.ToJson(handPosesRight);
        string path = Application.dataPath + "/recordedHandData.json";
        StreamWriter sw = new StreamWriter(path);
        sw.Write(JsonString);
        sw.Close();
        Debug.Log("Saved");*./
        //File.WriteAllText(path, JsonString);

        //printinh
        /* if (handPosesRight.Count > 0)
         {
             Debug.Log("Size of recorded hand pose is " + handPosesRight.Count);
             Debug.Log("the data recoded is");
             foreach (var bone in handPosesRight)
             {
                 Debug.Log(bone.name);
                 foreach (var a in bone.fingerData)
                 {
                     Debug.Log(a.x + "," + a.y + "," + a.z);
                 }
                 Debug.Log("End of a record");
             }
         }*/
    }




    HandPoser Recognize()
    {
        HandPoser currentHandPose = new HandPoser();


        float currentMin = Mathf.Infinity;
        foreach (var hp in handPosesRight)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for (int i = 0; i < fingerBonesRight.Count; i++)
            {
                Vector3 currentData = ovrSkeletonRight.transform.InverseTransformPoint(fingerBonesRight[i].Transform.position);
                float distance = Vector3.Distance(currentData, hp.fingerData[i]);
                if (distance > threshold)
                {
                    isDiscarded = true;
                    break;
                }
                sumDistance += distance;
            }

            if (!isDiscarded && sumDistance < currentMin)
            {
                currentMin = sumDistance;
                currentHandPose = hp;
            }


        }



        return currentHandPose;
    }



}
