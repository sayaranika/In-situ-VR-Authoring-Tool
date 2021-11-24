using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private GameObject line;
    private UnityEngine.LineRenderer lr;
    public Transform pos1;
    public Transform pos2;
    // Start is called before the first frame update
    void Start()
    {

        line = new GameObject();
        line.transform.position = pos1.position;
        line.AddComponent<UnityEngine.LineRenderer>();



        lr = line.GetComponent<UnityEngine.LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        //lr.startColor = Color.blue;
        //lr.endColor = Color.blue;
        lr.startWidth = 1.0f;
        lr.endWidth = 1.0f;
        lr.positionCount = 2;
        lr.sortingOrder = 1;

    }

    // Update is called once per frame
    void Update()
    {
        
        lr.SetPosition(0, pos1.position);
        lr.SetPosition(1, pos2.position);
        lr.startWidth = 0.001f;
        lr.endWidth = 0.001f;
        
        
        
    }
}
