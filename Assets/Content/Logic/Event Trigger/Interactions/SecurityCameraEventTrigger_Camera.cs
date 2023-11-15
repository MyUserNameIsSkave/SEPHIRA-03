using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sherbert.Framework.Generic;


public class SecurityCameraEventTrigger_Camera : CameraBase
{

    [SerializeField] private SerializableDictionary<MonoBehaviour, float> dictionnary = new();






    protected override void Transitionned()
    {
        print("SHOULD TRGGER AN EVENT");
    }
}
