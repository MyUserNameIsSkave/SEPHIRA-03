using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PI_Level1Transition_Fan : Player_Interaction
{
    public override void Interaction()
    {
        GetComponent<BoxCollider>().enabled = false;
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("stop");
    }

    public override void TriggerEvent()
    {
        //throw new System.NotImplementedException();
    }
}
