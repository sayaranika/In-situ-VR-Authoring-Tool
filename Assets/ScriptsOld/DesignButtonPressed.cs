using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignButtonPressed : MonoBehaviour
{
    [SerializeField]
    private GameObject designerObjList;
    [SerializeField]
    private GameObject triggerObjList;

    // Start is called before the first frame update
    void Start()
    {
        triggerObjList.SetActive(false);
        GetComponent<Interactable>().OnClick.AddListener(SetDesignPanelActive);
    }

    public void SetDesignPanelActive()
    {
        designerObjList.SetActive(true);
        triggerObjList.SetActive(false);
    } 
}
