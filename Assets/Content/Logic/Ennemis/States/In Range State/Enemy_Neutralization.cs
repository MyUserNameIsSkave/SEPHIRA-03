using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Neutralization : Enemy_InRangeState
{
    public override void EnterState()
    {
        UtilityAI_Manager binahManager = BaseManager.BinahManager;
        binahManager.GetNeutralized();

        Debug.Log("Start Struggling with Binah");
    }

    public override void ExitState()
    {

    }

    public override void FixedUpdateState()
    {
        return;
    }

    public override void UpdateState()
    {
        return;
    }
}


