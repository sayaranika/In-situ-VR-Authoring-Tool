using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustPositionOfColorPalette : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    
    void Update()
    {
        BoundsControl bc = cube.GetComponent<BoundsControl>();
        gameObject.transform.position = bc.transform.position + new Vector3(bc.transform.lossyScale.x, bc.transform.lossyScale.y - 0.5f, 0);

    }
}
