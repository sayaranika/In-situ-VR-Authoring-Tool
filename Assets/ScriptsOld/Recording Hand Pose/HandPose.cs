using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class HandPose
{
    public string name; 
    public List<Vector3> fingerData;
    public Quaternion wristRotation;
    public bool isLeft;
    public bool isRight;
    
    //public UnityEvent onRecognized;

    public HandPose(string name, List<Vector3> fingerData, Quaternion wristRotation, bool isLeft, bool isRight)
    {
        this.name = name;
        this.fingerData = fingerData;
        this.wristRotation = wristRotation;
        this.isLeft = isLeft;
        this.isRight = isRight;
    }
}
