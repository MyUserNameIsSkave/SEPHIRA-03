using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Warning : Enemy_InRangeState
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Warning ");
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exite Warning ");
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
