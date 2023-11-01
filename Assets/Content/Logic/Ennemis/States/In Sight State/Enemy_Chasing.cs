using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chasing : Enemy_InSight
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Chasing ");
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exite Chasing ");
    }

    public override void FixedUpdateState()
    {
        return;
    }

    public override void UpdateState()
    {
        return;
    }




    public override void HeardSuspectNoise()
    {
        Debug.Log(BaseManager.gameObject.name + " Heard Something");
    }

    public override void SeenSuspectThing()
    {
        Debug.Log(BaseManager.gameObject.name + " Seen Something");

    }

    public override void DetectedBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Detected Binah");

    }

    public override void LostBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Lost Binah");

    }
}
