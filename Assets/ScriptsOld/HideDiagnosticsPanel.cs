using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDiagnosticsPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CoreServices.DiagnosticsSystem.ShowDiagnostics = false;

        CoreServices.DiagnosticsSystem.ShowProfiler = false;
    }
}
