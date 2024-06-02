using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_DistractedState : Enemy_SearchingState
{
    public Vector3 DistractionPosition;


    public override void EnterState()
    {
        BaseManager.ArrivedOnWarning = false;

        BaseManager.MoveAgent(DistractionPosition);
        BaseManager.DetectionProgression += BaseManager.WarnedDetectionFill;
        BaseManager.Agent.speed = BaseManager.WalkMoveSpeed;

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
        if (Vector3.Distance(BaseManager.transform.position, DistractionPosition) < 1.2f && !BaseManager.ArrivedOnWarning) //Valeur arbitraire
        {
            BaseManager.ArrivedOnWarning = true;
            BaseManager.StopAgent();
            return;
        }






    }

    public override void UpdateState()
    {

    }
}
