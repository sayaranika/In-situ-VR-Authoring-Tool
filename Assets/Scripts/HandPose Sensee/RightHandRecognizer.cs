//using Microsoft.MixedReality.Toolkit.Input;
//using Microsoft.MixedReality.Toolkit.UI;
//using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public struct RightHandGesture
//{
//    public List<Vector3> fingers;
//    public int transitionSceneId;
//    public UnityEvent onRecognized;
//}

//public class RightHandRecognizer : MonoBehaviour
//{
//    private GameObject rightHandAnchor;
//    private Transform rightHand;
//    private OVRHand ovrHandRight;
//    private OVRSkeleton ovrSkeletonRight;
//    private List<OVRBone> fingerBonesRight;

//    public float threshold = 0.7f;

//    [SerializeField] private TMPro.TMP_Dropdown sceneDropdown;
//    [SerializeField] private GameObject manager;

//    private bool testingNow = false;

//    public List<RightHandGesture> rightHandGestures;
//    private RightHandGesture previousRightHandPose;

//    public void initiateTest()
//    {
//        Debug.Log("initiating test");
//        Debug.Log("YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY");
//        initializeRightHand();
//        StartCoroutine(initializeHands());

//        int startScene = sceneDropdown.value + 1;
//        Debug.Log("in initiate test start scene is " + startScene + "  HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");

//        manager.GetComponent<initializeScene>().loadGameObjects(startScene);

//        GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");
//        if (listOfObjects.Length > 0)
//        {
//            foreach (var obj in listOfObjects)
//            {

//                obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().enabled = false;
//                obj.GetComponent<BoundsControl>().HighlightWires();
//                obj.GetComponent<BoundsControl>().ScaleHandlesConfig.ShowScaleHandles = false;
//                obj.GetComponent<RotationAxisConstraint>().enabled = true;
//                Debug.Log("Length: " + listOfObjects.Length);
//            }
//        }
//        rightHandGestures = new List<RightHandGesture>();
//        previousRightHandPose = new RightHandGesture();
//        loadSceneConditions(startScene);

//        Debug.Log("Completed init");

//        testingNow = true;

//        /*PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOff);*/
//    }
//    void Update()
//    {
//        if (testingNow)
//        {
//            RightHandGesture currentRightHandPose = rightHandGestureRecognizer();
//            bool hasRecognized = !currentRightHandPose.Equals(new RightHandGesture());
//            Debug.Log("5000 has recognized : " + hasRecognized.ToString());
          
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
//        }
//    }
//    IEnumerator initializeHands()
//    {
//        Debug.Log("cOURINEEEEE@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
//        while (!(ovrSkeletonRight.IsInitialized && ovrHandRight.IsTracked))
//        {
//            yield return null;
//        }
//        Debug.Log("Hands initialized");
//        if (ovrSkeletonRight.IsInitialized)
//        {
//            fingerBonesRight = new List<OVRBone>(ovrSkeletonRight.Bones);
//        }
//        else
//        {
//            Debug.Log("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ");
//        }

//    }
//    private void loadSceneConditions(int sceneID)
//    {
//        Debug.Log("Start of load conditions");
//        List<SceneObj> scenes = FileHandler.ReadListFromJSON<SceneObj>("Root.json");
//        Debug.Log("scenes count" + scenes.Count);
//        List<HandPoseSensor> handPoses = FileHandler.ReadListFromJSON<HandPoseSensor>("handLibrary.json");
//        Debug.Log("hand Poses count " + handPoses.Count);
//        foreach (var scene in scenes)
//        {
//            if (scene.sceneID == sceneID)
//            {
//                Debug.Log("Scene id matched for " + scene.sceneID + ", " + sceneID);
//                List<HandPoseConditions> handConditions = scene.handPoseConditions;
//                Debug.Log("hand Conditions found: " + handConditions.Count);
//                foreach (var condition in handConditions)
//                {
//                    foreach (var hp in handPoses)
//                    {
//                        if (condition.handPoseId == hp.handPoseId)
//                        {
//                            if (hp.isRight)
//                            {
//                                Debug.Log("Inside hp.isRight");
//                                RightHandGesture rightHand = new RightHandGesture();
//                                rightHand.fingers = hp.fingerData;
//                                rightHand.transitionSceneId = condition.transitionSceneId;
//                                Debug.Log(rightHand.transitionSceneId);
//                                Debug.Log("10000MMMMMMMMMMMMMMMMMMMMMMMM " +condition.handPoseId);
//                                rightHandGestures.Add(rightHand);

//                                Debug.Log("Right hand condition");
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        Debug.Log("eND of load condition");
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
//                Debug.Log("sum distance is "+sumDistance); 
//                currentMin = sumDistance;
//                currentHandPose = hp;
//            }
//        }

//        //Debug.Log("CurrentHandPose + " + currentHandPose.l)
//        return currentHandPose;
//    }
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
//                Debug.Log("fOUND");

//            }
//        }


//    }


//}
