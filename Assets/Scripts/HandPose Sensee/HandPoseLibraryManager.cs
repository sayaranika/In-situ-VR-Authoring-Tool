using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPoseLibraryManager : MonoBehaviour
{
    [SerializeField] private GameObject glow;
    [SerializeField] private GameObject handMenu;
    [SerializeField] private GameObject menu;

    //[SerializeField] private GameObject recordingOptions;

    [SerializeField] private GameObject tab;

    [SerializeField] private GameObject builderPanel;
    [SerializeField] private GameObject triggerPanel;
    //[SerializeField] private GameObject testPanel;

    public void instantiateSelectedHand(int handPoseId)
    {
        Vector3 spawnPosition = glow.transform.position + new Vector3(0,1,0);
        Quaternion rotation = Quaternion.identity;
        GameObject handSensee = Instantiate(this.gameObject);
        handSensee.transform.parent = menu.transform;
        handSensee.transform.localPosition = new Vector3(0,0,0);
        handSensee.transform.localScale = new Vector3(3,3,3);
        //handMenu.transform.position = spawnPosition;

        tab.SetActive(false);
        builderPanel.SetActive(false);
        triggerPanel.SetActive(false);
        //testPanel.SetActive(false);
        glow.SetActive(false);

        handMenu.transform.localPosition = new Vector3(0.3f, -1.3f, 0); 
    }
}
