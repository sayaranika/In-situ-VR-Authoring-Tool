using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsButtonPressed : MonoBehaviour
{
    [SerializeField]
    private GameObject shapesObjList;
    [SerializeField]
    private GameObject assetsObjList;
    [SerializeField]
    private GameObject scenesObjList;

    void Start()
    {
        GetComponent<Interactable>().OnClick.AddListener(SetDesignPanelActive);
    }

    public void SetDesignPanelActive()
    {
        shapesObjList.SetActive(false);
        assetsObjList.SetActive(true);
        scenesObjList.SetActive(false);
    }
}
