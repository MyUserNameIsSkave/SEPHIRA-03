using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Search : Enemy_LostState
{
    private float patrolFrequency = 1f;

    private int i = 0;
    private float enterTime;




    public override void EnterState()
    {
        Debug.Log("Enemy Enter Search ");


        //Enable Detection Decrease
        BaseManager.isLosingInterest = true;

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
        if (i < (Time.time - enterTime) / patrolFrequency)
        {
            i += 1;
            Vector3 nextPosition = new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f)) + BaseManager.transform.position;
            BaseManager.MoveAgentTo(nextPosition);
        }

        if (BaseManager.DetectionProgression == 0)
        {
            BaseManager.SwitchState(BaseManager.NeutralStates[Random.Range(0, BaseManager.NeutralStates.Count)]);
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

        // Switch State
        BaseManager.SwitchState(BaseManager.ChasingState);
    }

    public override void LostBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Lost Binah");

    }
}
