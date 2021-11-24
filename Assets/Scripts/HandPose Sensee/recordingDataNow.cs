using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class recordingDataNow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ParseScene()
    {
        List<SceneObj> scenes = FileHandler.ReadListFromJSON<SceneObj>("Root.json");
        List<HandPoseSensor> handPoses = FileHandler.ReadListFromJSON<HandPoseSensor>("handLibrary.json");
        List<int> poseId = new List<int>();

        foreach(var scene in scenes)
        {
            List<HandPoseConditions> handConditions = scene.handPoseConditions;
            foreach(var condition in handConditions)
            {
                if (condition.handPoseId != -1) poseId.Add(condition.handPoseId);
            }
        }

        List<int> distinctPoses = poseId.Distinct().ToList();

        foreach (var i in distinctPoses)
        {
            Debug.Log("2000: Unique hand pose id " + i);
        }
    }
}
