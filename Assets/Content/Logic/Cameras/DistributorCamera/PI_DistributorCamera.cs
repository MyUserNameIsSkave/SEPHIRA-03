using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PI_DistributorCamera : Player_Interaction
{



    public override void Interaction()
    {
        print("ACTION");
    }

    public override void TriggerEvent()
    {
        print("trigger");
        transform.GetChild(0).GetComponent<CameraBase>().Interaction();

        return;
    }
}
