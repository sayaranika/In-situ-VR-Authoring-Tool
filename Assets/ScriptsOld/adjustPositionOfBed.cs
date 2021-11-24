using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustPositionOfBed : MonoBehaviour
{
    [SerializeField] private GameObject bed;
    [SerializeField] private GameObject animationPanel;
    // Start is called before the first frame update
    void Start()
    {
        animationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        BoundsControl bc = bed.GetComponent<BoundsControl>();
        gameObject.transform.position = bc.transform.position + new Vector3(0, bc.transform.lossyScale.y, 0.2f);

    }
}
