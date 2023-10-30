using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : Enemy_NeutralState
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Patrol ");
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exite Patrol ");
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
