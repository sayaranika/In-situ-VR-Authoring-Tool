using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObjects : MonoBehaviour
{

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform parentObj;

    [SerializeField] private GameObject manager;
    [SerializeField] private GameObject propMenu;
    [SerializeField] private FlexibleColorPicker colour;
    [SerializeField] private GameObject container;

    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject capsule;
    [SerializeField] private GameObject quad;
    [SerializeField] private GameObject cylinder;

    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject apple;
    [SerializeField] private GameObject plate;

    [SerializeField] private GameObject banana;
    [SerializeField] private GameObject chopstick;
    [SerializeField] private GameObject character;



    [SerializeField] private GameObject bed;
    [SerializeField] private GameObject chair;
    [SerializeField] private GameObject bookcase;
    [SerializeField] private GameObject table;

    [SerializeField] private GameObject halfApple;
    [SerializeField] private GameObject knife;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject cup;


    public void spawnHalfApple()
    {
        spawnObject(halfApple);
    }

    public void spawnKnife()
    {
        spawnObject(knife);
    }

    public void spawnGun()
    {
        spawnObject(gun);
    }

    public void spawnCup()
    {
        spawnObject(cup);
    }

    public void spawnBanana()
    {
        spawnObject(banana);
    }

    public void spawnChopstick()
    {
        spawnObject(chopstick);
    }

    

    public void spawnApple()
    {
        spawnObject(apple);
    }

    public void spawnPlate()
    {
        spawnObject(plate);
    }
    public void spawnCube()
    {
        spawnObject(cube);
    }

    public void spawnSphere()
    {
        spawnObject(sphere);
    }

    public void spawnCapsule()
    {
        spawnObject(capsule);
    }

    public void spawnQuad()
    {
        spawnObject(quad);
    }

    public void spawnCylinder()
    {
        spawnObject(cylinder);
    }

    

    public void spawnBed()
    {
        spawnObject(bed);
    }

    public void spawnChair()
    {
        spawnObject(chair);
    }

    public void spawnBookcase()
    {
        spawnObject(bookcase);
    }

    public void spawnTable()
    {
        spawnObject(table);
    }

    public void spawnShield()
    {
        spawnObject(shield);
    }
    private void spawnObject(GameObject obj)
    {
        
        GameObject copy = Instantiate(obj);
        copy.GetComponent<setRef>().placeholder = manager;
        copy.GetComponent<setRef>().propertiesMenu = propMenu;
        copy.GetComponent<setRef>().colourPicker = colour;
        copy.GetComponent<setRef>().colourPickerContainer = container;
        colour.color = Color.white;
        copy.GetComponent<Renderer>().material.color = Color.white;
        copy.transform.parent = parentObj;
        copy.transform.position = spawnPoint.position - new Vector3(0, -0.5f, 0);
        copy.transform.rotation = spawnPoint.rotation;
        copy.transform.up = Vector3.up;
        manager.GetComponent<SceneHandler>().referenceObj = copy;
    }
}
