using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chasing : Enemy_InSight
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Chasing ");
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exite Chasing ");
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
