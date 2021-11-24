using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenu;
    private bool isMainMenuEnabled = false;
    private OVRHand ovrHandRight;
    private OVRHand ovrHandLeft;
    // Start is called before the first frame update
    void Start()
    {
        GameObject rightHandAnchor = GameObject.Find("RightHandAnchor");
        if (rightHandAnchor != null)
        {
            Transform rightHand = rightHandAnchor.transform.Find("OVRHandPrefab_Right");
            if (rightHand != null)
            {
                ovrHandRight = rightHand.GetComponent<OVRHand>();
            }
        }
        else
            Debug.Log("Not Found Right Hand");



        GameObject leftHandAnchor = GameObject.Find("LeftHandAnchor");
        if (leftHandAnchor != null)
        {
            Transform leftHand = leftHandAnchor.transform.Find("OVRHandPrefab_Left");
            if (leftHand != null)
            {
                ovrHandLeft = leftHand.GetComponent<OVRHand>();
            }
        }
        else
            Debug.Log("Not Found Left Hand");
    }

    // Update is called once per frame
    void Update()
    {
        if (checkPinchState())
        {
            if(isMainMenuEnabled == true)
            {
                mainMenu.SetActive(false);
                isMainMenuEnabled = false;
            }
            else
            {
                mainMenu.SetActive(true);
                isMainMenuEnabled = true;
            }
            
        }
       
    }

    bool checkPinchState()
    {
        bool isIndexFingerPinchingLeft = ovrHandLeft.GetFingerIsPinching(OVRHand.HandFinger.Index);
        float leftIndexFingerPinchStrength = ovrHandLeft.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        bool isIndexFingerPinchingRight = ovrHandRight.GetFingerIsPinching(OVRHand.HandFinger.Index);
        float rightIndexFingerPinchStrength = ovrHandRight.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        if (isIndexFingerPinchingLeft && isIndexFingerPinchingRight && leftIndexFingerPinchStrength > 0.9 && rightIndexFingerPinchStrength > 0.9)
        {
            return true;
        }
        return false;
    }
}
