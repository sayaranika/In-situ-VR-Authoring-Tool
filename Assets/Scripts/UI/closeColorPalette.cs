using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeColorPalette : MonoBehaviour
{
    [SerializeField] private GameObject colorPalette;
    [SerializeField] private GameObject controlBar;

    public void closeColor()
    {
        colorPalette.SetActive(false);
        controlBar.SetActive(true);
    }
}
