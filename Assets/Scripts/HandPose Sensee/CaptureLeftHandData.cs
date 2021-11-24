using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureLeftHandData : MonoBehaviour
{
    [SerializeField] private Text trackingText;
    [SerializeField] private Text warningText;
    [SerializeField] private Text countdownText;
    [SerializeField] private GameObject saveDialog;
    [SerializeField] private GameObject statusPanel;
    [SerializeField] private GameObject handModel;
    [SerializeField] private GameObject recordingOptions;
    [SerializeField] private GameObject hpButtonList;
    [SerializeField] private GameObject titleText;


    [SerializeField] private GameObject tab;
    [SerializeField] private GameObject glow;
    [SerializeField] private GameObject builderPanel;
    [SerializeField] private GameObject triggerPanel;

    private GameObject leftHandAnchor;
    private Transform leftHand;
    private OVRHand ovrHandLeft;
    private OVRSkeleton ovrSkeletonLeft;
    private List<OVRBone> fingerBonesLeft;

    Quaternion wristRotation;
    List<HandPoseSensor> handPoseEntries = new List<HandPoseSensor>();

    private int countdownTime = 5;
    private int i = 0;
    private int isSession = 0;
    int totalHandPose;

    //mapping data
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
    // Start is called before the first frame update
    void Start()
    {
        
        gameObject.SetActive(false);
        saveDialog.SetActive(false);
        handModel.SetActive(false);
        FileHandler.SaveToJSON<HandPoseSensor>(new List<HandPoseSensor>(), "handLibrary.json");
    }

    private void OnEnable()
    {
        if (i != 0)
        {
            trackingText.text = "initializing";
            initializeLeftHand();
            PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOff);

            countdownText.text = countdownTime.ToString();
            StartCoroutine(CountdownToCapture());
        }
        i++;

        //List<HandPoseSensor> s = FileHandler.ReadListFromJSON<SceneObj>("Root.json");
    }

    IEnumerator CountdownToCapture()
    {
        titleText.GetComponent<Text>().text = "Recording Left Hand Pose";
        while (!(ovrSkeletonLeft.IsInitialized && ovrHandLeft.IsTracked))
        {
            yield return null;
        }

        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownTime--;
            Debug.Log("Executed");
            countdownText.text = countdownTime.ToString();
        }
        trackingText.text = "Do the hand pose you want to record. Data will be captured at the end of the countdown"; //added now
        yield return new WaitForSeconds(1f);

        if (ovrSkeletonLeft.IsInitialized && ovrHandLeft.IsTracked)
        {
            handPoseEntries = FileHandler.ReadListFromJSON<HandPoseSensor>("handLibrary.json");
            totalHandPose = handPoseEntries.Count + 1;
            Debug.Log(totalHandPose);
            fingerBonesLeft = new List<OVRBone>(ovrSkeletonLeft.Bones);
            List<Vector3> data = new List<Vector3>();
            foreach (var bone in fingerBonesLeft)
            {
                data.Add(ovrSkeletonLeft.transform.InverseTransformPoint(bone.Transform.position));
            }
            Quaternion wristRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.rotation;
            HandPoseSensor entry = new HandPoseSensor(totalHandPose, data, wristRotation, true, false);

            PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOn);
            //mapDataToHandModel();

            handPoseEntries.Add(entry);
            //nameInput.text = "";

            Debug.Log("left coroutine");
            statusPanel.SetActive(false);
            handModel.SetActive(true);
            mapDataToHandModel();
            saveDialog.SetActive(true);

        }
        else
        {
            StopCoroutine(CountdownToCapture());
            countdownTime = 5;
            StartCoroutine(CountdownToCapture());
        }

        countdownTime = 5;

    }

    // Update is called once per frame
    void Update()
    {
        if (isSession == 1)
        {
            if (ovrHandLeft.IsTracked == true)
            {
                trackingText.text = "Do the hand pose you want to record. Data will be captured at the end of the countdown"; //added from ok
                warningText.text = "";
            }
            else
            {
                trackingText.text = "Put away the controllers and place your hands right in front of you in the camera's view"; //added
                warningText.text = "Tracking Status: Lost tracking";
                countdownTime = 6;
            }
        }
    }

    void initializeLeftHand()
    {
        leftHandAnchor = GameObject.Find("LeftHandAnchor");
        if (leftHandAnchor != null)
        {
            leftHand = leftHandAnchor.transform.Find("OVRHandPrefab_Left");
            if (leftHand != null)
            {
                ovrHandLeft = leftHand.GetComponent<OVRHand>();
                ovrSkeletonLeft = leftHand.GetComponent<OVRSkeleton>();
                isSession = 1;
            }
        }
        else
        {
            warningText.text = "Could not initialize left hand";
            trackingText.text = "";
        }

    }


    void mapDataToHandModel()
    {
        wrist.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.localPosition + new Vector3(0.01f, -0.1f, 0);
        wrist.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.localRotation;

        index1.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Index1].Transform.localPosition;
        index2.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Index2].Transform.localPosition;
        index3.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Index3].Transform.localPosition;
        index1.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Index1].Transform.localRotation;
        index2.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Index2].Transform.localRotation;
        index3.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Index3].Transform.localRotation;

        thumb0.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Thumb0].Transform.localPosition;
        thumb1.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Thumb1].Transform.localPosition;
        thumb2.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Thumb2].Transform.localPosition;
        thumb3.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Thumb3].Transform.localPosition;
        thumb0.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Thumb0].Transform.localRotation;
        thumb1.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Thumb1].Transform.localRotation;
        thumb2.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Thumb2].Transform.localRotation;
        thumb3.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Thumb3].Transform.localRotation;

        middle1.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Middle1].Transform.localPosition;
        middle2.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Middle2].Transform.localPosition;
        middle3.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Middle3].Transform.localPosition;
        middle1.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Middle1].Transform.localRotation;
        middle2.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Middle2].Transform.localRotation;
        middle3.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Middle3].Transform.localRotation;

        ring1.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Ring1].Transform.localPosition;
        ring2.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Ring2].Transform.localPosition;
        ring3.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Ring3].Transform.localPosition;
        ring1.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Ring1].Transform.localRotation;
        ring2.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Ring2].Transform.localRotation;
        ring3.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Ring3].Transform.localRotation;

        pinky0.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Pinky0].Transform.localPosition;
        pinky1.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Pinky1].Transform.localPosition;
        pinky2.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Pinky2].Transform.localPosition;
        pinky3.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Pinky3].Transform.localPosition;
        pinky0.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Pinky0].Transform.localRotation;
        pinky1.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Pinky1].Transform.localRotation;
        pinky2.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Pinky2].Transform.localRotation;
        pinky3.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Pinky3].Transform.localRotation;


        forearmStub.transform.localPosition = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_ForearmStub].Transform.localPosition;

        wrist.transform.rotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.rotation;
        forearmStub.transform.localRotation = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_ForearmStub].Transform.localRotation;
    }

    public void saveHandPose()
    {
        FileHandler.SaveToJSON<HandPoseSensor>(handPoseEntries, "handLibrary.json");
        
        

        statusPanel.SetActive(false);
        recordingOptions.SetActive(false);
        tab.SetActive(true);
        builderPanel.SetActive(false);
        triggerPanel.SetActive(true);

        Transform newHPButton = hpButtonList.transform.Find("Left" + totalHandPose.ToString());
        if (newHPButton != null)
        {
            newHPButton.gameObject.SetActive(true);

            Transform Hleft = newHPButton.GetChild(0);
            Debug.Log("my name is " + Hleft.name);

            Transform Hwrist = Hleft.transform.GetChild(0).gameObject.transform.GetChild(0);
            Debug.Log("my name is " + Hwrist.name);
            Transform Hforearm_stub = Hwrist.transform.GetChild(0);
            Debug.Log("my name is " + Hforearm_stub.name);
            Transform Hindex1 = Hwrist.transform.GetChild(1);
            Debug.Log("my name is " + Hindex1.name);
            Transform Hindex2 = Hindex1.transform.GetChild(0);
            Debug.Log("my name is " + Hindex2.name);
            Transform Hindex3 = Hindex2.transform.GetChild(0);
            Debug.Log("my name is " + Hindex3.name);

            Transform Hmiddle1 = Hwrist.transform.GetChild(2);
            Debug.Log("my name is " + Hmiddle1.name);
            Transform Hmiddle2 = Hmiddle1.transform.GetChild(0);
            Debug.Log("my name is " + Hmiddle2.name);
            Transform Hmiddle3 = Hmiddle2.transform.GetChild(0);
            Debug.Log("my name is " + Hmiddle3.name);

            Transform Hpinky0 = Hwrist.transform.GetChild(3);
            Debug.Log("my name is " + Hpinky0.name);
            Transform Hpinky1 = Hpinky0.transform.GetChild(0);
            Debug.Log("my name is " + Hpinky1.name);
            Transform Hpinky2 = Hpinky1.transform.GetChild(0);
            Debug.Log("my name is " + Hpinky2.name);
            Transform Hpinky3 = Hpinky2.transform.GetChild(0);
            Debug.Log("my name is " + Hpinky3.name);


            Transform Hring1 = Hwrist.transform.GetChild(4);
            Debug.Log("my name is " + Hring1.name);
            Transform Hring2 = Hring1.transform.GetChild(0);
            Debug.Log("my name is " + Hring2.name);
            Transform Hring3 = Hring2.transform.GetChild(0);
            Debug.Log("my name is " + Hring3.name);



            Transform Hthumb0 = Hwrist.transform.GetChild(5);
            Debug.Log("my name is " + Hthumb0.name);
            Transform Hthumb1 = Hthumb0.transform.GetChild(0);
            Debug.Log("my name is " + Hthumb1.name);
            Transform Hthumb2 = Hthumb1.transform.GetChild(0);
            Debug.Log("my name is " + Hthumb2.name);
            Transform Hthumb3 = Hthumb2.transform.GetChild(0);
            Debug.Log("my name is " + Hthumb3.name);



            Hwrist.localPosition = wrist.transform.localPosition;
            Hwrist.localRotation = wrist.transform.localRotation;

            Hindex1.localPosition = index1.transform.localPosition;
            Hindex2.localPosition = index2.transform.localPosition;
            Hindex3.localPosition = index3.transform.localPosition;
            Hindex1.localRotation = index1.transform.localRotation;
            Hindex2.localRotation = index2.transform.localRotation;
            Hindex3.localRotation = index3.transform.localRotation;

            Hthumb0.localPosition = thumb0.transform.localPosition;
            Hthumb1.localPosition = thumb1.transform.localPosition;
            Hthumb2.localPosition = thumb2.transform.localPosition;
            Hthumb3.localPosition = thumb3.transform.localPosition;
            Hthumb0.localRotation = thumb0.transform.localRotation;
            Hthumb1.localRotation = thumb1.transform.localRotation;
            Hthumb2.localRotation = thumb2.transform.localRotation;
            Hthumb3.localRotation = thumb3.transform.localRotation;

            Hmiddle1.localPosition = middle1.transform.localPosition;
            Hmiddle2.localPosition = middle2.transform.localPosition;
            Hmiddle3.localPosition = middle3.transform.localPosition;
            Hmiddle1.localRotation = middle1.transform.localRotation;
            Hmiddle2.localRotation = middle2.transform.localRotation;
            Hmiddle3.localRotation = middle3.transform.localRotation;

            Hring1.localPosition = ring1.transform.localPosition;
            Hring2.localPosition = ring2.transform.localPosition;
            Hring3.localPosition = ring3.transform.localPosition;
            Hring1.transform.localRotation = ring1.transform.localRotation;
            Hring2.transform.localRotation = ring2.transform.localRotation;
            Hring3.transform.localRotation = ring3.transform.localRotation;

            Hpinky0.localPosition = pinky0.transform.localPosition;
            Hpinky1.localPosition = pinky1.transform.localPosition;
            Hpinky2.localPosition = pinky2.transform.localPosition;
            Hpinky3.localPosition = pinky3.transform.localPosition;
            Hpinky0.localRotation = pinky0.transform.localRotation;
            Hpinky1.localRotation = pinky1.transform.localRotation;
            Hpinky2.localRotation = pinky2.transform.localRotation;
            Hpinky3.localRotation = pinky3.transform.localRotation;


            Hforearm_stub.transform.localPosition = forearmStub.transform.localPosition;

            //wrist.transform.rotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.rotation;
            Hforearm_stub.localRotation = forearmStub.transform.localRotation;

            Hleft.GetComponent<handPoseId>().myHandPoseId = totalHandPose;



            /*Hwrist.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.localPosition;
            Hwrist.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.localRotation;

            Hindex1.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index1].Transform.localPosition;
            Hindex2.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index2].Transform.localPosition;
            Hindex3.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index3].Transform.localPosition;
            Hindex1.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index1].Transform.localRotation;
            Hindex2.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index2].Transform.localRotation;
            Hindex3.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index3].Transform.localRotation;

            Hthumb0.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb0].Transform.localPosition;
            Hthumb1.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb1].Transform.localPosition;
            Hthumb2.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb2].Transform.localPosition;
            Hthumb3.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb3].Transform.localPosition;
            Hthumb0.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb0].Transform.localRotation;
            Hthumb1.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb1].Transform.localRotation;
            Hthumb2.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb2].Transform.localRotation;
            Hthumb3.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Thumb3].Transform.localRotation;

            Hmiddle1.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Middle1].Transform.localPosition;
            Hmiddle2.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Middle2].Transform.localPosition;
            Hmiddle3.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Middle3].Transform.localPosition;
            Hmiddle1.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Middle1].Transform.localRotation;
            Hmiddle2.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Middle2].Transform.localRotation;
            Hmiddle3.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Middle3].Transform.localRotation;

            Hring1.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Ring1].Transform.localPosition;
            Hring2.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Ring2].Transform.localPosition;
            Hring3.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Ring3].Transform.localPosition;
            Hring1.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Ring1].Transform.localRotation;
            Hring2.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Ring2].Transform.localRotation;
            Hring3.transform.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Ring3].Transform.localRotation;

            Hpinky0.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky0].Transform.localPosition;
            Hpinky1.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky1].Transform.localPosition;
            Hpinky2.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky2].Transform.localPosition;
            Hpinky3.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky3].Transform.localPosition;
            Hpinky0.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky0].Transform.localRotation;
            Hpinky1.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky1].Transform.localRotation;
            Hpinky2.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky2].Transform.localRotation;
            Hpinky3.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Pinky3].Transform.localRotation;


            Hforearm_stub.transform.localPosition = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_ForearmStub].Transform.localPosition;

            //wrist.transform.rotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.rotation;
            Hforearm_stub.localRotation = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_ForearmStub].Transform.localRotation;

            Debug.Log("mapped all"+Hpinky3.name);*/
        }



        glow.SetActive(true);
        saveDialog.SetActive(false);
        handModel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void cancelSession()
    {
        statusPanel.SetActive(false);
        recordingOptions.SetActive(false);
        tab.SetActive(true);
        builderPanel.SetActive(false);
        triggerPanel.SetActive(true);
        glow.SetActive(true);
        saveDialog.SetActive(false);
        handModel.SetActive(false);
        gameObject.SetActive(false);
    }
}
