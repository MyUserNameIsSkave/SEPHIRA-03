using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PI_StrugglingPoint : Player_Interaction
{
    [HideInInspector]
    public bool triggered = false;


    public override void Interaction()
    {
        GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        GetComponent<SphereCollider>().enabled = false;

        triggered = true;
    }

    public override void TriggerEvent()
    {
        return;
    }
}
