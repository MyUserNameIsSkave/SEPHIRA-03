using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Light : InteractiveObject
{
    // ----- INTERFACE METHODS -----



    public override void Interaction()
    {
        GetComponentInChildren<Light>().enabled = !GetComponentInChildren<Light>().enabled;


        ModifyStateVariables();
    }

}
