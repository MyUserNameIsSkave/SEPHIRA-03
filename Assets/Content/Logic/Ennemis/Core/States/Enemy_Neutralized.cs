using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Neutralized : Enemy_BaseState
{
    public override void AwakeState()
    {

    }

    public override void EnterState()
    {
        Debug.Log("Enter Neutralized");
        BaseManager.Kill();
        BaseManager.animator.SetTrigger("Neutralized");
    }

    public override void ExitState()
    {
        Debug.Log(BaseManager.gameObject + " EXITED NEUTRALIZED STATE");
    }

    public override void FixedUpdateState()
    {
    }

    public override void StartState()
    {
    }

    public override void UpdateState()
    {
    }
}
