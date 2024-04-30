using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Enemy_Struggling : Enemy_AttackingState
{
    public override void EnterState()
    {
        BaseManager.Agent.speed = BaseManager.WalkMoveSpeed;
        BaseManager.MoveAgent(BaseManager.transform.position);
    }

    public void YakuzaWin()
    {
        GameManager.Instance.Respawn();
    }
}
