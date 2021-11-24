using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RightHandButtonPressed : MonoBehaviour
{

    [SerializeField]
    private GameObject captureSession;
    
    void Start()
    {
        GetComponent<Interactable>().OnClick.AddListener(HideMenu);
    }

    void HideMenu()
    {
        captureSession.SetActive(true);


    }


    
}
