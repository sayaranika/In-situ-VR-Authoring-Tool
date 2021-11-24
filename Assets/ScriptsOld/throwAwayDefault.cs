using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwAwayDefault : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Canvas>().worldCamera = null;
    }
}
