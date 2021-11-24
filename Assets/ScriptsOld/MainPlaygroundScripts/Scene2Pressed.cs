using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Pressed : MonoBehaviour
{
    List<ActiveObjects> sceneEntries = new List<ActiveObjects>();
    List<ActiveObjects> sceneRetrievals = new List<ActiveObjects>();
    [SerializeField] private GameObject objectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>().OnClick.AddListener(saveSceneOneandLoadSceneTwo);
    }

    public void saveSceneOneandLoadSceneTwo()
    {
        GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");
        //sceneEntries = FileHandler.ReadListFromJSON<ActiveObjects>("scene2.json");
        foreach (var obj in listOfObjects)
        {
            /*Vector3 parentPos = obj.transform.position;
            Quaternion parentRot = obj.transform.rotation;
            Vector3 parentScale = obj.transform.localScale;
            Transform childCube = obj.transform.Find("Cube");
            Vector3 cubePos = childCube.transform.localPosition;
            Quaternion cubeRot = childCube.transform.localRotation;

            BoundsControl bc = childCube.GetComponent<BoundsControl>();
            Vector3 cubeScale = bc.transform.lossyScale;

            //Vector3 cubeScale = obj.transform.lossyScale;
            //int activeColor = childCube.GetComponent<Material>().GetHashCode();
            string ac = ColorUtility.ToHtmlStringRGBA(childCube.GetComponent<Renderer>().material.color);

            ActiveObjects activeObj = new ActiveObjects(parentPos, parentRot, parentScale, cubePos, cubeRot, cubeScale, ac);
            sceneEntries.Add(activeObj);*/

            Destroy(obj);

        }

        //FileHandler.SaveToJSON<ActiveObjects>(sceneEntries, "scene2.json");
        //Debug.Log("Saved");

        sceneRetrievals = FileHandler.ReadListFromJSON<ActiveObjects>("scene2.json");
        foreach (var scene in sceneRetrievals)
        {
            GameObject o = Instantiate(objectToSpawn);
            o.transform.position = scene.parentPos;
            o.transform.rotation = scene.parentRot;
            o.transform.localScale = scene.parentScale;
            Transform ch = o.transform.Find("Cube");
            ch.localPosition = scene.cubePos;
            ch.localRotation = scene.cubeRot;
            ch.localScale = scene.cubeScale;

            Renderer renderer = ch.GetComponent<Renderer>();
            //get the material of the renderer
            Material mat = renderer.material;
            //set the color property
            Color color;
            if (ColorUtility.TryParseHtmlString("#" + scene.colorVal, out color))
                mat.SetColor("_Color", color);
            //reassign the material to the renderer
            renderer.material = mat;
        }
    }
}
