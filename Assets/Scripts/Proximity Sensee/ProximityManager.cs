using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProximityManager : MonoBehaviour
{
    [SerializeField] private GameObject sensee1;
    [SerializeField] private GameObject sensee2;
    [SerializeField] private GameObject proximityPanel;
    [SerializeField] private GameObject manager;

    [SerializeField] private TMPro.TMP_Dropdown pDropdown;

    [SerializeField] private Text distanceValText;
    private OVRSkeleton ovrSkeletonRight;

    private GameObject attachedObj;
    private int attachedBone;

    private int isInitialized = -1;
    private bool isPressed = false;
    private bool isValSet = false;

    public bool isPanelOpen = false;

    float distance;
    float distanceToSet;
    int navigationSceneId;
    // Start is called before the first frame update
    void Start()
    {
        attachedObj = null;
        attachedBone = -1;
        isPressed = false;

        isPanelOpen = false;
        isValSet = false;
        initializeHand();
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();

        if(isPanelOpen)
        {
            if (isInitialized == -1) initializeHand();

            isPressed = OVRInput.Get(OVRInput.Button.One);
            if (isPressed == true && attachedBone != -1 && attachedObj != null)
            {
                distanceToSet = distance;
                distanceValText.text = String.Format("{0:0.00}", distance);
                isValSet = true;

                
            }

            
            if(isValSet == false)
            {
                if (sensee1.GetComponent<detectAttachedObject>().attachedObject != null)
                {
                    if (sensee2.GetComponent<detectAttachedObject>().attachedBoneId != -1)
                    {
                        attachedObj = sensee1.GetComponent<detectAttachedObject>().attachedObject;
                        attachedBone = sensee2.GetComponent<detectAttachedObject>().attachedBoneId;

                        Vector3 bonePos = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index1].Transform.position;
                        distance = Vector3.Distance(attachedObj.transform.position, bonePos);
                    }
                    distanceValText.text = String.Format("{0:0.00}", distance);

                }

                else if (sensee2.GetComponent<detectAttachedObject>().attachedObject != null)
                {
                    if (sensee1.GetComponent<detectAttachedObject>().attachedBoneId != -1)
                    {
                        attachedObj = sensee2.GetComponent<detectAttachedObject>().attachedObject;
                        attachedBone = sensee1.GetComponent<detectAttachedObject>().attachedBoneId;

                        Vector3 bonePos = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index1].Transform.position;
                        distance = Vector3.Distance(attachedObj.transform.position, bonePos);
                        
                        
                    }
                    
                    distanceValText.text = String.Format("{0:0.00}", distance);
                }
                else
                {
                    distance = -1;
                    distanceValText.text = "value";
                }
            }
            

            

        }
        
       

        
    }


    public void saveCondition()
    {
        attachedObj.GetComponent<setRef>().distance = distance;
        attachedObj.GetComponent<setRef>().navigateTo = pDropdown.value + 1 ;

        //manager.GetComponent<initializeScene>().saveGameObjects(manager.GetComponent<initializeScene>().currentScene);
        Debug.Log("Saving Conditions");
        manager.GetComponent<initializeScene>().saveGameObjects(manager.GetComponent<initializeScene>().currentScene);
        manager.GetComponent<initializeScene>().loadGameObjects(manager.GetComponent<initializeScene>().currentScene);

        attachedObj = null;
        attachedBone = -1;
        isPressed = false;

        isPanelOpen = false;
        isValSet = false;

        proximityPanel.transform.position = new Vector3(1.048f, -1.833168f, 1.0044f);
        sensee1.transform.localPosition = new Vector3(0, 0, 0);
        sensee2.transform.localPosition = new Vector3(0.19f, 0, 0);

        manager.GetComponent<VisibilityOfOptions>().deactivateDistancePanel();
    }

    public void dropdownValChanged()
    {
        navigationSceneId = pDropdown.value + 1;
    }
        
    

    private void FixedUpdate()
    {
        OVRInput.FixedUpdate();
    }

    private void initializeHand()
    {
        GameObject rightHandAnchor = GameObject.Find("RightHandAnchor");
        if (rightHandAnchor != null)
        {
            Transform rightHand = rightHandAnchor.transform.Find("OVRHandPrefab_Right");
            if (rightHand != null)
            {
                ovrSkeletonRight = rightHand.GetComponent<OVRSkeleton>();
                isInitialized = 1;
            }
        }
        else
            isInitialized = -1;
    }
}
