using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Struggling : Enemy_InRangeState
{
    public override void EnterState()
    {
        UtilityAI_Manager binahManager = BaseManager.BinahManager;
        binahManager.StartStruggling();

        BaseManager.MoveAgentTo(BaseManager.transform.position);
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
