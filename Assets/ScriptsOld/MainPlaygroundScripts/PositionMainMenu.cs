using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMainMenu : MonoBehaviour
{
    private GameObject centerEye;
    // Start is called before the first frame update
    void Start()
    {
        centerEye = GameObject.Find("CenterEyeAnchor");
        if (centerEye != null)
            gameObject.transform.position = centerEye.transform.position + new Vector3(-0.01f, -0.08f, 0.3f);
        else
            Debug.Log("Could not locate center eye");
    }

    private void OnEnable()
    {
        centerEye = GameObject.Find("CenterEyeAnchor");
        if (centerEye != null)
            gameObject.transform.position = centerEye.transform.position + new Vector3(-0.1f, -0.1f, 0.4f);
        else
            Debug.Log("Could not locate center eye");
    }


}
