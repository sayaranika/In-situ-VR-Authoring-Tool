using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeHandler : MonoBehaviour
{
    [SerializeField] private GameObject colorPickerContainer;
    // Start is called before the first frame update
    void Start()
    {
        //colorPickerContainer.SetActive(false);
        //styleOption.transform.position = gameObject.transform.position + new Vector3(-1,0,0);
        colorPickerContainer.SetActive(false);
        Debug.Log("I am executed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
