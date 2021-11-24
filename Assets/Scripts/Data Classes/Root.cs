using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

[Serializable]
public class Root
{
    public List<SceneObj> sceneObj;

    public Root(List<SceneObj> sceneObj)
    {
        this.sceneObj = sceneObj;
    }
}

[Serializable]
public class SceneObj
{
    public int sceneID;
    public List<GameObj> gameObj;
    public List<HandPoseConditions> handPoseConditions;
    public List<ProximitySensorConditions> proximitySensorConditions;
    public List<HPAnimation> animations;

    public SceneObj()
    {
        
    }

    public SceneObj(int sceneID)
    {
        this.sceneID = sceneID;
    }

    public SceneObj(int sceneID, List<GameObj> gameObj)
    {
        this.sceneID = sceneID;
        this.gameObj = gameObj;
    }

    public SceneObj(int sceneID, List<GameObj> gameObj, List<HandPoseConditions> handPoseConditions, List<ProximitySensorConditions> proximitySensorConditions, List<HPAnimation> animations)
    {
        this.sceneID = sceneID;
        this.gameObj = gameObj;
        this.handPoseConditions = handPoseConditions;
        this.proximitySensorConditions = proximitySensorConditions;
        this.animations = animations;
    }
}

[Serializable]
public class GameObj
{
    public int objId;
    public string objectName;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public string colourValue;


    public float distance;
    public int navigateTo;

    public HandsAnimation handsAnimation;
    public List<CustomAnimation> customAnimations;

    public GameObj() { }
    public GameObj(int objId, string objName, Vector3 position, Quaternion rotation, Vector3 scale, string colourValue)
    {
        this.objId = objId;
        this.objectName = objName;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.colourValue = colourValue;

    }

   

    public GameObj(int objId, string objName, Vector3 position, Quaternion rotation, Vector3 scale, string colourValue, float distance, int navigateTo)
    {
        this.objId = objId;
        this.objectName = objName;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.colourValue = colourValue;
        this.distance = distance;
        this.navigateTo = navigateTo;
    }

    public GameObj(int objId, string objName, Vector3 position, Quaternion rotation, Vector3 scale, string colourValue, float distance, int navigateTo, HandsAnimation handsAnimation)
    {
        this.objId = objId;
        this.objectName = objName;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.colourValue = colourValue;
        this.distance = distance;
        this.navigateTo = navigateTo;
        this.handsAnimation = handsAnimation;
    }

    public GameObj(int objId, string objName, Vector3 position, Quaternion rotation, Vector3 scale, string colourValue, float distance, int navigateTo, HandsAnimation handsAnimation, List<CustomAnimation> customAnimations)
    {
        this.objId = objId;
        this.objectName = objName;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.colourValue = colourValue;
        this.distance = distance;
        this.navigateTo = navigateTo;
        this.handsAnimation = handsAnimation;
        this.customAnimations = customAnimations;
    }
}

[Serializable]
public class ProximitySensorConditions
{
    static int nextId;
    public int proximitySensorConditionId { get; private set; }
    
    public bool isSensee_1_ObjectAttached;
    public bool isSensee_1_handAttached;
    public int sensee_1_objectId;
    public int sensee_1_boneId;
    public Vector3 sensee_1_position;


    public bool isSensee_2_ObjectAttached;
    public bool isSensee_2_handAttached;
    public int sensee_2_objectId;
    public int sensee_2_boneId;
    public Vector3 sensee_2_position;

    public float distance;
    public bool isGreaterCondition;
    public bool isLessCondition;
    public bool isEqualCondition;

    public int currentSceneId;
    public int transitionSceneId;

