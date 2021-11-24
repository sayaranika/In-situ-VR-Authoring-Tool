using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using SpaceBear.VRUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openColorPalette : MonoBehaviour
{
    [SerializeField] private GameObject colorPalette;
    [SerializeField] private FlexibleColorPicker colors;
    [SerializeField] private GameObject propertiesMenu;
    [SerializeField] private GameObject placeholder;

    [SerializeField] private GameObject animationPanelTitle;
    [SerializeField] private GameObject animationCustom;
    [SerializeField] private GameObject animationHands;


    [SerializeField] private GameObject startRecordButton;
    [SerializeField] private GameObject stopRecordButton;
    [SerializeField] private GameObject saveRecordButton;
    [SerializeField] private GameObject cancelRecordButton;

    [SerializeField] private VRUIRadio radio1;

    [SerializeField] private VRUIRadio radio2;

    [SerializeField] private VRUICheckbox isX;
    [SerializeField] private VRUICheckbox isY;
    [SerializeField] private VRUICheckbox isZ;

    private Vector3 previousPosition;
    private Quaternion previousRotation;

    private bool isRecorded = false;
    private List<CustomAnimation> customAnimations;

    GameObject ObjectBeingRecorded;
    public void openColor()
    {
        GameObject obj = placeholder.GetComponent<SceneHandler>().referenceObj;
        colors.color = obj.GetComponent<Renderer>().material.color;
        colorPalette.SetActive(true);
        propertiesMenu.SetActive(false);
        //propertiesMenu.transform.position = new Vector3(0,200,0);
    }

    public void closePropertiesPanel()
    {
        propertiesMenu.SetActive(false);
    }

    public void openAnimationPanel()
    {
        //animationPanel.SetActive(true);
        // animationPanel.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        //animationPanel.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);

        GameObject playerAnchor = GameObject.Find("CenterEyeAnchor");
        if (playerAnchor != null)
        {
            animationPanelTitle.transform.position = playerAnchor.transform.position + new Vector3(0, 1, 3);
        }
        animationPanelTitle.transform.LookAt(playerAnchor.transform);
        animationPanelTitle.transform.Rotate(0, 180, 0);
        //animationPanelTitle.transform.rotation = Quaternion.identity;


        animationCustom.transform.position = new Vector3(0, 1000, 0);
        animationHands.transform.position = new Vector3(0, 1000, 0);

        //animationParent.GetComponent<RadialView>().enabled = true;


        propertiesMenu.SetActive(false);

        /*GameObject[] a = GameObject.FindGameObjectsWithTag("spawnedObject");
        if (a.Length > 0)
        {
            foreach (var obj in a)
            {
                if(obj != placeholder.GetComponent<SceneHandler>().referenceObj)

                obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().enabled = false;
                obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().TwoHandedManipulationType = Microsoft.MixedReality.Toolkit.Utilities.TransformFlags.Move;
                obj.GetComponent<BoundsControl>().enabled = false;

            }
        }*/

        ObjectBeingRecorded = placeholder.GetComponent<SceneHandler>().referenceObj;
        placeholder.GetComponent<SceneHandler>().referenceObj = null;

        Debug.Log("1000B: Object is " + ObjectBeingRecorded.name);


    }

    public void closeAnimationPanel()
    {
        //animationParent.GetComponent<RadialView>().enabled = false;

        animationPanelTitle.transform.position = new Vector3(0, 1000, 0);
        animationCustom.transform.position = new Vector3(0, 1000, 0);
        animationHands.transform.position = new Vector3(0, 1000, 0);




        propertiesMenu.SetActive(true);
        placeholder.GetComponent<SceneHandler>().referenceObj = ObjectBeingRecorded;

        /*GameObject[] a = GameObject.FindGameObjectsWithTag("spawnedObject");
        if (a.Length > 0)
        {
            foreach (var obj in a)
            {
                if (obj != placeholder.GetComponent<SceneHandler>().referenceObj)

                    obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().enabled = true;
                obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().TwoHandedManipulationType = Microsoft.MixedReality.Toolkit.Utilities.TransformFlags.Move;
                obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().TwoHandedManipulationType = Microsoft.MixedReality.Toolkit.Utilities.TransformFlags.Rotate;
                obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().TwoHandedManipulationType = Microsoft.MixedReality.Toolkit.Utilities.TransformFlags.Scale;
                obj.GetComponent<BoundsControl>().enabled = true;

            }
        }*/
    }

    public void enableOptionOneAnimationPanel()
    {
        Debug.Log("1000E: Object is " + ObjectBeingRecorded.name);
        radio2.isOn = false;

        GameObject playerAnchor = GameObject.Find("CenterEyeAnchor");
        if (playerAnchor != null)
        {
            animationHands.transform.position = playerAnchor.transform.position + new Vector3(0, 1, 3);
        }
        
        animationHands.transform.rotation = Quaternion.identity;

        
        animationCustom.transform.position = new Vector3(0, 1000, 0);
        animationPanelTitle.transform.position = new Vector3(0, 1000, 0);

        //animationPanel.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        //animationPanel.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
    }

    public void enableOptionTwoAnimationPanel()
    {
        Debug.Log("1000D: Object is " + ObjectBeingRecorded.name);
        radio1.isOn = false;
        startRecordButton.SetActive(true);
        stopRecordButton.SetActive(false);
        saveRecordButton.SetActive(false);
        //cancelRecordButton.SetActive(true);


        GameObject playerAnchor = GameObject.Find("CenterEyeAnchor");
        if (playerAnchor != null)
        {
            animationCustom.transform.position = playerAnchor.transform.position + new Vector3(0, 1,3);
        }
       
       
        animationCustom.transform.rotation = Quaternion.identity;


        animationHands.transform.position = new Vector3(0, 1000, 0);
        animationPanelTitle.transform.position = new Vector3(0, 1000, 0);
    }


    public void saveHandAnimation()
    {


        int boneId = placeholder.GetComponent<initializeScene>().selectedBone;



        Debug.Log("1000A: " + isX.isOn.ToString() + isY.isOn.ToString() + isZ.isOn.ToString() + boneId.ToString());
        HandsAnimation hAnimation = new HandsAnimation(boneId, isX.isOn, isY.isOn, isZ.isOn);
        
        ObjectBeingRecorded.GetComponent<setRef>().hAnimations = hAnimation;

        placeholder.GetComponent<initializeScene>().saveGameObjects(placeholder.GetComponent<initializeScene>().currentScene);
        placeholder.GetComponent<initializeScene>().loadGameObjects(placeholder.GetComponent<initializeScene>().currentScene);
        closeAnimationPanel();


        
    }

    public void recordStart()
    {
        startRecordButton.SetActive(false);
        stopRecordButton.SetActive(true);
        saveRecordButton.SetActive(false);
        customAnimations = new List<CustomAnimation>();
        isRecorded = true;
    }

    public void recordStop()
    {
        startRecordButton.SetActive(false);
        stopRecordButton.SetActive(false);
        saveRecordButton.SetActive(true);
        isRecorded = false;
    }

    public void saveCustomAnimation()
    {
        startRecordButton.SetActive(true);
        stopRecordButton.SetActive(false);
        saveRecordButton.SetActive(false);
        Debug.Log("1000C: Object is " + ObjectBeingRecorded.name);
        ObjectBeingRecorded.GetComponent<setRef>().cAnimations = customAnimations;

        placeholder.GetComponent<initializeScene>().saveGameObjects(placeholder.GetComponent<initializeScene>().currentScene);
        placeholder.GetComponent<initializeScene>().loadGameObjects(placeholder.GetComponent<initializeScene>().currentScene);
        SetTransform(0);
        closeAnimationPanel();
    }


    private void FixedUpdate()
    {

        if(isRecorded)
        {
           Vector3 currentPosition = ObjectBeingRecorded.transform.position;
           Quaternion currentRotation = ObjectBeingRecorded.transform.rotation;
           if(previousPosition != currentPosition || previousRotation != currentRotation)
            {
                customAnimations.Add(new CustomAnimation(ObjectBeingRecorded.transform.position, ObjectBeingRecorded.transform.rotation));
                previousPosition = currentPosition;
                previousRotation = currentRotation;
            }
            
        }
    }

    private void SetTransform(int index)
    {
        CustomAnimation c = customAnimations[index];
        ObjectBeingRecorded.transform.position = c.position;
        ObjectBeingRecorded.transform.rotation = c.rotation;
    }

}

