using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PI_Exemple : Player_Interaction
{
    public override void Interaction()
    {
        Debug.Log("Action Executed");
    }

    public override void TriggerEvent()
    {
        return;
    }
}
