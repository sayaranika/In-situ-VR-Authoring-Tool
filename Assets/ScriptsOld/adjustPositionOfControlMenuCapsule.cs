using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustPositionOfControlMenuCapsule : MonoBehaviour
{
    [SerializeField] private GameObject capsule;
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
        BoundsControl bc = capsule.GetComponent<BoundsControl>();
        gameObject.transform.position = bc.transform.position + new Vector3(0, bc.transform.lossyScale.y + 0.5f, 0);

    }
}
