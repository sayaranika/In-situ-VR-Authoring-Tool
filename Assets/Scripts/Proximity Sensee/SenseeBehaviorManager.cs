using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseeBehaviorManager : MonoBehaviour
{
    public List<ProximitySensorConditions> proximityConditions;


    private bool isSensee_1_ObjectAttached;
    private bool isSensee_1_handAttached;
    private int sensee_1_objectId;
    private int sensee_1_boneId;
    private Vector3 sensee_1_position;


    private bool isSensee_2_ObjectAttached;
    private bool isSensee_2_handAttached;
    private int sensee_2_objectId;
    private int sensee_2_boneId;
    private Vector3 sensee_2_position;

    private float distance;
    private bool isGreaterCondition;
    private bool isLessCondition;
    private bool isEqualCondition;

    private int currentSceneId;
    private int transitionSceneId;
    

    public void setSenseeOneProperties()
    {

    }

    public void setSenseeTwoProperties()
    {

    }

}
