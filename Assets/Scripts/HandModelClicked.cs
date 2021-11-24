using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandModelClicked : MonoBehaviour
{
    public void boneSelected(int i)
    {
       // selection.GetComponent<Material>().color = Color.green;
        Debug.Log("Called from " + i);
    }
}
