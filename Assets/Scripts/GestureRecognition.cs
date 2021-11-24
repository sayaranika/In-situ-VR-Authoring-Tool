using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using Microsoft.MixedReality.Toolkit.Input;
using TMPro;

public struct Gesture
{
    public int Id;
    public List<Vector3> fingerDatas;
    public int transitionSceneId;
    public UnityEvent onRecognized;
}


public struct SessionPoses
{
    public int Id;
    public List<Vector3> f_data;
}


public class GestureRecognition : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown sceneDropdown;
    [SerializeField] private GameObject manager;
    [SerializeField] private Text stext;
    [SerializeField]
    private GameObject[] models;
    [SerializeField] private GameObject container;


    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject toggleButton;
    [SerializeField] private GameObject thresholdButton;
    [SerializeField] private GameObject headerText;
    [SerializeField] private GameObject dropdownButton;

    private OVRSkeleton skeleton;
    public List<Gesture> gestures;
    private List<OVRBone> fingerBones;

    private Transform rightHand;
    private GameObject rightHandAnchor;

    private Gesture previousGesture;

    float threshold = 0.7f;
    public bool testSessionStarted = false;
    private bool hasRecognize = false;
    private bool done = false;
    private bool shouldLoadScene = false;
    private int activeScene;


    private float distanceToMonitor = -1;
    private int nextScene = -1;
    private GameObject objectToMonitor;

    private List<SessionPoses> listOfPoses;
    int startScene;
    private int frameToLoad;
    private bool playAnimations;
    //private int changedScene = 1;
    //private int valChanged = 0;


    void Start()
    {
        testSessionStarted = false;
        shouldLoadScene = false;
        stext.text = "";
        playAnimations = true;
    }


    void Update()
    {

        if (testSessionStarted)
        {

            // start to Recognize every gesture we make
            Gesture currentGesture = RecognizeStored();

            // we will associate the recognize to a boolean to see if the Gesture
            // we are going to make is one of the gesture we already saved
            hasRecognize = !currentGesture.Equals(new Gesture());

            // and if the gesture is recognized
            if (hasRecognize  && activeScene != currentGesture.transitionSceneId)
            {
                // we change another boolean to avoid a loop of event
                done = true;
                if (activeScene != currentGesture.transitionSceneId)
                {
                    Debug.Log("5000A: Gesture Recognized Active / Loading Scene: " + activeScene + " " + currentGesture.transitionSceneId);
                    playAnimations = false;
                    GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");
                    if (listOfObjects.Length > 0)
                    {
                        foreach (var obj in listOfObjects)
                        {

                            Destroy(obj.gameObject);
                        }


                    }
                    frameToLoad = 0;
                    manager.GetComponent<initializeScene>().loadGameObjects(currentGesture.transitionSceneId);
                    playAnimations = true;
                    GameObject[] a = GameObject.FindGameObjectsWithTag("spawnedObject");
                    if (a.Length > 0)
                    {
                        foreach (var obj in a)
                        {

                            obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().enabled = false;
                            obj.GetComponent<BoundsControl>().enabled = false;
                        }

                    }
                    loadSceneConditions(currentGesture.transitionSceneId);
                    activeScene = currentGesture.transitionSceneId;
                    loadProximityConditions();
                }
            }
            // if the gesture we done is no more recognized
            else
            {
                // and we just activated the boolean from earlier
                if (done)
                {
                    Debug.Log("Not Recognized");
                    // we set to false the boolean again, so this will not loop
                    done = false;
                }
            }

            if (distanceToMonitor > 0 && objectToMonitor != null )
            {
                if (Vector3.Distance(objectToMonitor.transform.position, skeleton.Bones[(int)OVRPlugin.BoneId.Hand_Index1].Transform.position) <= distanceToMonitor)
                {
                    playAnimations = false;
                    Debug.Log("6000A: Distance Recognized Active / Loading Scene" + activeScene + " " + currentGesture.transitionSceneId + " " + nextScene);
                    GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");
                    if (listOfObjects.Length > 0)
                    {
                        foreach (var obj in listOfObjects)
                        {

                            Destroy(obj.gameObject);
                        }


                    }
                    frameToLoad = 0;
                    manager.GetComponent<initializeScene>().loadGameObjects(nextScene);
                    playAnimations = true;
                    GameObject[] a = GameObject.FindGameObjectsWithTag("spawnedObject");
                    if (a.Length > 0)
                    {
                        foreach (var obj in a)
                        {

                            obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().enabled = false;
                            obj.GetComponent<BoundsControl>().enabled = false;
                        }

                    }
                    loadSceneConditions(nextScene);

                    activeScene = nextScene;
                    //currentGesture.transitionSceneId = nextScene;

                    loadProximityConditions();

                }
            }
        }



    }

    private void FixedUpdate()
    {
        if (testSessionStarted && playAnimations) loadHandAnimations();
        if (testSessionStarted && playAnimations) loadCustomAnimations();
    }



    public void startTest()
    {
        startScene = sceneDropdown.value + 1;

        testSessionStarted = false;
        headerText.SetActive(false);
        startButton.SetActive(false);
        toggleButton.SetActive(false);
        thresholdButton.SetActive(false);
        dropdownButton.SetActive(false);

        PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOff);
        gestures = new List<Gesture>();
        previousGesture = new Gesture();
        rightHandAnchor = GameObject.Find("RightHandAnchor");
        if (rightHandAnchor != null)
        {
            rightHand = rightHandAnchor.transform.Find("OVRHandPrefab_Right");
            if (rightHand != null)
            {

                skeleton = rightHand.GetComponent<OVRSkeleton>();

            }
        }
        fingerBones = new List<OVRBone>(skeleton.Bones);

        StartCoroutine(ParseScene());

        testSessionStarted = true;

    }


    private void loadSceneConditions(int sceneID)
    {
        // Debug.Log("8000 Start of load conditions, sceneId is : " + sceneID);
        List<SceneObj> scenes = FileHandler.ReadListFromJSON<SceneObj>("Root.json");
        // Debug.Log("8001 scenes count " + scenes.Count);
        List<HandPoseSensor> handPoses = FileHandler.ReadListFromJSON<HandPoseSensor>("handLibrary.json");
        // Debug.Log("8002 hand Poses count " + handPoses.Count);
        foreach (var scene in scenes)
        {
            if (scene.sceneID == sceneID)
            {
                // Debug.Log("8003 Scene id matched for " + scene.sceneID + ", " + sceneID);
                List<HandPoseConditions> handConditions = scene.handPoseConditions;
                // Debug.Log("8004 hand Conditions found: " + handConditions.Count);
                // Debug.Log("8005 hand Conditions found: " + listOfPoses.Count);

                foreach (var condition in handConditions)
                {

                    foreach (var item in listOfPoses)
                    {
                        if (condition.handPoseId == item.Id)
                        {
                            Gesture g = new Gesture();
                            g.Id = condition.handPoseId;
                            g.transitionSceneId = condition.transitionSceneId;
                            g.fingerDatas = item.f_data;

                            gestures.Add(g);
                        }
                    }




                }
            }
        }
        //Debug.Log("8008 END of load condition gesture loaded " + gestures.Count);
    }




    Gesture RecognizeStored()
    {
        Gesture currentHandPose = new Gesture();


        float currentMin = Mathf.Infinity;
        foreach (var hp in gestures)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for (int i = 0; i < fingerBones.Count; i++)
            {
                //stext.text = "we get " + fingerBones.Count + " recorded " + hp.fingerDatas.Count;
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, hp.fingerDatas[i]);
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

    IEnumerator ParseScene()
    {
        playAnimations = false;
        listOfPoses = new List<SessionPoses>();
        List<SceneObj> scenes = FileHandler.ReadListFromJSON<SceneObj>("Root.json");
        List<HandPoseSensor> handPoses = FileHandler.ReadListFromJSON<HandPoseSensor>("handLibrary.json");
        List<int> poseId = new List<int>();

        foreach (var scene in scenes)
        {
            List<HandPoseConditions> handConditions = scene.handPoseConditions;
            foreach (var condition in handConditions)
            {
                if (condition.handPoseId != -1) poseId.Add(condition.handPoseId);
            }
        }

        List<int> distinctPoses = poseId.Distinct().ToList();

        foreach (var i in distinctPoses)
        {
            foreach (var m in models)
            {
                if (m.GetComponent<handPoseId>().myHandPoseId == i)
                {
                    stext.text = "Do this Hand Pose";
                    GameObject k = Instantiate(m, container.transform);
                    //k.transform.localScale = new Vector3(2, 2, 2);

                    Vector3 temp = k.transform.localScale;
                    temp.x *= 2;
                    temp.y *= 2;
                    temp.z *= 2;

                    k.transform.localScale = temp;

                    k.transform.localPosition = k.transform.localPosition + new Vector3(0, 0, -1.0f);

                    yield return new WaitForSeconds(4f);
                    Save(i);
                    //stext.text = "out now";
                    Destroy(k.gameObject);
                }
            }
        }
        //int startScene = sceneDropdown.value + 1;
        //stext.text = "5001 Loading scene " + startScene;


        manager.GetComponent<initializeScene>().loadGameObjects(startScene);

        GameObject[] a = GameObject.FindGameObjectsWithTag("spawnedObject");
        if (a.Length > 0)
        {
            foreach (var obj in a)
            {

                obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().enabled = false;
                obj.GetComponent<BoundsControl>().enabled = false;
                //Debug.Log("9000: Scene: " + currentGesture.transitionSceneId + ", distance: " + obj.GetComponent<setRef>().distance + ", navigate to " + obj.GetComponent<setRef>().navigateTo);

            }

        }

        playAnimations = true;
        // Debug.Log("5002 Loaded scene " + startScene);
        loadSceneConditions(startScene);
        yield return new WaitForSeconds(20f);
        loadProximityConditions();

        activeScene = startScene;





        //stext.text = "number of poses recorded " + listOfPoses.Count;



    }

    void Save(int handId)
    {
        SessionPoses handPoseRight = new SessionPoses();
        //handPoseRight.Id = handId;
        List<Vector3> data = new List<Vector3>();
        //Debug.Log("Saving following data:");
        foreach (var bone in fingerBones)
        {
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));

        }



        handPoseRight.f_data = data;
        handPoseRight.Id = handId;
        listOfPoses.Add(handPoseRight);

    }

    void loadProximityConditions()
    {
        distanceToMonitor = -1;
        objectToMonitor = null;
        nextScene = -1;
        GameObject[] a = GameObject.FindGameObjectsWithTag("spawnedObject");
        if (a.Length > 0)
        {
            //Debug.Log("2000: distance: " + obj.GetComponent<setRef>().distance + "next scene: " + obj.GetComponent<setRef>().navigateTo);

            foreach (var obj in a)
            {
                if (obj.GetComponent<setRef>().navigateTo != 0)
                {
                    //Debug.Log("1000: distance: " + obj.GetComponent<setRef>().distance + "next scene: " + obj.GetComponent<setRef>().navigateTo);
                    distanceToMonitor = obj.GetComponent<setRef>().distance;
                    nextScene = obj.GetComponent<setRef>().navigateTo;
                    objectToMonitor = obj;
                    //shouldLoadScene = true;
                }


            }
        }
    }



    IEnumerator loadProximity()
    {
        distanceToMonitor = -1;
        objectToMonitor = null;
        nextScene = -1;
        GameObject[] a = GameObject.FindGameObjectsWithTag("spawnedObject");
        if (a.Length > 0)
        {
            //Debug.Log("2000: distance: " + obj.GetComponent<setRef>().distance + "next scene: " + obj.GetComponent<setRef>().navigateTo);

            foreach (var obj in a)
            {
                if (obj.GetComponent<setRef>().navigateTo != 0)
                {
                    //Debug.Log("1000: distance: " + obj.GetComponent<setRef>().distance + "next scene: " + obj.GetComponent<setRef>().navigateTo);
                    distanceToMonitor = obj.GetComponent<setRef>().distance;
                    nextScene = obj.GetComponent<setRef>().navigateTo;
                    objectToMonitor = obj;
                    //shouldLoadScene = true;
                }
                else
                {
                    distanceToMonitor = -1;
                    objectToMonitor = null;
                    nextScene = -1;
                }

            }
        }

        yield return null;

    }


    void loadHandAnimations()
    {
        //distanceToMonitor = -1;
        //objectToMonitor = null;
        GameObject[] a = GameObject.FindGameObjectsWithTag("spawnedObject");
        if (a.Length > 0)
        {
            //Debug.Log("2000: distance: " + obj.GetComponent<setRef>().distance + "next scene: " + obj.GetComponent<setRef>().navigateTo);

            foreach (var obj in a)
            {
                if (obj.GetComponent<setRef>().hAnimations.boneId > -1)
                {
                    //obj.transform.position = skeleton.Bones[obj.GetComponent<setRef>().hAnimations.boneId].Transform.position; 
                    Vector3 bonePos = skeleton.Bones[obj.GetComponent<setRef>().hAnimations.boneId].Transform.position;

                    if (obj.GetComponent<setRef>().hAnimations.followX && obj.GetComponent<setRef>().hAnimations.followY && obj.GetComponent<setRef>().hAnimations.followZ)
                    {
                        obj.transform.position = skeleton.Bones[obj.GetComponent<setRef>().hAnimations.boneId].Transform.position;
                        //obj.transform.rotation = skeleton.Bones[obj.GetComponent<setRef>().hAnimations.boneId].Transform.rotation;
                    }
                    else if (obj.GetComponent<setRef>().hAnimations.followX && obj.GetComponent<setRef>().hAnimations.followY)
                    {
                        obj.transform.position = new Vector3(bonePos.x, bonePos.y, obj.transform.position.z);
                    }
                    else if (obj.GetComponent<setRef>().hAnimations.followX && obj.GetComponent<setRef>().hAnimations.followZ)
                    {
                        obj.transform.position = new Vector3(bonePos.x, obj.transform.position.y, bonePos.z);
                    }
                    else if (obj.GetComponent<setRef>().hAnimations.followY && obj.GetComponent<setRef>().hAnimations.followZ)
                    {
                        obj.transform.position = new Vector3(obj.transform.position.x, bonePos.y, bonePos.z);
                    }

                    else if (obj.GetComponent<setRef>().hAnimations.followX)
                    {
                        obj.transform.position = new Vector3(bonePos.x, obj.transform.position.y, obj.transform.position.z);
                    }

                    else if (obj.GetComponent<setRef>().hAnimations.followY)
                    {
                        obj.transform.position = new Vector3(obj.transform.position.x, bonePos.y, obj.transform.position.z);
                    }
                    else if (obj.GetComponent<setRef>().hAnimations.followZ)
                    {
                        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, bonePos.z);
                    }

                }
            }
        }
    }



    void loadCustomAnimations()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("spawnedObject");
        if (a.Length > 0)
        {
            foreach (var obj in a)
            {
                if (obj.GetComponent<setRef>().cAnimations.Count > 0 && frameToLoad < obj.GetComponent<setRef>().cAnimations.Count)
                {
                    SetTransform(obj, frameToLoad);
                    frameToLoad++;
                }
            }
        }
    }

    private void SetTransform(GameObject ob, int index)
    {
        if (index < ob.GetComponent<setRef>().cAnimations.Count)
        {
            CustomAnimation c = ob.GetComponent<setRef>().cAnimations[index];
            ob.transform.position = c.position;
            ob.transform.rotation = c.rotation;
        }

    }
}
