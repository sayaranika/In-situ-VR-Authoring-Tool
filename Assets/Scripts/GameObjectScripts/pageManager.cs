using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spacebear.VRUI;
using UnityEngine.UI;
public class pageManager : MonoBehaviour
{
    int i = 1;

    [SerializeField] private GameObject menu1;
    [SerializeField] private GameObject menu2;
    /*[SerializeField] private GameObject item3;
    [SerializeField] private GameObject item4;
    [SerializeField] private GameObject item5;
    [SerializeField] private GameObject item6;
    [SerializeField] private GameObject item7;
    [SerializeField] private GameObject item8;
    [SerializeField] private GameObject item9;
    [SerializeField] private GameObject item10;
    [SerializeField] private GameObject item11;
    [SerializeField] private GameObject item12;
    [SerializeField] private GameObject item13;
    [SerializeField] private GameObject item14;
    [SerializeField] private GameObject item15;
    [SerializeField] private GameObject item16;
    [SerializeField] private GameObject item17;
    [SerializeField] private GameObject item18;*/
    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        loadIcons();
    }

    public void loadIcons()
    {
        if (i == 1)
        {
            menu2.SetActive(false);
            menu1.SetActive(true);
            
            
            
        }
        else
        {
            menu1.SetActive(false);
            menu2.SetActive(true);
            
        }
    }

        public void nextPressed()
        {
        Debug.Log("900A: Pressed value: " + i);
        if (i == 1) i = 2;
            else i = 1;

        Debug.Log("900B: Pressed value: " + i);
            loadIcons();
        }
    }

