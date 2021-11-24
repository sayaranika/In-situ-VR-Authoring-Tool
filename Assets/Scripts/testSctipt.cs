using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public struct HandConfig
{
    public string name; //name of the pose
    public List<Vector3> fingerData;
    public UnityEvent onRecognized;
}

public class testSctipt : MonoBehaviour
{ 
    private Transform leftHand;
    private Transform rightHand;

    private OVRHand ovrHandLeft;
    private OVRSkeleton ovrSkeletonLeft;
    private List<OVRBone> fingerBonesLeft;


    private OVRHand ovrHandRight;
    private OVRSkeleton ovrSkeletonRight;
    private List<OVRBone> fingerBonesRight;

    public List<HandConfig> handPosesRight;
    private HandConfig previousHandPose;
    
    //threshold for pinching to start record
    public float threshold = 0.7f;

    [SerializeField] private Text status;
    private int countdownTime = 3;
    private bool isCoroutineNotRunning = true;
    int count = 0;
    private GameObject rightHandAnchor;
    private GameObject leftHandAnchor;
    

    // Start is called before the first frame update
    void Start()
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
        else
        {
            status.text = "Could not initialize right hand";
            //trackingText.text = "";
        }

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
        else
        {
            status.text = "Could not initialize left hand";
            //trackingText.text = "";
        }
        
        

        handPosesRight = new List<HandConfig>();


        status.text = "data loaded";

        previousHandPose = new HandConfig();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkPinchState() && isCoroutineNotRunning) StartCoroutine(CountdownToCapture());
        HandConfig currentHandPose = Recognize();
        bool hasRecognized = !currentHandPose.Equals(new HandConfig());

        if (hasRecognized && !currentHandPose.Equals(previousHandPose))
        {
            Debug.Log("New Gesture found" + currentHandPose.name);
            status.text = "Found " + currentHandPose.name;
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

            status.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);
            countdownTime--;
        }


        status.text = "Recording Data";

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
        status.text = "Pinch with both hands to start recording";
        
    }


    void Save()
    {
        HandConfig handPoseRight = new HandConfig();
        handPoseRight.name = "New Gesture" + count.ToString();
        List<Vector3> data = new List<Vector3>();
        Debug.Log("Saving following data:");
        int i = 0;
        foreach (var bone in fingerBonesRight)
        {
            data.Add(ovrSkeletonRight.transform.InverseTransformPoint(bone.Transform.position));
            Vector3 g = ovrSkeletonRight.transform.InverseTransformPoint(bone.Transform.position);
            Debug.Log("101: bone " + i + ": " + string.Format("( {0:F4}, {0:F4}, {0:F4}", g.x, g.y, g.z));
            Debug.Log(g);
            //string.Format("Your total is {0:F3}, have a nice day.", x);
            i++;

        }




        handPoseRight.fingerData = data;
        handPosesRight.Add(handPoseRight);

        count++;
    }

    HandConfig Recognize()
    {
        HandConfig currentHandPose = new HandConfig();


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
