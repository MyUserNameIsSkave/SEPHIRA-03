using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AII_Exemple : AI_Interaction
{
    public override void Interaction()
    {
        if (CheckStamina())
        {
            Debug.Log("Action Executed");
        }
        else
        {
            Debug.Log("Action Failed");
        }
    }

}
