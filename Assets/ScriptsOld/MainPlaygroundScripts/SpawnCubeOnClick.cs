using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubeOnClick : MonoBehaviour
{
    public Transform spawnPoint;
    [SerializeField] private GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>().OnClick.AddListener(spawnCube);
    }

    public void spawnCube()
    {
        GameObject copy = Instantiate(cube);
        copy.transform.position = spawnPoint.position;
        copy.transform.rotation = spawnPoint.rotation;
        copy.transform.up = Vector3.up;
        //copy.transform.localScale = spawnPoint.localScale;

        RotationAxisConstraint rotationConstraint = copy.GetComponent<RotationAxisConstraint>();
        rotationConstraint.ConstraintOnRotation = Microsoft.MixedReality.Toolkit.Utilities.AxisFlags.XAxis | Microsoft.MixedReality.Toolkit.Utilities.AxisFlags.ZAxis;
    }
}
