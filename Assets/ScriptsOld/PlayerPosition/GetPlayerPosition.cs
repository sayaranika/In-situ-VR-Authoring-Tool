using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerPosition : MonoBehaviour
{
    private GameObject leftHandAnchor;
    private Transform leftHand;

    private OVRHand ovrHandLeft;
    private OVRSkeleton ovrSkeletonLeft;

    private Vector3 positionOfIndex;
    private Vector3 positionOfWrist;
    int found = 0;
    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log("Not Found Left Hand");
    }

    // Update is called once per frame
    void Update()
    {
        if(ovrHandLeft != null)
        {
            positionOfIndex = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Index1].Transform.position;
            positionOfWrist = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.position;

            Debug.Log("Position of index is " + positionOfIndex + "Position of wrist is " + positionOfWrist);

            //positionOfIndex = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_Index1].Transform.localPosition;
            //positionOfWrist = ovrSkeletonLeft.Bones[(int)OVRPlugin.BoneId.Hand_WristRoot].Transform.localPosition;

            //Debug.Log("Local Position of index is " + positionOfIndex + "Local Position of wrist is " + positionOfWrist);
        }
    }
}
