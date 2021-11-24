using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableStyleOptions : MonoBehaviour
{
    [SerializeField] private GameObject styleOption;
    [SerializeField] private GameObject colorPickerContainer;
    private void Start()
    {
        styleOption.SetActive(false);
    }

    public void enableStyleOption()
    {
        styleOption.SetActive(true);
        //styleOption.transform.position = gameObject.transform.position + new Vector3(-1.0f, 1.0f, 0.5f);
        
    }
}
