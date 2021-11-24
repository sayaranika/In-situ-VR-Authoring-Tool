using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableStyleOptions : MonoBehaviour
{
    [SerializeField] private GameObject styleOption;

    private void Start()
    {
        styleOption.SetActive(false);
    }
    public void disableStyleOption()
    {
        styleOption.SetActive(false);
    }
}
