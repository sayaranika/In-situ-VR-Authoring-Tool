using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneBuilderHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject sceneBuilder;
    public void toggleSceneBuilder()
    {
        if (sceneBuilder.activeInHierarchy == false)
        {
            sceneBuilder.SetActive(true);

        }
        
    }
}
