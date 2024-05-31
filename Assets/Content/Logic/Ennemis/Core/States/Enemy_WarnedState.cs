using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WarnedState : Enemy_SearchingState
{
    public Vector3 WarningPosition;




    public override void EnterState()
    {
        BaseManager.animator.SetTrigger("StartRunning");
        BaseManager.ArrivedOnWarning = false;

        BaseManager.LastKnownPosition = WarningPosition;
        BaseManager.DetectionProgression += BaseManager.WarnedDetectionFill;

        BaseManager.Agent.speed = BaseManager.RunMoveSpeed;
        BaseManager.MoveAgent(WarningPosition);
    }

    public override void ExitState()
    {
        BaseManager.animator.SetTrigger("StopRunning");
    }





    public override void AwakeState()
    {

    }

    public override void StartState()
    {

    }



    public override void FixedUpdateState()
    {
        if (Vector3.Distance(BaseManager.transform.position, WarningPosition) > 3f) //Valeur arbitraire
        {
            return;
        }

        BaseManager.ArrivedOnWarning = true;
        BaseManager.StopAgent();

    }

    public override void UpdateState()
    {

    }
}
