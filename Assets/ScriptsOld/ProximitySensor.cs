using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySensor : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject spawnObject;
    int count = 2;
    
    public InputSourceType sourceType = InputSourceType.Hand;
    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        
        if (spawnObject != null && eventData.InputSource.SourceType == sourceType && count!=0)
        {
            var spawn = Instantiate(spawnObject);
            var result = eventData.Pointer.Result;
            if (result != null)
            {
                spawn.transform.position = result.Details.Point;
            }
            count--;
        }

        if(count == 0)
        {

            count = 2;
            gameObject.SetActive(false);

        }
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
    }
}
