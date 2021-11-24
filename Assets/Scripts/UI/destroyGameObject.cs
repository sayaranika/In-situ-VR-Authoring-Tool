using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyGameObject : MonoBehaviour
{
    [SerializeField] private GameObject placeholder;
    [SerializeField] private GameObject propertiesPanel;
    public bool destroyCalled = false;
    
    public void deleteObj()
    {
        propertiesPanel.transform.position = new Vector3(0, 200, 0);
        GameObject o = placeholder.GetComponent<SceneHandler>().referenceObj;
        //o.GetComponent<Renderer>().enabled = false;
        placeholder.GetComponent<SceneHandler>().referenceObj = null;
        
        Destroy(o);
    }

}
