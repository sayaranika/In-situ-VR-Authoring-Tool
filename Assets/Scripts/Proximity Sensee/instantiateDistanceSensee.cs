using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateDistanceSensee : MonoBehaviour
{
    [SerializeField] private GameObject distanceSensor;

    public void instantiateSensees()
    {
        Instantiate(distanceSensor);
    }
}
