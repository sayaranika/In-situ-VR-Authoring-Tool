using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using TMPro;
using UnityEngine.UI;

public class attachingSensees : MonoBehaviour
{
    private int attach = 0;
    private GameObject attachedSensee;
    [SerializeField]
    private GameObject LabelContainer;
    private GameObject labelContainer;
    private Transform label;

    [SerializeField] private Text displayText;

    private float distance;
    private int showDist = 0;
    private int inst = 0;

    private int objId = 1000;
    private Vector3 senseePos;
    private OVRSkeleton ovrSkeletonRight;
    //private OVRHand ovrHandRight;

    private void Start()
    {
        LabelContainer.SetActive(false);
        GameObject rightHandAnchor = GameObject.Find("RightHandAnchor");
        if (rightHandAnchor != null)
        {
            Transform rightHand = rightHandAnchor.transform.Find("OVRHandPrefab_Right");
            if (rightHand != null)
            {
                ovrSkeletonRight = rightHand.GetComponent<OVRSkeleton>();
            }
        }
        else
            Debug.Log("Not Found Right Hand");
    }

    private void Update()
    {
        if(inst == 0 && showDist == 1)
        {
            //labelContainer = GameObject.Instantiate(LabelContainer);
            //label = LabelContainer.transform.Find("Title");
            //label.GetComponent<TextMeshPro>().text = "";
            LabelContainer.SetActive(true);
            inst++;
        }
        if (showDist == 1)
        {
            Vector3 bonePos = ovrSkeletonRight.Bones[(int)OVRPlugin.BoneId.Hand_Index1].Transform.position;
            
            GameObject[] a = GameObject.FindGameObjectsWithTag("pointSensee");
            foreach(var sensee in a)
            {
                if(sensee.GetInstanceID() != objId)
                {
                    senseePos = sensee.transform.position;
                }
            }
            distance = Vector3.Distance(bonePos, senseePos);
            Debug.Log("distance is " + distance.ToString());
            displayText.text = "Distance: " + distance.ToString();


        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("pointSensee"))
        {
            attachedSensee = collision.gameObject;
            Debug.Log("Dtected trigger");
            //attachedSensee.transform.position

            
            objId = attachedSensee.GetInstanceID();
            attach = 1;
            showDist = 1;

            //LabelContainer.text = "Proximity: ";



        }
    }

    private void OnTriggerExit(Collider collision)
    {
        attach = 0;
        //attachedSensee.transform.position = gameObject.transform.position + new Vector3(0.2f, 0, 0);
        Debug.Log("Exiting trigger");
        
    }

    

    private void FixedUpdate()
    {
        if (attach == 1 && attachedSensee != null)
        {
            //attachedSensee.transform.position = gameObject.transform.position;
        }
    }

}
