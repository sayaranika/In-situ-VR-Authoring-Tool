using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSensorAtPointerLocation : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject spawnObject;
    public GameObject lineDrawer;
    public InputSourceType sourceType = InputSourceType.Hand;
    int count = 2;
    int i = 0;

    private GameObject[] spawnedGameObjects;

    private LineRenderer lr;
    private Vector3[] points;
    // Start is called before the first frame update
    void OnEnable()
    {
        CoreServices.InputSystem.Register(gameObject);


    }

    private void OnDisable()
    {
        if (CoreServices.InputSystem != null)
        {
            CoreServices.InputSystem.Unregister(gameObject);
        }
    }

    private void Start()
    {
        lr = lineDrawer.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        Debug.Log("Clicked Proximity");
        gameObject.GetComponent<spawnSensorAtPointerLocation>().enabled = false;
    }

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
            spawnedGameObjects[i] = spawn;
            i++;
            var result = eventData.Pointer.Result;
            if (result != null)
            {
                spawn.transform.position = result.Details.Point;
                points[i] = result.Details.Point;
            }

            if(count == 1)
            {
                
                    lr.SetPosition(0, points[0]);
                    lr.SetPosition(1, points[1]);
                
            }
        }

        if(count == 0)
        {
            gameObject.GetComponent<spawnSensorAtPointerLocation>().enabled = false;
            count = 2;
        }
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
    }
}
