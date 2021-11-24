using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceUser : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("CenterEyeAnchor").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            gameObject.transform.LookAt(target);
            gameObject.transform.Rotate(0, 180, 0);
        }
        
        else target = GameObject.Find("CenterEyeAnchor").transform;
       

    }
}
