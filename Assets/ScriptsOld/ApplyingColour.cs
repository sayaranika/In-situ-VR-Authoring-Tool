using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyingColour : MonoBehaviour
{
    [SerializeField] private FlexibleColorPicker colourPicker;
    [SerializeField] private Material material;
    [SerializeField] private GameObject placeholder;
    public bool isColourPickerEnabled = false;
   
    void Update()
    {


        //if(colourPicker.enabled == true)
        //{
        /*GameObject obj = placeholder.GetComponent<initializaMenu>().referenceObj;
            Renderer renderer = obj.GetComponent<Renderer>();
            Material mat = renderer.material;
            mat.SetColor("_Color", colourPicker.color);
            renderer.material = mat;
            /*

            MaterialPropertyBlock props = new MaterialPropertyBlock();
            props.SetColor("_Color", colourPicker.color);
            GetComponent<Renderer>().SetPropertyBlock(props);*/
       // }
    }
}
