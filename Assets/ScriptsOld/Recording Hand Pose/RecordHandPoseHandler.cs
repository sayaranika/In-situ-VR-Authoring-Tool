//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class RecordHandPoseHandler : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject rightHand;
//    [SerializeField]
//    private GameObject leftHand;
//    [SerializeField] string filename;
//    [SerializeField]
//    private Text systemStatusText;

//    [SerializeField]
//    private Text leftHandStatusText;

//    [SerializeField]
//    private Text rightHandStatusText;



//    private OVRHand ovrHandRight;
//    private OVRSkeleton ovrSkeletonRight;
//    private List<OVRBone> fingerBonesRight;

//    private OVRHand ovrHandLeft;
//    private OVRSkeleton ovrSkeletonLeft;
//    private List<OVRBone> fingerBonesLeft;


//    List<HandPose> handPoseEntries = new List<HandPose>();
//    int count = 0;
//    private int countdownTime = 3;
//    private bool isCoroutineNotRunning = true;



//    void Start()
//    {
//        ovrHandRight = rightHand.GetComponent<OVRHand>();
//        ovrSkeletonRight = rightHand.GetComponent<OVRSkeleton>();
//        ovrHandLeft = leftHand.GetComponent<OVRHand>();
//        ovrSkeletonLeft = leftHand.GetComponent<OVRSkeleton>();

//        handPoseEntries = FileHandler.ReadListFromJSON<HandPose>(filename);

//        systemStatusText.gameObject.SetActive(true);
//        rightHandStatusText.gameObject.SetActive(true);
//        leftHandStatusText.gameObject.SetActive(true);
//        systemStatusText.text = "Pinch with both hands to start recording";
//        rightHandStatusText.text = "Status(Left): NA";
//        leftHandStatusText.text = "Status(Right): NA";
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (checkPinchState() && isCoroutineNotRunning) StartCoroutine(CountdownToCapture());
//    }

//    bool checkPinchState()
//    {
//        bool isIndexFingerPinchingLeft = ovrHandLeft.GetFingerIsPinching(OVRHand.HandFinger.Index);
//        float indexFingerPinchStrengthLeft = ovrHandLeft.GetFingerPinchStrength(OVRHand.HandFinger.Index);

//        bool isIndexFingerPinchingRight = ovrHandRight.GetFingerIsPinching(OVRHand.HandFinger.Index);
//        float indexFingerPinchStrengthRight = ovrHandRight.GetFingerPinchStrength(OVRHand.HandFinger.Index);

//        if (isIndexFingerPinchingLeft && isIndexFingerPinchingRight)
//        {
//            return true;
//        }
//        return false;
//    }

//    IEnumerator CountdownToCapture()
//    {
//        isCoroutineNotRunning = false;

//        while (countdownTime > 0)
//        {

//            systemStatusText.text = countdownTime.ToString();

//            yield return new WaitForSeconds(1f);
//            countdownTime--;
//        }


//        systemStatusText.text = "Recording Data";

//        yield return new WaitForSeconds(1f);

//        if (ovrSkeletonLeft.IsInitialized && ovrHandLeft.IsTracked)
//        {
//            fingerBonesLeft = new List<OVRBone>(ovrSkeletonLeft.Bones);
//            Save(false, true);
//            leftHandStatusText.text = "Status(Right): Successfully saved";
//        }

//        if (ovrSkeletonRight.IsInitialized && ovrHandRight.IsTracked)
//        {
//            fingerBonesRight = new List<OVRBone>(ovrSkeletonRight.Bones);
//            Debug.Log("Number of bones: " + fingerBonesRight.Count);
//            Save(true, false);
//            rightHandStatusText.text = "Status(Left): Successfully saved";
//        }
//        isCoroutineNotRunning = true;
//        countdownTime = 3;
//        systemStatusText.text = "Pinch with both hands to start recording again";
//    }


//    void Save(bool isRight, bool isLeft)
//    {
//        if(isRight == true)
//        {
//            string name = "Pose_R_" + count.ToString();
//            List<Vector3> data = new List<Vector3>();
//            foreach (var bone in fingerBonesRight)
//            {
//                data.Add(ovrSkeletonRight.transform.InverseTransformPoint(bone.Transform.position));
//            }
//            Quaternion wristRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.rotation;
//            HandPose entry = new HandPose(name, data, wristRotation, false, true);

//            handPoseEntries.Add(entry);
//            //nameInput.text = "";

//            FileHandler.SaveToJSON<HandPose>(handPoseEntries, filename);
//            count++;
//            Debug.Log("Saved R");
//        }
//        else
//        {
//            string name = "Pose_L_" + count.ToString();
//            List<Vector3> data = new List<Vector3>();
//            foreach (var bone in fingerBonesLeft)
//            {
//                data.Add(ovrSkeletonLeft.transform.InverseTransformPoint(bone.Transform.position));
//            }
//            Quaternion wristRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.rotation;
//            HandPose entry = new HandPose(name, data, wristRotation, true, false);

//            handPoseEntries.Add(entry);

//            FileHandler.SaveToJSON<HandPose>(handPoseEntries, filename);
//            count++;
//            Debug.Log("Saved L");
//        }
       
//    } 
//}
