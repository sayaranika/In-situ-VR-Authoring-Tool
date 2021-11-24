using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StyleButtonPressed : MonoBehaviour
{

    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject colorPickerContainer;
    [SerializeField] private FlexibleColorPicker colorPicker;
    // Start is called before the first frame update
    void Start()
    {
        colorPickerContainer.SetActive(false);
        GetComponent<Interactable>().OnClick.AddListener(enableColorPicker);
    }

    public void enableColorPicker()
    {
        cube.GetComponent<ApplyingColour>().isColourPickerEnabled = true;
        colorPickerContainer.SetActive(true);
        //colorPickerContainer.transform.position = cube.transform.position + new Vector3(-2.0f,0,0);
        colorPicker.startingColor = cube.GetComponent<Material>().color;
        
    }
}
