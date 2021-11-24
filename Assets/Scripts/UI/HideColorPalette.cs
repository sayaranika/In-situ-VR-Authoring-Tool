using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideColorPalette : MonoBehaviour
{
    [SerializeField] private GameObject colorPalette;

    private void Start()
    {
        
        colorPalette.SetActive(false);
        gameObject.transform.position = new Vector3(0, 200, 0);
        Debug.Log("hid the palette");
    }

    
}
