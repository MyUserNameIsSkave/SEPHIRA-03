using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Neutralization : Enemy_InRangeState
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Neutralization ");
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exite Neutralization ");
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
