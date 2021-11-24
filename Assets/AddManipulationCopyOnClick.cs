using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddManipulationCopyOnClick : MonoBehaviour
{
    public Transform spawnPoint;
    

    // Start is called before the first frame update
    void Start()
    {

        GetComponent<Interactable>().OnClick.AddListener(Copy);
    }

    public void Copy()
    {
        GameObject copy = Instantiate(gameObject);
        copy.transform.position = spawnPoint.position;
        copy.transform.rotation = spawnPoint.rotation;
        copy.transform.up = Vector3.up;
        copy.transform.localScale = spawnPoint.localScale;

        copy.AddComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>();
        BoundingBox boundingBox = copy.AddComponent<BoundingBox>();
        boundingBox.BoundingBoxActivation = BoundingBox.BoundingBoxActivationType.ActivateByProximityAndPointer;
        boundingBox.ShowWireFrame = false;


        RotationAxisConstraint rotationConstraint = copy.AddComponent<RotationAxisConstraint>();
        rotationConstraint.ConstraintOnRotation = Microsoft.MixedReality.Toolkit.Utilities.AxisFlags.XAxis | Microsoft.MixedReality.Toolkit.Utilities.AxisFlags.ZAxis;

        Destroy(copy.GetComponent<Interactable>());
        Destroy(copy.GetComponent<AddManipulationCopyOnClick>());
    }
}
