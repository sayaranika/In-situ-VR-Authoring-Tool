using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _handModel;
    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            _handModel.transform.rotation *= Quaternion.Euler(-v, 0, 0);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
