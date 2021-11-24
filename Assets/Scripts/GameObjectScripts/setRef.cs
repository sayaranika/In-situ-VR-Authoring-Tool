using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setRef : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject propertiesMenu;
    [SerializeField] public GameObject placeholder;
    [SerializeField] public GameObject colourPickerContainer;
    [SerializeField] public FlexibleColorPicker colourPicker;

    [SerializeField] public GameObject manager;




    [SerializeField] public float distance;
    public int navigateTo;
    public HandsAnimation hAnimations;
    public List<CustomAnimation> cAnimations;

    private bool isManipulated = false;
    private int i = 0;
    void Start()
    {
        isManipulated = false;
             
    }

    
    void Update()
    {
        if(isManipulated)
        {
            BoundsControl bc = gameObject.GetComponent<BoundsControl>();
            propertiesMenu.transform.position = bc.transform.position + new Vector3(0, bc.transform.lossyScale.y + 0.1f, 0);
          
            

            colourPickerContainer.transform.position = new Vector3(gameObject.transform.position.x, bc.transform.position.y+ bc.transform.lossyScale.y + 0.5f, bc.transform.position.z);

            //animationPanel.transform.position = new Vector3(gameObject.transform.position.x, bc.transform.position.y + bc.transform.lossyScale.y + 0.5f, bc.transform.position.z);


        }
  
    }

    public void setManipulated()
    {
        if (i == 0)
        {
            gameObject.transform.parent = null;
            i++;
        }

        isManipulated = true;

        placeholder.GetComponent<SceneHandler>().referenceObj = gameObject;
        colourPicker.color = gameObject.GetComponent<Renderer>().material.color;


        propertiesMenu.transform.GetChild(0).gameObject.SetActive(true);
       // manager.GetComponent<openColorPalette>().closeAnimationPanel();
    }

    public void resetManipulated()
    {
        isManipulated = false;
        propertiesMenu.transform.GetChild(0).gameObject.SetActive(true);
        colourPickerContainer.SetActive(false);
        //animationPanel.SetActive(false);
        //manager.GetComponent<openColorPalette>().closeAnimationPanel();

    }
}
