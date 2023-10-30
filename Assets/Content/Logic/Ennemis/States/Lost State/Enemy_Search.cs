using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Search : Enemy_LostState
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Search ");
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exite Search ");
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
