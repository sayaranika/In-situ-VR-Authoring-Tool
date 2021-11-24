using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initializeScene : MonoBehaviour
{
    [SerializeField] private GameObject sceneButtonList;
    [SerializeField] public GameObject placeholder;

    
    //private Transform currentSceneButton;

    public int currentScene = 0;
    public int previousScene = 0;
    public int totalNumberOfScenes = 0;
    public int selectedBone;


    List<SceneObj> entries = new List<SceneObj>();
    //List<SceneObj> retrievals = new List<SceneObj>();



    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject capsule;
    [SerializeField] private GameObject quad;
    [SerializeField] private GameObject cylinder;


    [SerializeField] private GameObject bed;
    [SerializeField] private GameObject chair;
    [SerializeField] private GameObject bookcase;
    [SerializeField] private GameObject table;

    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject apple;
    [SerializeField] private GameObject plate;

    [SerializeField] private GameObject banana;
    [SerializeField] private GameObject chopstick;
    [SerializeField] private GameObject halfApple;
    [SerializeField] private GameObject knife;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject cup;
    [SerializeField] private GameObject target;



    [SerializeField] private GameObject propMenu;
    [SerializeField] private FlexibleColorPicker colour;
    [SerializeField] private GameObject container;

    [SerializeField] public GameObject manager;



    private void Awake()
    {
        
        FileHandler.SaveToJSON<SceneObj>(new List<SceneObj>(), "Root.json");
        container.SetActive(false);
        createScene();
        currentScene = totalNumberOfScenes;
        setCurrentSceneButtonColor();
        manager.GetComponent<openColorPalette>().closeAnimationPanel();
    }

    public void createScene()
    {
        entries = FileHandler.ReadListFromJSON<SceneObj>("Root.json");
        totalNumberOfScenes = totalNumberOfScenes + 1;
        SceneObj scene = new SceneObj(totalNumberOfScenes);
        entries.Add(scene);
        FileHandler.SaveToJSON<SceneObj>(entries, "Root.json");

        Transform newSceneButton = sceneButtonList.transform.Find("Scene" + totalNumberOfScenes.ToString());
        if (newSceneButton != null) newSceneButton.gameObject.SetActive(true);

    }


    public void duplicateCurrentScene()
    {
        entries = FileHandler.ReadListFromJSON<SceneObj>("Root.json");
        totalNumberOfScenes = totalNumberOfScenes + 1;


        List<GameObj> objEntries = new List<GameObj>();
        GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");

        foreach (var obj in listOfObjects)
        {
            int objId = obj.GetInstanceID();
            string objName = obj.name;
            Vector3 objPosition = obj.transform.position;
            Quaternion objRotation = obj.transform.rotation;

            BoundsControl bc = obj.GetComponent<BoundsControl>();
            Vector3 objScale = bc.transform.lossyScale;

            //float distance = -1;

            string ac = ColorUtility.ToHtmlStringRGBA(obj.GetComponent<Renderer>().material.color);

            GameObj item = new GameObj(objId, objName, objPosition, objRotation, objScale, ac);
            objEntries.Add(item);
        }

        SceneObj scene = new SceneObj(totalNumberOfScenes, objEntries);

        entries.Add(scene);
        FileHandler.SaveToJSON<SceneObj>(entries, "Root.json");

        Transform newSceneButton = sceneButtonList.transform.Find("Scene" + totalNumberOfScenes.ToString());
        if (newSceneButton != null) newSceneButton.gameObject.SetActive(true);

    }

    private void setCurrentSceneButtonColor()
    {
        Transform currentSceneButton = sceneButtonList.transform.Find("Scene" + currentScene.ToString());
        if (currentSceneButton != null)
        {
            currentSceneButton.gameObject.GetComponent<Image>().color = Color.green;
        }
        
        Transform previousSceneButton = sceneButtonList.transform.Find("Scene" + previousScene.ToString());
        if (previousSceneButton != null)
        {
            previousSceneButton.gameObject.GetComponent<Image>().color = Color.white;
        }
        
    }

    public void changeSceneSelection(int newSceneId)
    {
        propMenu.transform.position = new Vector3(0, 200, 0);
        //set current and previous scene values
        previousScene = currentScene;
        currentScene = newSceneId;
        //set button colors based on current and previous scene views
        setCurrentSceneButtonColor();
        //save previous scene objects
        saveGameObjects(previousScene);
        //load current scene objects
        loadGameObjects(currentScene);
    }

    public void saveGameObjects(int sceneId)
    {
        //Debug.Log("3000: Saving Objects");
        List<SceneObj> retrievals = FileHandler.ReadListFromJSON<SceneObj>("Root.json");

        //modify property
        List<GameObj> objEntries = getObjectsOfCurrentScene();
        foreach(var item in retrievals)
        {
            if(item.sceneID == sceneId)
            {
                item.gameObj = objEntries;
            }
        }

        //object to json
        FileHandler.SaveToJSON<SceneObj>(retrievals, "Root.json");
    }

    private List<GameObj> getObjectsOfCurrentScene()
    {
        List<GameObj> objEntries = new List<GameObj>();
        GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("spawnedObject");
       
        foreach (var obj in listOfObjects)
        {
            int objId = obj.GetInstanceID();
            string objName = obj.name;
            Vector3 objPosition = obj.transform.position;
            Quaternion objRotation = obj.transform.rotation;
            
            BoundsControl bc = obj.GetComponent<BoundsControl>();
            Vector3 objScale = bc.transform.lossyScale;

            float distance = obj.GetComponent<setRef>().distance;
            int navigateTo = obj.GetComponent<setRef>().navigateTo;

            HandsAnimation handsAnimations = obj.GetComponent<setRef>().hAnimations;
            List<CustomAnimation> cAnimations = obj.GetComponent<setRef>().cAnimations;

            string ac = ColorUtility.ToHtmlStringRGBA(obj.GetComponent<Renderer>().material.color);

            

            GameObj item = new GameObj(objId, objName, objPosition, objRotation, objScale, ac, obj.GetComponent<setRef>().distance, obj.GetComponent<setRef>().navigateTo, handsAnimations, cAnimations);
            //Debug.Log("3000: Distance: " + obj.GetComponent<setRef>().distance + ", navigate to: " + obj.GetComponent<setRef>().navigateTo);
            objEntries.Add(item);

            Destroy(obj);
        }

        return objEntries;
    }






    //loading Game
    public void loadGameObjects(int sceneId)
    {
        //Debug.Log("Loading objects");
        List<SceneObj> sceneRetrievals = FileHandler.ReadListFromJSON<SceneObj>("Root.json");
        foreach (var scene in sceneRetrievals)
        {
            if(scene.sceneID == sceneId)
            {
                List<GameObj> gameObjects = scene.gameObj;
                foreach(var item in gameObjects)
                {
                    GameObject o;
                    if (item.objectName == "cube(Clone)" || item.objectName == "cube")
                    {
                        o = Instantiate(cube);
                    }
                    else if (item.objectName == "sphere(Clone)" || item.objectName == "sphere")
                    {
                        o = Instantiate(sphere);
                    }
                    else if (item.objectName == "cylinder(Clone)" || item.objectName == "cylinder")
                    {
                        o = Instantiate(cylinder);
                    }
                    else if (item.objectName == "capsule(Clone)" || item.objectName == "capsule")
                    {
                        o = Instantiate(capsule);
                    }
                    else if (item.objectName == "quad(Clone)" || item.objectName == "quad")
                    {
                        o = Instantiate(quad);
                    }
                    else if (item.objectName == "bed(Clone)" || item.objectName == "bed")
                    {
                        o = Instantiate(bed);
                    }
                    else if (item.objectName == "chair(Clone)" || item.objectName == "chair")
                    {
                        o = Instantiate(chair);
                    }
                    else if (item.objectName == "table(Clone)" || item.objectName == "table")
                    {
                        o = Instantiate(table);
                    }
                    else if (item.objectName == "bookcase(Clone)" || item.objectName == "bookcase") //added new
                    {
                        o = Instantiate(bookcase);
                    }
                    else if (item.objectName == "shield(Clone)" || item.objectName == "shield") //added new
                    {
                        o = Instantiate(shield);
                    }
                    else if (item.objectName == "apple(Clone)" || item.objectName == "apple") //added new
                    {
                        o = Instantiate(apple);
                    }
                    else if (item.objectName == "plate(Clone)" || item.objectName == "plate") //added new
                    {
                        o = Instantiate(plate);
                    }
                    else if (item.objectName == "banana(Clone)" || item.objectName == "banana") //added new
                    {
                        o = Instantiate(banana);
                    }
                    else if (item.objectName == "chopstick(Clone)" || item.objectName == "chopstick") //added new
                    {
                        o = Instantiate(chopstick);
                    }
                    else if (item.objectName == "appleHalf(Clone)" || item.objectName == "appleHalf")
                    {
                        o = Instantiate(halfApple);
                    }
                    else if (item.objectName == "cookingKnife(Clone)" || item.objectName == "cookingKnife")
                    {
                        o = Instantiate(knife);
                    }
                    else if (item.objectName == "blasterN(Clone)" || item.objectName == "blasterN")
                    {
                        o = Instantiate(gun);
                    }
                    else if (item.objectName == "cup(Clone)" || item.objectName == "cup")
                    {
                        o = Instantiate(cup);
                    }
                    /*else if (item.objectName == "chopstick(Clone)" || item.objectName == "chopstick")
                    {
                        o = Instantiate(target);
                    }*/
                    else 
                    {
                        o = null;
                    }

                    if(o!=null)
                    {
                        o.GetComponent<setRef>().placeholder = placeholder;
                        o.GetComponent<setRef>().propertiesMenu = propMenu;
                        o.GetComponent<setRef>().colourPicker = colour;
                        o.GetComponent<setRef>().colourPickerContainer = container;
                        o.GetComponent<setRef>().distance = item.distance;
                        o.GetComponent<setRef>().navigateTo = item.navigateTo;
                        o.GetComponent<setRef>().manager = manager;
                        o.GetComponent<setRef>().hAnimations = item.handsAnimation;
                        o.GetComponent<setRef>().cAnimations = item.customAnimations;
                        //Debug.Log("3000: " + item.distance + " , " + item.navigateTo);
                    }
                    

                    Renderer renderer = o.GetComponent<Renderer>();
                    Material mat = renderer.material;
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#" + item.colourValue, out color))
                        mat.SetColor("_Color", color);
                    renderer.material = mat;
                    o.transform.position = item.position;
                    o.transform.rotation = item.rotation;
                    o.transform.localScale = item.scale;
                    
                    //placeholder.GetComponent<SceneHandler>().referenceObj = o;


                }
            }
        }
    }




}