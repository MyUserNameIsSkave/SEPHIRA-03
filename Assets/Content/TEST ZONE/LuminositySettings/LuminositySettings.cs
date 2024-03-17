using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class LuminositySettings : MonoBehaviour
{
    [Range(-0.3f, 0.3f)]
    public float gammaValue = 0;



    private void OnValidate()
    {
        UpdateVolume();
    }


    public void UpdateVolume()
    {
        VolumeProfile profile = GetComponent<Volume>().sharedProfile;

        if (profile.TryGet<LiftGammaGain>(out var llg))
        {
            Vector4 shadowVal = llg.gamma.value;
            llg.gamma.overrideState = true;
            llg.gamma.SetValue(new UnityEngine.Rendering.Vector4Parameter(new Vector4(1, 1, 1, gammaValue)));
        }
    }
}
