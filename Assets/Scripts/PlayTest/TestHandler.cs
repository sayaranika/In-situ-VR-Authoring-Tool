//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using UnityEngine.Events;

////valem
//public struct RightHandGesture
//{
//    public List<Vector3> fingers;
//    public int transitionSceneId;
//    public UnityEvent onRecognized;
//}

//public struct LeftHandGesture
//{
//    public List<Vector3> fingers;
//    public int transitionSceneId;
//    public UnityEvent onRecognized;
//}


//public class TestHandler : MonoBehaviour
//{

//    private List<HandPoseSensor> handPosesRight;
//    private List<HandPoseSensor> handPosesLeft;

//    private GameObject rightHandAnchor;
//    private Transform rightHand;
//    private OVRHand ovrHandRight;
//    private OVRSkeleton ovrSkeletonRight;
//    private List<OVRBone> fingerBonesRight;


//    private GameObject leftHandAnchor;
//    private Transform leftHand;
//    private OVRHand ovrHandLeft;
//    private OVRSkeleton ovrSkeletonLeft;
//    private List<OVRBone> fingerBonesLeft;

//    //private List<OVRBone> fingerBonesLeft;

//    public float threshold = 0.6f;

//    [SerializeField] private TMPro.TMP_Dropdown sceneDropdown;
//    [SerializeField] private GameObject manager;

//    private bool testInProgress = false;



//    public List<RightHandGesture> rightHandGestures;
//    //public List<LeftHandGesture> leftHandGestures;
//    private RightHandGesture previousRightHandPose;
//    //private LeftHandGesture previousLeftHandPose;
//    void Start()
//    {

//    }

//    void Update()
//    {

//        if (testInProgress)
//        {
//            //right hand recognizer

//            RightHandGesture currentRightHandPose = rightHandGestureRecognizer();
//            bool hasRecognized = !currentRightHandPose.Equals(new RightHandGesture());

//            if (hasRecognized && !currentRightHandPose.Equals(previousRightHandPose))
//            {
//                Debug.Log("New Gesture found, transition to" + currentRightHandPose.transitionSceneId);

//                GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");
//                if (listOfObjects.Length > 0)
//                {
//                    foreach (var obj in listOfObjects)
//                    {

//                        Destroy(obj);
//                    }


//                }

//                manager.GetComponent<initializeScene>().loadGameObjects(currentRightHandPose.transitionSceneId);
//                previousRightHandPose = currentRightHandPose;
//                currentRightHandPose.onRecognized.Invoke();
//            }



//            //left hand recognizer
//            /*LeftHandGesture currentLeftHandPose = leftHandGestureRecognizer();
//            bool hasLeftRecognized = !currentLeftHandPose.Equals(new LeftHandGesture());

//            if (hasLeftRecognized && !currentLeftHandPose.Equals(previousLeftHandPose))
//            {
//                Debug.Log("New Gesture found, transition to" + currentLeftHandPose.transitionSceneId);

//                GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");
//                if (listOfObjects.Length > 0)
//                {
//                    foreach (var obj in listOfObjects)
//                    {

//                        Destroy(obj);
//                    }


//                }

//                manager.GetComponent<initializeScene>().loadGameObjects(currentLeftHandPose.transitionSceneId);
//                previousLeftHandPose = currentLeftHandPose;
//                //currentLeftHandPose.onRecognized.Invoke();
//            }*/

//        } //test in progress
//    }


//    public void initiateTest()
//    {
//        initializeRightHand();
//        //initializeLeftHand();

//        StartCoroutine(initializeHands());

//        fingerBonesRight = new List<OVRBone>(ovrSkeletonRight.Bones);
//        //fingerBonesLeft = new List<OVRBone>(ovrSkeletonLeft.Bones);

//        int startScene = sceneDropdown.value + 1;
//        manager.GetComponent<initializeScene>().loadGameObjects(startScene);


//        rightHandGestures = new List<RightHandGesture>();
//        //leftHandGestures = new List<LeftHandGesture>();
//        //previousLeftHandPose = new LeftHandGesture();
//        previousRightHandPose = new RightHandGesture();



//        loadSceneConditions(startScene);



//        testInProgress = true;
//    }

//    IEnumerator initializeHands()
//    {
//        while (!(ovrSkeletonRight.IsInitialized && ovrHandRight.IsTracked))
//        {
//            yield return null;
//        }

