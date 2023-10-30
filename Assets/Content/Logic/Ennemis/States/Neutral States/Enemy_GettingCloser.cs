using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GettingCloser : Enemy_NeutralState
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter GettingCloser ");
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exite GettingCloser ");
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
