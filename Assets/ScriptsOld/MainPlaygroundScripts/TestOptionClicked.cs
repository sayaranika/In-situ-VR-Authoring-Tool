using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOptionClicked : MonoBehaviour
{
    [SerializeField] private GameObject designOption;
    [SerializeField] private GameObject interactionOption;

    [SerializeField] private GameObject DesignUI;
    [SerializeField] private GameObject InteractionUI;
    public bool isTestSpaceSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>().OnClick.AddListener(OpenTestSpace);
    }

    private void OpenTestSpace()
    {
        designOption.GetComponent<DesignOptionClicked>().isDesignSpaceSelected = false;
        interactionOption.GetComponent<InteractionOptionClicked>().isInteractionSpaceEnabled = false;
        if (isTestSpaceSelected == false)
        {
            DesignUI.SetActive(false);
            InteractionUI.SetActive(false);
            isTestSpaceSelected = true;
        }

    }
}
