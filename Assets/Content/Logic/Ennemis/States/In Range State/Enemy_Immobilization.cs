using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Immobilization : Enemy_InRangeState
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Immobilization ");
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exite Immobilization ");
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
