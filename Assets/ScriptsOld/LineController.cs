using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform[] points;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    public void setUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
