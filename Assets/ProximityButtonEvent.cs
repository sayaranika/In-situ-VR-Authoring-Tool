using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityButtonEvent : MonoBehaviour
{

    [SerializeField]
    private GameObject pointSensee;
    private GameObject sensee1, sensee2;
    private GameObject centreEye;

    private GameObject line;
    private UnityEngine.LineRenderer lr;

    [SerializeField]
    private GameObject handsUI;
    private GameObject hands;


    /*private Vector3 headpos;

    int lineInitiated = 0;

    

    
    */

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>().OnClick.AddListener(SetProximityActive);

        //Get reference to player position
        centreEye = GameObject.Find("CenterEyeAnchor");
    }

    void SetProximityActive()
    {
        sensee1 = Instantiate(pointSensee);
        sensee1.transform.position = centreEye.transform.position + new Vector3(0, 0, 0.5f);

        sensee2 = Instantiate(pointSensee);
        sensee2.transform.position = centreEye.transform.position + new Vector3(0.4f, 0, 0.5f);

        hands = Instantiate(handsUI);
        hands.transform.position = centreEye.transform.position + new Vector3(0f, 0, 0.5f);

        line = new GameObject();
        line.transform.position = sensee1.transform.position;
        line.AddComponent<UnityEngine.LineRenderer>();
        lr = line.GetComponent<UnityEngine.LineRenderer>();
        lr.material = new Material(Shader.Find("Assets/MRTK/Shaders/MixedRealityStandard.shader"));
        lr.startWidth = 1.0f;
        lr.endWidth = 1.0f;
        lr.positionCount = 2;
        lr.sortingOrder = 1;
        

        
    }


    // Update is called once per frame
    void Update()
    {
        if(lr!=null)
        {
            lr.SetPosition(0, sensee1.transform.position);
            lr.SetPosition(1, sensee2.transform.position);
            lr.startWidth = 0.001f;
            lr.endWidth = 0.001f;
        }

        if(sensee1!=null && sensee2!=null)
        {
            float distance = Vector3.Distance(sensee1.transform.position, sensee2.transform.position);
            PlayerPrefs.SetFloat("dist", distance);
            Debug.Log(distance);
        }

        
        
    }
}
