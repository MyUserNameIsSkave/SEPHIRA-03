using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Search : Enemy_LostState
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Search ");


        //Enable Detection Decrease
        BaseManager.isLosingInterest = true;

    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exite Search ");
    }

    public override void FixedUpdateState()
    {
        if (BaseManager.DetectionProgression == 0)
        {
            BaseManager.SwitchState(BaseManager.NeutralStates[Random.Range(0, BaseManager.NeutralStates.Count)]);
        }
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

        // Switch State
        // CHASE
    }

    public override void LostBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Lost Binah");

    }
}
