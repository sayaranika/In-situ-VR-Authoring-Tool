using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityOfOptions : MonoBehaviour
{
    [SerializeField] private GameObject recordingOptions;

    [SerializeField] private GameObject statusPanel;

    [SerializeField] private GameObject tab;

    [SerializeField] private GameObject builderPanel;
    [SerializeField] private GameObject triggerPanel;
    [SerializeField] private GameObject testPanel;

    [SerializeField] private GameObject captureSession;
    [SerializeField] private GameObject leftHandSession;

    [SerializeField] private GameObject proximityPanel;
    [SerializeField] private GameObject proximityManager;
    //[SerializeField] private GameObject testPanel;

    [SerializeField] private GameObject glow;

    [SerializeField] private TMPro.TMP_Dropdown pDropdown;


    // Start is called before the first frame update
    void Start()
    {
        recordingOptions.SetActive(false);
        statusPanel.SetActive(false);
        deactivateDistancePanel();
    }

    public void setActiveRecordingOptions()
    {
        recordingOptions.SetActive(true);

    }

    public void setInactiveRecordingOptions()
    {
        recordingOptions.SetActive(false);
    }

    public void captureRightHand()
    {
        setInactiveRecordingOptions();
        tab.SetActive(false);
        builderPanel.SetActive(false);
        triggerPanel.SetActive(false);
        testPanel.SetActive(false);
        glow.SetActive(false);
        statusPanel.SetActive(true);
        captureSession.SetActive(true);
    }

    public void captureLeftHand()
    {
        setInactiveRecordingOptions();
        tab.SetActive(false);
        builderPanel.SetActive(false);
        triggerPanel.SetActive(false);
        testPanel.SetActive(false);
        glow.SetActive(false);
        statusPanel.SetActive(true);
        leftHandSession.SetActive(true);
    }

    public void setInactiveStatusPanel()
    {
        statusPanel.SetActive(false);
        tab.SetActive(true);
        builderPanel.SetActive(true);
        triggerPanel.SetActive(true);
        glow.SetActive(true);
       // testPanel.SetActive(true);

    }

    public void activateDistancePanel()
    {
        proximityManager.GetComponent<ProximityManager>().isPanelOpen = true;
        PopulateDropdownWithSceneList(pDropdown);

        if (proximityPanel.transform.position.y > 200)
        {


            GameObject playerAnchor = GameObject.Find("CenterEyeAnchor");
            if (playerAnchor != null)
            {
                proximityPanel.transform.position = playerAnchor.transform.position + new Vector3(0, 0, 1.5f);
            }
            else
            {
                proximityPanel.transform.position = new Vector3(0, 0.8114f, 1.0044f);
            }
            proximityPanel.transform.rotation = Quaternion.identity;
        }

        
        //proximityPanel.GetComponent<RadialView>().enabled = true;

        //handMenu.AddComponent<>

    }

    public void deactivateDistancePanel()
    {
        proximityManager.GetComponent<ProximityManager>().isPanelOpen = false;
        //proximityPanel.GetComponent<RadialView>().enabled = false;
        proximityPanel.transform.position = new Vector3(0, 300, 0);
    }

    public void PopulateDropdownWithSceneList(TMPro.TMP_Dropdown dropdown)
    {
        List<string> options = new List<string>();

        List<SceneObj> sceneEntries = FileHandler.ReadListFromJSON<SceneObj>("Root.json");
        int totalScenes = sceneEntries.Count;

        for (int i = 1; i <= totalScenes; i++)
        {
            options.Add(i.ToString());
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }
}
