using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Warning : Enemy_InSight
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Warning ");
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exite Warning ");
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
