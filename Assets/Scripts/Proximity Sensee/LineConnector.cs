using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnector : MonoBehaviour
{
    [SerializeField] private GameObject sensee1;
    [SerializeField] private GameObject sensee2;
    private GameObject line;
    private UnityEngine.LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        line = new GameObject();
        line.transform.position = sensee1.transform.position;
        line.AddComponent<UnityEngine.LineRenderer>();
        lr = line.GetComponent<UnityEngine.LineRenderer>();
        lr.material = new Material(Shader.Find("Assets/MRTK/Shaders/MixedRealityStandard.shader"));
        lr.startWidth = 1.0f;
        lr.endWidth = 1.0f;
        lr.positionCount = 2;
        lr.sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (lr != null)
        {
            lr.SetPosition(0, sensee1.transform.position);
            lr.SetPosition(1, sensee2.transform.position);
            lr.startWidth = 0.001f;
            lr.endWidth = 0.001f;
        }
    }
}
