using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignOptionClicked : MonoBehaviour
{
    [SerializeField] private GameObject interactionOption;
    [SerializeField] private GameObject testOption;

    public bool isDesignSpaceSelected = false;

    [SerializeField]
    private GameObject DesignUI;
    [SerializeField] private GameObject InteractionUI;
    void Start()
    {
        DesignUI.SetActive(false);
        GetComponent<Interactable>().OnClick.AddListener(OpenDesignSpace);
    }

    private void OpenDesignSpace()
    {
        interactionOption.GetComponent<InteractionOptionClicked>().isInteractionSpaceEnabled = false;
        testOption.GetComponent<TestOptionClicked>().isTestSpaceSelected = false;
        if (isDesignSpaceSelected == false)
        {
            DesignUI.SetActive(true);
            InteractionUI.SetActive(false);
            isDesignSpaceSelected = true;
        }
    }
}
