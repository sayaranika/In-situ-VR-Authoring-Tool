using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ActiveObjects
{
    public Vector3 parentPos;
    public Quaternion parentRot;
    public Vector3 parentScale;
    public Vector3 cubePos;
    public Quaternion cubeRot;
    public Vector3 cubeScale;
    public string colorVal;
   // public int activeColor;

    public ActiveObjects(Vector3 parentPos, Quaternion parentRot, Vector3 parentScale, Vector3 cubePos, Quaternion cubeRot, Vector3 cubeScale, string ac)
    {
        this.parentPos = parentPos;
        this.parentRot = parentRot;
        this.parentScale = parentScale;
        this.cubePos = cubePos;
        this.cubeRot = cubeRot;
        this.cubeScale = cubeScale;
        this.colorVal = ac;
       // this.activeColor = activeColor;
    }
}
