using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustPositionOfControlMenu : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject colorPicker;
    [SerializeField] private GameObject animationPanel;
    // Start is called before the first frame update
    void Start()
    {
        colorPicker.SetActive(false);
        animationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        BoundsControl bc = cube.GetComponent<BoundsControl>();
        gameObject.transform.position = bc.transform.position + new Vector3(0, bc.transform.lossyScale.y - 0.1f, 0);
        
    }
}