//        /*while (!(ovrSkeletonLeft.IsInitialized && ovrHandLeft.IsTracked))
//        {
//            yield return null;
//        }*/
//    }

//    private void loadSceneConditions(int sceneID)
//    {
//        List<SceneObj> scenes = FileHandler.ReadListFromJSON<SceneObj>("Root.json");
//        List<HandPoseSensor> handPoses = FileHandler.ReadListFromJSON<HandPoseSensor>("handLibrary.json");
//        foreach (var scene in scenes)
//        {
//            if (scene.sceneID == sceneID)
//            {
//                List<HandPoseConditions> handConditions = scene.handPoseConditions;

//                foreach (var condition in handConditions)
//                {
//                    foreach (var hp in handPoses)
//                    {
//                        if (condition.handPoseId == hp.handPoseId)
//                        {
//                            if (hp.isLeft)
//                            {
//                                LeftHandGesture leftHand = new LeftHandGesture();
//                                leftHand.fingers = hp.fingerData;
//                                leftHand.transitionSceneId = condition.transitionSceneId;
//                                //leftHandGestures.Add(leftHand);
//                            }
//                            else
//                            {
//                                RightHandGesture rightHand = new RightHandGesture();
//                                rightHand.fingers = hp.fingerData;
//                                rightHand.transitionSceneId = condition.transitionSceneId;
//                                rightHandGestures.Add(rightHand);
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }


//    RightHandGesture rightHandGestureRecognizer()
//    {
//        RightHandGesture currentHandPose = new RightHandGesture();
//        float currentMin = Mathf.Infinity;

//        foreach (var hp in rightHandGestures)
//        {
//            float sumDistance = 0;
//            bool isDiscarded = false;
//            for (int i = 0; i < fingerBonesRight.Count; i++)
//            {
//                Vector3 currentData = ovrSkeletonRight.transform.InverseTransformPoint(fingerBonesRight[i].Transform.position);
//                float distance = Vector3.Distance(currentData, hp.fingers[i]);
//                if (distance > threshold)
//                {
//                    isDiscarded = true;
//                    break;
//                }
//                sumDistance += distance;
//            }

//            if (!isDiscarded && sumDistance < currentMin)
//            {
//                currentMin = sumDistance;
//                currentHandPose = hp;
//            }
//        }
//        return currentHandPose;
//    }

//    /*LeftHandGesture leftHandGestureRecognizer()
//    {
//        LeftHandGesture currentHandPose = new LeftHandGesture();
//        float currentMin = Mathf.Infinity;

//        foreach (var hp in leftHandGestures)
//        {
//            float sumDistance = 0;
//            bool isDiscarded = false;
//            for (int i = 0; i < fingerBonesLeft.Count; i++)
//            {
//                Vector3 currentData = ovrSkeletonLeft.transform.InverseTransformPoint(fingerBonesLeft[i].Transform.position);
//                float distance = Vector3.Distance(currentData, hp.fingers[i]);
//                if (distance > threshold)
//                {
//                    isDiscarded = true;
//                    break;
//                }
//                sumDistance += distance;
//            }

//            if (!isDiscarded && sumDistance < currentMin)
//            {
//                currentMin = sumDistance;
//                currentHandPose = hp;
//            }
//        }
//        return currentHandPose;
//    }*/


//    void initializeRightHand()
//    {
//        rightHandAnchor = GameObject.Find("RightHandAnchor");
//        if (rightHandAnchor != null)
//        {
//            rightHand = rightHandAnchor.transform.Find("OVRHandPrefab_Right");
//            if (rightHand != null)
//            {
//                ovrHandRight = rightHand.GetComponent<OVRHand>();
//                ovrSkeletonRight = rightHand.GetComponent<OVRSkeleton>();

//            }
//        }


//    }

//    /*void initializeLeftHand()
//    {
//        leftHandAnchor = GameObject.Find("LeftHandAnchor");
//        if (leftHandAnchor != null)
//        {
//            leftHand = leftHandAnchor.transform.Find("OVRHandPrefab_Right");
//            if (leftHand != null)
//            {
//                ovrHandLeft = leftHand.GetComponent<OVRHand>();
//                ovrSkeletonLeft = leftHand.GetComponent<OVRSkeleton>();

//            }
//        }


//    }*/


//}
