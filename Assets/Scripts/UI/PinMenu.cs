using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    private bool isPinned = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>().OnClick.AddListener(togglePin);
    }

    public void togglePin()
    {
        if(isPinned == true)
        {
            isPinned = false;
            menu.GetComponent<RadialView>().enabled = true;
        }
        else
        {
            isPinned = true;
            menu.GetComponent<RadialView>().enabled = false;
        }
    }
}
