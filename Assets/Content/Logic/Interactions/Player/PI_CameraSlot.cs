using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PI_CameraSlot : Player_Interaction
{
    public override void Interaction()
    {

    }

    public override void SelectedByPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ClickOnCamera(gameObject);
    }
}
