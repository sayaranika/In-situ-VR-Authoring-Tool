using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateButtonPressed : MonoBehaviour
{
    [SerializeField] private GameObject duplicateButton;
    [SerializeField] private GameObject objectToSpawn;
    private int i = 0;
    List<ActiveObjects> sceneEntries = new List<ActiveObjects>();
    List<ActiveObjects> sceneRetrievals = new List<ActiveObjects>();
    // Start is called before the first frame update
    void Start()
    {
        duplicateButton.SetActive(false);
        
        GetComponent<Interactable>().OnClick.AddListener(enableDuplicate);
    }

    public void enableDuplicate()
    {
        if(i==0)
        duplicateButton.SetActive(true);
        


        if(i==0)
        {
            GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");
            sceneEntries = FileHandler.ReadListFromJSON<ActiveObjects>("scene1.json");
            foreach (var obj in listOfObjects)
            {
                Vector3 parentPos = obj.transform.position;
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
                sceneEntries.Add(activeObj);

                Destroy(obj);


                

            }
            //enable later
            //FileHandler.SaveToJSON<ActiveObjects>(sceneEntries, "scene1.json");
            Debug.Log("Saved");


            //loading from file
            sceneRetrievals = FileHandler.ReadListFromJSON<ActiveObjects>("scene1.json");
            foreach(var scene in sceneRetrievals)
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
                if(ColorUtility.TryParseHtmlString("#"+scene.colorVal, out color))
                mat.SetColor("_Color", color );
                //reassign the material to the renderer
                renderer.material = mat;
            }
        }

        i++;
    }
}
