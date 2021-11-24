using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustPosition : MonoBehaviour
{
    [SerializeField]
    GameObject UIPanel;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = UIPanel.transform.position + new Vector3(0.001f,0,0);
        //gameObject.transform.localRotation = UIPanel.transform.rotation;
        //gameObject.transform.localScale = 0.2f;

    }
}
