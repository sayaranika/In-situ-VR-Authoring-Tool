using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class handPosePanelManager : MonoBehaviour
{
    [SerializeField] private GameObject handMenu;
    [SerializeField] private GameObject manager;

    [SerializeField] private GameObject conditionOne;
    [SerializeField] private GameObject conditionTwo;
    [SerializeField] private GameObject conditionThree;

    [SerializeField] private TMPro.TMP_Dropdown d1;
    [SerializeField] private TMPro.TMP_Dropdown d3;
    [SerializeField] private TMPro.TMP_Dropdown d5;

    private int HP_1 = -1;
    private int HP_1_1 = -1;
    private int HP_2 = -1;
    private int HP_2_1 = -1;
    private int HP_3 = -1;
    private int HP_3_1 = -1;

    private GameObject model1;
    private GameObject model1_1;
    private GameObject model2;
    private GameObject model2_1;
    private GameObject model3;
    private GameObject model3_1;

    [SerializeField] private List<GameObject> models;



    private bool isConditionOneSet = false;
    private bool isConditionTwoSet = false;
    private bool isConditionThreeSet = false;

    private bool isConditionOneAndSet = false;
    private bool isConditionTwoAndSet = false;
    private bool isConditionThreeAndSet = false;

    private bool isConditionOneAndButtonPressed = false;
    private bool isConditionTwoAndButtonPressed = false;
    private bool isConditionThreeAndButtonPressed = false;

    private int id = 1;

    private int totalConditions = 1;

    //public bool hasMovedAway = true;


    void Start()
    {
        deactivateHandPanel();
        deactivateAllConditions();
    }
    public void handPoseOnClick(GameObject handPoseIcon)
    {
        resetAll();
        loadSceneHPConditions(); //added now
        activateHandPanel();
        //need to set values of these variables first if this scene was initialized
        
        if (totalConditions == 1 && isConditionOneSet == false)
        {
            activateOneCondition(conditionOne);

            model1 = Instantiate(handPoseIcon, conditionOne.transform.GetChild(1));
            model1.transform.localScale = new Vector3(300, 300, 300);
            //PopulateDropdown(d1);
            totalConditions = 2;
            isConditionOneSet = true;
            HP_1 = handPoseIcon.GetComponent<handPoseId>().myHandPoseId;

        }
        else if (totalConditions == 2 && isConditionTwoSet == false)
        {
            activateOneCondition(conditionTwo);
            model2 = Instantiate(handPoseIcon, conditionTwo.transform.GetChild(1));
            model2.transform.localScale = new Vector3(300, 300, 300);
            //PopulateDropdown(d3);
            totalConditions = 3;
            isConditionTwoSet = true;
            HP_2 = handPoseIcon.GetComponent<handPoseId>().myHandPoseId;
        }
        else if (totalConditions == 3 && isConditionThreeSet == false)
        {
            activateOneCondition(conditionThree);
            model3 = Instantiate(handPoseIcon, conditionThree.transform.GetChild(1));
            model3.transform.localScale = new Vector3(300, 300, 300);
            //PopulateDropdown(d5);
            totalConditions = 4;
            isConditionThreeSet = true;
            HP_3 = handPoseIcon.GetComponent<handPoseId>().myHandPoseId;
        }

        saveHandPoseConditionData();
    }


    public void saveHandPoseConditionData()
    {
        int transitionSceneId = -1;
        HandPoseConditions condition = null;
        List<HandPoseConditions> entries = new List<HandPoseConditions>();

        Debug.Log("final number of conditions" + totalConditions);

        transitionSceneId = d1.value + 1;
        Debug.Log(transitionSceneId);
        condition = new HandPoseConditions(1, HP_1, transitionSceneId, HP_1_1);
        entries.Add(condition);

        transitionSceneId = d3.value + 1;
        condition = new HandPoseConditions(2, HP_2, transitionSceneId, HP_2_1);
        entries.Add(condition);

        transitionSceneId = d5.value + 1;
        condition = new HandPoseConditions(3, HP_3, transitionSceneId, HP_3_1);
        entries.Add(condition);

        //save
        List<SceneObj> retrievals = FileHandler.ReadListFromJSON<SceneObj>("Root.json");

        foreach (var item in retrievals)
        {
            if (item.sceneID == manager.GetComponent<initializeScene>().currentScene)
            {
                item.handPoseConditions = entries;
            }
        }

        //object to json
        FileHandler.SaveToJSON<SceneObj>(retrievals, "Root.json");


        //clear and reset

        
    }

    public void resetAll()
    {
        HP_1 = -1; HP_1_1 = -1; HP_2 = -1; HP_2_1 = -1; HP_3 = -1; HP_3_1 = -1;

        //d1TransitionScene = 1; d3TransitionScene = 1; d5TransitionScene = 1;

        isConditionOneSet = false; isConditionTwoSet = false; isConditionThreeSet = false;

        isConditionOneAndSet = false; isConditionTwoAndSet = false; isConditionThreeAndSet = false;

        isConditionOneAndButtonPressed = false; isConditionTwoAndButtonPressed = false; isConditionThreeAndButtonPressed = false;

        totalConditions = 1;

        Destroy(model1); Destroy(model1_1); Destroy(model2); Destroy(model2_1); Destroy(model3); Destroy(model3_1);
        model1 = null; model1_1 = null; model2 = null; model2_1 = null; model3 = null; model3_1 = null;

        d1.ClearOptions(); d3.ClearOptions(); d5.ClearOptions();
        deactivateAllConditions();
        deactivateHandPanel();
    }

    public void loadSceneHPConditions() //made the scene id a parameter
    {
        //activateHandPanel();
        PopulateDropdown(d1);
        PopulateDropdown(d3);
        PopulateDropdown(d5);
        List<SceneObj> retrievals = FileHandler.ReadListFromJSON<SceneObj>("Root.json");
        List<HandPoseConditions> hp;

        foreach (var item in retrievals)
        {
            if (item.sceneID == manager.GetComponent<initializeScene>().currentScene)
            {
                Debug.Log("I am in scene" + item.sceneID);

                hp = item.handPoseConditions;
                if (hp.Count > 0)
                {
                    foreach (var c in hp)
                    {
                        int conditionNum = c.handPoseConditionNum;

                        if (conditionNum == 1)
                        {
                            HP_1 = c.handPoseId;
                            HP_1_1 = c.andHandPoseId;
                            Debug.Log("condition 1 is set");
                            foreach (GameObject m in models)
                            {

                                Debug.Log("I am in models block with id " + m.GetComponent<handPoseId>().myHandPoseId + "and HP_1 is " + HP_1);
                                if (m.GetComponent<handPoseId>().myHandPoseId == HP_1)
                                {
                                    Debug.Log("handpose id" + HP_1);
                                    Debug.Log("model id" + m.GetComponent<handPoseId>().myHandPoseId);
                                    activateHandPanel();
                                    activateOneCondition(conditionOne);
                                    model1 = Instantiate(m, conditionOne.transform.GetChild(1));
                                    model1.transform.localScale = new Vector3(300, 300, 300);
                                    totalConditions = 2;
                                    isConditionOneSet = true;
                                    d1.value = c.transitionSceneId - 1;
                                }

                                /*if (HP_1_1 != -1 && m.GetComponent<handPoseId>().myHandPoseId == HP_1_1)
                                {
                                    Debug.Log("and condition handpose id" + HP_1);
                                    Debug.Log("model id" + m.GetComponent<handPoseId>().myHandPoseId);

                                    conditionOne.transform.GetChild(4).transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;

                                    model1_1 = Instantiate(m, conditionOne.transform.GetChild(1));
                                    model1_1.transform.localScale = new Vector3(300, 300, 300);
                                    model1_1.transform.localPosition = new Vector3(-5.9f, -30.6f, -5.284005f);

                                    isConditionOneAndSet = true;
                                    isConditionOneAndButtonPressed = true;
                                    Debug.Log("Condition one and button pressed");
                                    //totalConditions--;

                                }*/
                            }
                        }

                        else if (conditionNum == 2)
                        {
                            HP_2 = c.handPoseId;
                            HP_2_1 = c.andHandPoseId;
                            Debug.Log("condition 2 is set");

                            foreach (GameObject m in models)
                            {
                                if (m.GetComponent<handPoseId>().myHandPoseId == HP_2)
                                {
                                    Debug.Log("handpose id" + HP_2);
                                    Debug.Log("model id" + m.GetComponent<handPoseId>().myHandPoseId);
                                    activateOneCondition(conditionTwo);
                                    model2 = Instantiate(m, conditionTwo.transform.GetChild(1));
                                    model2.transform.localScale = new Vector3(300, 300, 300);
                                    totalConditions = 3;
                                    isConditionTwoSet = true;
                                    d3.value = c.transitionSceneId - 1;
                                }

                                /*if (HP_2_1 != -1 && m.GetComponent<handPoseId>().myHandPoseId == HP_2_1)
                                {
                                    Debug.Log("and condition handpose id" + HP_2_1);
                                    Debug.Log("model id" + m.GetComponent<handPoseId>().myHandPoseId);
                                    conditionTwo.transform.GetChild(4).transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;



                                    model2_1 = Instantiate(m, conditionTwo.transform.GetChild(1));
                                    model2_1.transform.localScale = new Vector3(300, 300, 300);
                                    model2_1.transform.localPosition = new Vector3(-7.3f, -30.6f, -5.284005f);
                                    //  PopulateDropdown(d1);
                                    isConditionTwoAndSet = true;
                                    isConditionTwoAndButtonPressed = true;
                                    //totalConditions--;

                                }*/
                            }
                        }


                        else if (conditionNum == 3)
                        {
                            HP_3 = c.handPoseId;
                            HP_3_1 = c.andHandPoseId;

                            foreach (GameObject m in models)
                            {
                                if (m.GetComponent<handPoseId>().myHandPoseId == HP_3)
                                {
                                    activateOneCondition(conditionThree);
                                    model3 = Instantiate(m, conditionThree.transform.GetChild(1));
                                    model3.transform.localScale = new Vector3(300, 300, 300);
                                    totalConditions = 4;
                                    isConditionThreeSet = true;
                                    d5.value = c.transitionSceneId - 1;
                                }

                                /*if (HP_3_1!=-1 && m.GetComponent<handPoseId>().myHandPoseId == HP_3_1)
                                {
                                    conditionThree.transform.GetChild(4).transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;



                                    model3_1 = Instantiate(m, conditionThree.transform.GetChild(1));
                                    model3_1.transform.localScale = new Vector3(300, 300, 300);
                                    model3_1.transform.localPosition = new Vector3(-7.3f, -30.6f, -5.284005f);
                                    //PopulateDropdown(d1);
                                    isConditionThreeAndSet = true;
                                    isConditionThreeAndButtonPressed = true;
                                    //totalConditions--;

                                }*/
                            }
                        }

                    }
                }



            }
        }
    }


    private void deactivateAllConditions()
    {
        conditionOne.SetActive(false);

        conditionTwo.SetActive(false);

        conditionThree.SetActive(false);

    }

    private void activateOneCondition(GameObject c)
    {
        c.SetActive(true);
    }

    public void activateHandPanel()
    {
        if (handMenu.transform.position.y > 200)
        {
            

            GameObject playerAnchor = GameObject.Find("CenterEyeAnchor");
            if (playerAnchor != null)
            {
                handMenu.transform.position = playerAnchor.transform.position + new Vector3(0, 0, 1.5f);
            }
            else
            {
                handMenu.transform.position = new Vector3(0, 0.8114f, 1.0044f);
            }
            handMenu.transform.rotation = Quaternion.identity;
        }

        //handMenu.AddComponent<>

    }

    public void deactivateHandPanel()
    {
        handMenu.transform.position = new Vector3(0, 300, 0);
    }

    public void PopulateDropdown(TMPro.TMP_Dropdown dropdown)
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

    public void activateAndConditions(GameObject andButton)
    {
        if (andButton.name == "AND1" && isConditionOneAndButtonPressed == false)
        {
            isConditionOneAndButtonPressed = true;
            andButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
        }
        else if (andButton.name == "AND2" && isConditionTwoAndButtonPressed == false)
        {
            isConditionTwoAndButtonPressed = true;
            andButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
        }
        else if (andButton.name == "AND3" && isConditionThreeAndButtonPressed == false)
        {
            isConditionThreeAndButtonPressed = true;
            andButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
        }

    }
}
