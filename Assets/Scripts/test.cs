using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private GameObject handModel;
    int i = 0;
    void Start()
    {
        Debug.Log("Before instantiate");
        GameObject left = Instantiate(handModel);
        Debug.Log("After instantiate");
        Transform wrist = left.transform.GetChild(0).gameObject.transform.GetChild(0);
        Transform forearm_stub = wrist.transform.GetChild(0);
        Transform index1 = wrist.transform.GetChild(1);
        Transform index2 = index1.transform.GetChild(0);
        Transform index3 = index2.transform.GetChild(0);

        Transform middle1 = wrist.transform.GetChild(2);
        Transform middle2 = middle1.transform.GetChild(0);
        Transform middle3 = middle2.transform.GetChild(0);

        Transform pinky0 = wrist.transform.GetChild(3);
        Transform pinky1 = pinky0.transform.GetChild(0);
        Transform pinky2 = pinky1.transform.GetChild(0);
        Transform pinky3 = pinky2.transform.GetChild(0);


        Transform ring1 = wrist.transform.GetChild(4);
        Transform ring2 = ring1.transform.GetChild(0);
        Transform ring3 = ring2.transform.GetChild(0);



        Transform thumb0 = wrist.transform.GetChild(5);
        Transform thumb1 = thumb0.transform.GetChild(0);
        Transform thumb2 = thumb1.transform.GetChild(0);
        Transform thumb3 = thumb2.transform.GetChild(0);


    }

    // Update is called once per frame
    void Update()
    {
        if (i == 1) Debug.Log("Found Data");
        else Debug.Log("Not found");
    }
}
