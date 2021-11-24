using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateBoundsOnSelection : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private int i = 0;
    //[SerializeField] private GameObject objContainer;
    //int a = 0;
    public void deactivateOtherMenus()
    {
        if(i==0)
        {
            gameObject.transform.parent = null;
        }
        Debug.Log("Called");
        GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");
        foreach (var o in listOfObjects)
        {
            if (ReferenceEquals(o, obj))
            {
                o.transform.Find("controlBar").gameObject.SetActive(true);
            }
            else
                o.transform.Find("controlBar").gameObject.SetActive(false);
        }
    }




}
