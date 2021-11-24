using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectAttachedObject : MonoBehaviour
{
    //public int boneId;
    public GameObject attachedObject;
    public int attachedBoneId;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("spawnedObject"))
        {
            attachedObject = collision.gameObject;
            Debug.Log(gameObject.name + ": Dtected collision with " + collision.gameObject.name);
        }

        if (collision.gameObject.CompareTag("bone"))
        {
            //string name = ;
            attachedBoneId = int.Parse(collision.gameObject.name);
            Debug.Log(gameObject.name + ": Dtected collision with " + collision.gameObject.name);
        }

    }


    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("spawnedObject"))
        {
            attachedObject = null;
            //Debug.Log(gameObject.name + ": Dtected collision with " + collision.gameObject.name);
        }

        if (collision.gameObject.CompareTag("bone"))
        {
            //string name = ;
            attachedBoneId = -1;
            //Debug.Log(gameObject.name + ": Dtected collision with " + collision.gameObject.name);
        }
        Debug.Log(gameObject.name + ": Exiting trigger from " + collision.gameObject.name);

    }
}
