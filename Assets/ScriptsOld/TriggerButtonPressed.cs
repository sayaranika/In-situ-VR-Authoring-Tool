using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButtonPressed : MonoBehaviour
{
    [SerializeField]
    private GameObject designerObjList;
    [SerializeField]
    private GameObject triggerObjList;
    
    void Start()
    {
        triggerObjList.SetActive(false);
        GetComponent<Interactable>().OnClick.AddListener(SetTriggerPanelActive);
    }

    public void SetTriggerPanelActive()
    {
        designerObjList.SetActive(false);
       
        triggerObjList.SetActive(true);
    }
}
