using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_IdleState : Enemy_NeutralState
{
    public override void EnterState()
    {
        BaseManager.Agent.speed = BaseManager.WalkMoveSpeed;
        BaseManager.MoveAgent(BaseManager.InitialPosition);
    }

    public override void ExitState()
    {

    }





    public override void AwakeState()
    {

    }

    public override void StartState()
    {

    }
    public override void FixedUpdateState()
    {

    }

    public override void UpdateState()
    {

    }
}