using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using SpaceBear.VRUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadViews : MonoBehaviour
{
    public bool isSceneBuilderActive = true;
    public bool isTriggerEditorActive = false;
    public bool isTestActive = false;

    public bool isSceneLoaded = false;
    public int currentScene = 1; // 1 for scene, 2 for trigger, 3 for test

    [SerializeField] private GameObject sceneMenu;
    [SerializeField] private GameObject triggerMenu;
    [SerializeField] private GameObject testMenu;
    [SerializeField] private GameObject propertiesMenu;
    [SerializeField] private VRUITabButton sceneButton;

    [SerializeField] private GameObject handPosePanelManager;

    [SerializeField] private TMPro.TMP_Dropdown sceneDropdown;

    [SerializeField] private GameObject testHandler;

    
    void Start()
    {
        sceneButton.Select();
        isSceneBuilderActive = true;
        isSceneLoaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //load appropriate views based on data
        if(isSceneBuilderActive && isSceneLoaded == false)
        {
            if(currentScene == 2)
            {
                //save trigger data
                handPosePanelManager.GetComponent<handPosePanelManager>().saveHandPoseConditionData();
                handPosePanelManager.GetComponent<handPosePanelManager>().resetAll();
                handPosePanelManager.GetComponent<handPosePanelManager>().deactivateHandPanel();

                gameObject.GetComponent<VisibilityOfOptions>().deactivateDistancePanel();
            }
            if(currentScene == 3)
            {
                testHandler.GetComponent<GestureRecognition>().testSessionStarted = false;
                GameObject[] a = GameObject.FindGameObjectsWithTag("spawnedObject");
                if (a.Length > 0)
                {
                    foreach (var obj in a)
                    {
                        Destroy(obj);
                    }
                }
            }
            currentScene = 1; 


            //load objects 
            //make them editable
            //disappear triggers

            sceneMenu.SetActive(true);
            propertiesMenu.SetActive(true);
            triggerMenu.SetActive(false);
            testMenu.SetActive(false);
            //propertiesMenu.SetActive(true);
            isSceneLoaded = true;

            GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");
            if (listOfObjects.Length > 0)
            {
                foreach (var obj in listOfObjects)
                {
                    
                    obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().enabled = true;
                    obj.GetComponent<BoundsControl>().UnhighlightWires();
                    obj.GetComponent<BoundsControl>().ScaleHandlesConfig.ShowScaleHandles = true;
                    obj.GetComponent<RotationAxisConstraint>().enabled = false;
                    Debug.Log("Length: " + listOfObjects.Length);
                }
            }
            else
            {
                int sceneId = gameObject.GetComponent<initializeScene>().currentScene;
                gameObject.GetComponent<initializeScene>().loadGameObjects(sceneId);
                GameObject[] _listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");

                if(_listOfObjects.Length >0)
                {
                    foreach (var obj in _listOfObjects)
                    {

                        obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().enabled = true;
                        obj.GetComponent<BoundsControl>().UnhighlightWires();
                        obj.GetComponent<BoundsControl>().ScaleHandlesConfig.ShowScaleHandles = true;
                        obj.GetComponent<RotationAxisConstraint>().enabled = false;
                        Debug.Log("Length: " + listOfObjects.Length);
                    }
                }
                
            }

        }
        else if(isTriggerEditorActive && isSceneLoaded == false)
        {
            if(currentScene == 1)
            {
                //save scene
                gameObject.GetComponent<initializeScene>().saveGameObjects(gameObject.GetComponent<initializeScene>().currentScene);
            }
            if (currentScene == 3)
            {
                testHandler.GetComponent<GestureRecognition>().testSessionStarted = false;
                GameObject[] a = GameObject.FindGameObjectsWithTag("spawnedObject");
                if (a.Length > 0)
                {
                    foreach (var obj in a)
                    {
                        Destroy(obj);
                    }
                }
            }
            currentScene = 2;
            handPosePanelManager.GetComponent<handPosePanelManager>().resetAll();
            handPosePanelManager.GetComponent<handPosePanelManager>().loadSceneHPConditions();
            //handPosePanelManager.GetComponent<handPosePanelManager>().activateHandPanel();
            //load objects 
            //make them not editable
            //enable triggers

            sceneMenu.SetActive(false);
            propertiesMenu.SetActive(false);
            triggerMenu.SetActive(true);
            testMenu.SetActive(false);
            //propertiesMenu.SetActive(false);
            isSceneLoaded = true;

            //gameObject.GetComponent<initializeScene>().saveGameObjects(gameObject.GetComponent<initializeScene>().currentScene);
            int sceneId = gameObject.GetComponent<initializeScene>().currentScene;
            gameObject.GetComponent<initializeScene>().loadGameObjects(sceneId);
            GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");
            if(listOfObjects.Length > 0)
            {
                foreach (var obj in listOfObjects)
                {
                    
                    obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().enabled = false;
                    obj.GetComponent<BoundsControl>().HighlightWires();
                    obj.GetComponent<BoundsControl>().ScaleHandlesConfig.ShowScaleHandles = false ;
                    obj.GetComponent<RotationAxisConstraint>().enabled = true;
                    Debug.Log("Length: " + listOfObjects.Length);

                }

            }
            else
            {
                sceneId = gameObject.GetComponent<initializeScene>().currentScene;
                gameObject.GetComponent<initializeScene>().loadGameObjects(sceneId);
                GameObject[] _listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");

                if(_listOfObjects.Length > 0)
                {
                    foreach (var obj in _listOfObjects)
                    {

                        obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().enabled = false;
                        obj.GetComponent<BoundsControl>().HighlightWires();
                        obj.GetComponent<BoundsControl>().ScaleHandlesConfig.ShowScaleHandles = false;
                        obj.GetComponent<RotationAxisConstraint>().enabled = true;
                        Debug.Log("Length: " + listOfObjects.Length);

                    }
                }
                
            }

            
        }
        else if (isTestActive && isSceneLoaded == false)
        {
            if(currentScene == 1)
            {
                //save scene
                gameObject.GetComponent<initializeScene>().saveGameObjects(gameObject.GetComponent<initializeScene>().currentScene);
            }
            if(currentScene == 2)
            {
                //save trigger
                handPosePanelManager.GetComponent<handPosePanelManager>().saveHandPoseConditionData();
                handPosePanelManager.GetComponent<handPosePanelManager>().resetAll();
                handPosePanelManager.GetComponent<handPosePanelManager>().deactivateHandPanel();
            }
            currentScene = 3;
            //handPosePanelManager.GetComponent<handPosePanelManager>().activateHandPanel();
            sceneMenu.SetActive(false);
            propertiesMenu.SetActive(false);
            triggerMenu.SetActive(false);
            testMenu.SetActive(true);
            //propertiesMenu.SetActive(true);
            isSceneLoaded = true;
            Debug.Log("Called");

            gameObject.GetComponent<initializeScene>().saveGameObjects(gameObject.GetComponent<initializeScene>().currentScene);
            GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");
            if (listOfObjects.Length > 0)
            {
                foreach (var obj in listOfObjects)
                {

                    Destroy(obj);
                }


            }


            //load dropdown list
            handPosePanelManager.GetComponent<handPosePanelManager>().PopulateDropdown(sceneDropdown);

        }

    }

    public void activateSceneBuilder()
    {
        //handPosePanelManager.GetComponent<handPosePanelManager>().saveHandPoseConditionData();
        isSceneBuilderActive = true;
        isTriggerEditorActive = false;
        isTestActive = false;
        isSceneLoaded = false;
    }

    public void activateTriggerEditor()
    {
        isSceneBuilderActive = false;
        isTriggerEditorActive = true;
        isTestActive = false;
        isSceneLoaded = false;
    }

    public void activateTest()
    {
        //handPosePanelManager.GetComponent<handPosePanelManager>().saveHandPoseConditionData();
        //handPosePanelManager.GetComponent<handPosePanelManager>().deactivateHandPanel();
        isSceneBuilderActive = false;
        isTriggerEditorActive = false;
        isTestActive = true;
        isSceneLoaded = false;
    }
}
