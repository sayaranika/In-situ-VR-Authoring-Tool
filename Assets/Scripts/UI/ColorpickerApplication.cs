using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorpickerApplication : MonoBehaviour
{
    [SerializeField] private FlexibleColorPicker colourPicker;
    [SerializeField] private GameObject placeholder;

    [SerializeField] private Material material;

    void Update()
    {
        GameObject obj = placeholder.GetComponent<SceneHandler>().referenceObj;
        if(obj != null)
        {
            Renderer renderer = obj.GetComponent<Renderer>(); //prev
            Material mat = renderer.material; //prev


            

            //mat.color = colourPicker.color;


            mat.SetColor("_Color", colourPicker.color); //prev
            renderer.material = mat; //prev
           



            //with alpha
            /*material.color = colourPicker.color;
            renderer.material = material;*/



        }
        
    }


}