    public ProximitySensorConditions() {
        //this.proximitySensorConditionId = Interlocked.Increment(ref nextId);
    }
    public ProximitySensorConditions(int proximitySensorConditionId, bool isSensee_1_ObjectAttached, bool isSensee_1_handAttached,
     int sensee_1_objectId, int sensee_1_boneId, Vector3 sensee_1_position, bool isSensee_2_ObjectAttached, bool isSensee_2_handAttached,
     int sensee_2_objectId, int sensee_2_boneId, Vector3 sensee_2_position, float distance, bool isGreaterCondition, bool isLessCondition, bool isEqualCondition, 
     int currentSceneId, int transitionSceneId)
    {
        this.proximitySensorConditionId = Interlocked.Increment(ref nextId);
        this.sensee_1_position = sensee_1_position;
        this.isSensee_1_ObjectAttached = isSensee_1_ObjectAttached;
        this.isSensee_1_handAttached = isSensee_1_handAttached;
        this.sensee_1_objectId = sensee_1_objectId;
        this.sensee_1_boneId = sensee_1_boneId;
        this.isSensee_2_ObjectAttached = isSensee_2_ObjectAttached;
        this.isSensee_2_handAttached = isSensee_2_handAttached;
        this.sensee_2_objectId = sensee_2_objectId;
        this.sensee_2_boneId = sensee_2_boneId;
        this.sensee_2_position = sensee_2_position;
        this.distance = distance;
        this.isGreaterCondition = isGreaterCondition;
        this.isLessCondition = isLessCondition;
        this.isEqualCondition = isEqualCondition;
        this.currentSceneId = currentSceneId; 
        this.transitionSceneId = transitionSceneId;
    }
}

[Serializable]
public class HandPoseSensor
{
    public int handPoseId;
    public List<Vector3> fingerData;
    public Quaternion wristRotation;
    public bool isLeft;
    public bool isRight;

    //public UnityEvent onRecognized;

    public HandPoseSensor() { }
    public HandPoseSensor(int handPoseId, List<Vector3> fingerData, Quaternion wristRotation, bool isLeft, bool isRight)
    {
        this.handPoseId = handPoseId;
        this.fingerData = fingerData;
        this.wristRotation = wristRotation;
        this.isLeft = isLeft;
        this.isRight = isRight;
    }
}
[Serializable]
public class HandPoseConditions
{
    public int handPoseConditionNum;
    public int handPoseId;
    public int transitionSceneId;
    public int andHandPoseId;

    public HandPoseConditions() { }

    public HandPoseConditions(int conditionNum, int handPoseId, int transitionSceneId)
    {
        this.handPoseConditionNum = conditionNum;
        this.handPoseId = handPoseId;
        this.transitionSceneId = transitionSceneId;
    }
    public HandPoseConditions(int conditionNum, int handPoseId, int transitionSceneId, int andConditionId)
    {
        this.handPoseConditionNum = conditionNum;
        this.handPoseId = handPoseId;
        this.transitionSceneId = transitionSceneId;
        this.andHandPoseId = andConditionId;
    }

}


[Serializable]
public class HandsAnimation
{
    public int boneId;
    public bool followX;
    public bool followY;
    public bool followZ;

    public HandsAnimation() { }
    public HandsAnimation(int boneId, bool followX, bool followY, bool followZ)
    {
        this.boneId = boneId;
        this.followX = followX;
        this.followY = followY;
        this.followZ = followZ;
    }

}


[Serializable]
public class CustomAnimation
{
    public Vector3 position;
    public Quaternion rotation;

    public CustomAnimation() { }
    public CustomAnimation(Vector3 pos, Quaternion rot)
    {
        this.position = pos;
        this.rotation = rot;
    }

}

[Serializable]
public class HPAnimation
{
    public int objectId;
    public List<Vector3> positionTranslations;
    public List<Quaternion> rotationTranslations;

    public HPAnimation() { }
    public HPAnimation(int objectId, List<Vector3> positionTranslations, List<Quaternion> rotationTranslations)
    {
        this.objectId = objectId;
        this.positionTranslations = positionTranslations;
        this.rotationTranslations = rotationTranslations;
    }

}