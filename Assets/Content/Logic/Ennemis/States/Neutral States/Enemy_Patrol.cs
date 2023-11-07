using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Patrol : Enemy_NeutralState
{
    private float patrolFrequency = 7f;
    private Vector2 patrolRange = new Vector2 (2f, 3f);

    private int i = 0;

    private float enterTime;


    public override void EnterState()
    {
        Debug.Log("Enemy Enter Patrol ");


        //Stop Moving
        BaseManager.MoveAgentTo(BaseManager.transform.position);
        BaseManager.ChangeAgentSpeed(BaseManager.WalkSpeed);

        enterTime = Time.time;
    }


    #region Useless
    public override void ExitState()
    {
        Debug.Log("Enemy Exite Patrol ");
    }
    #endregion


    public override void FixedUpdateState()
    {
        Debug.Log(enterTime);
        if (i < (Time.time - enterTime) / patrolFrequency)
        {
            i += 1;
            Vector3 nextPosition = new Vector3(Random.Range(0f, 1f) * Random.Range(patrolRange.x, patrolRange.y), 0, Random.Range(0f, 1f) * Random.Range(patrolRange.x, patrolRange.y)) + BaseManager.transform.position;
            BaseManager.MoveAgentTo(nextPosition);
        }
    }


    #region Useless
    public override void UpdateState()
    {
        return;
    }

    public override void HeardSuspectNoise()
    {
        Debug.Log(BaseManager.gameObject.name + " Heard Something");
    }
    #endregion


    public override void SeenSuspectThing()
    {
        Debug.Log(BaseManager.gameObject.name + " Seen Something");


        //Switch State
        if (BaseManager.InvestingatingState != null)
        {
            BaseManager.lastSeenPosition = BaseManager.Binah.transform.position;
            BaseManager.SwitchState(BaseManager.InvestingatingState);
        }
        else
        {
            Debug.LogError("no InvestingatingState set on " + BaseManager.gameObject.name + ", it needs it to work currently");
        }
    }

    public override void DetectedBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Detected Binah");

        //Switch State

        BaseManager.SwitchState(BaseManager.ChasingState);
    }

    public override void LostBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Lost Binah");
    }
}

