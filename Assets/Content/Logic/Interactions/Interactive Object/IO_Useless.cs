using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Useless : InteractiveObject
{
    // ----- INTERFACE METHODS -----

    public override void Interaction()
    {
        Debug.Log("Vous avez effectu� une action inutile");

        ModifyStateVariables();
    }
}
