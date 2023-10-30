using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : Enemy_NeutralState
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Idle ");
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exite Idle ");
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
