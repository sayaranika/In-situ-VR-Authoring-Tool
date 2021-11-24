using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSelection : MonoBehaviour
{
    [SerializeField] GameObject[] boneList;
    Color defaultColor;

    [SerializeField] GameObject manager;

    private void Start()
    {
        defaultColor = boneList[0].GetComponent<Renderer>().material.color;
    }
    public void changeColor()
    {
        
        foreach (var bone in boneList)
        {
           
                Renderer r = bone.GetComponent<Renderer>(); //prev
                Material m = r.material; //prev


                m.SetColor("_Color", defaultColor); //prev
                r.material = m; //prev
            
        }

        Renderer renderer = gameObject.GetComponent<Renderer>(); //prev
        Material mat = renderer.material; //prev

        mat.SetColor("_Color", Color.green); //prev
        renderer.material = mat; //prev

        manager.GetComponent<initializeScene>().selectedBone = int.Parse(gameObject.transform.name);


    }
}
