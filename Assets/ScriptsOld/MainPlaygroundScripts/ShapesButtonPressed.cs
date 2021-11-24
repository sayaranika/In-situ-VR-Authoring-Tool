using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesButtonPressed : MonoBehaviour
{
    [SerializeField]
    private GameObject shapesObjList;
    [SerializeField]
    private GameObject assetsObjList;
    [SerializeField]
    private GameObject scenesObjList;
    // Start is called before the first frame update
    void Start()
    {
        assetsObjList.SetActive(false);
        scenesObjList.SetActive(false);
        GetComponent<Interactable>().OnClick.AddListener(SetDesignPanelActive);
    }

    public void SetDesignPanelActive()
    {
        shapesObjList.SetActive(true);
        assetsObjList.SetActive(false);
        scenesObjList.SetActive(false);
    }
}
