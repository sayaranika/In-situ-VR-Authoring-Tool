using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionOptionClicked : MonoBehaviour
{
    [SerializeField] private GameObject designOption;
    [SerializeField] private GameObject testOption;

    [SerializeField] private GameObject DesignUI;
    [SerializeField] private GameObject InteractionUI;

    public bool isInteractionSpaceEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        InteractionUI.SetActive(false);
        GetComponent<Interactable>().OnClick.AddListener(OpenInteractionSpace);
    }

    private void OpenInteractionSpace()
    {
        designOption.GetComponent<DesignOptionClicked>().isDesignSpaceSelected = false;
        testOption.GetComponent<TestOptionClicked>().isTestSpaceSelected = false; 
        if(isInteractionSpaceEnabled == false)
        {
            DesignUI.SetActive(false);
            InteractionUI.SetActive(true);
            isInteractionSpaceEnabled = true;
        }

    }
}
