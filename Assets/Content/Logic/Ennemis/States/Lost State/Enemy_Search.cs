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
            BaseManager.SwitchToNeutralState();
        }
    }


    #region Useless
    public override void UpdateState()
    {
        return;
    }

    public override void HeardSuspectNoise()
    {
        //Debug.Log(BaseManager.gameObject.name + " Heard Something");
    }
    #endregion


    public override void SeenSuspectThing()
    {
        //Debug.Log(BaseManager.gameObject.name + " Seen Something");


        //Switch State
        BaseManager.SwitchToInvestingatingState();
    }

    public override void DetectedBinah()
    {
        //Debug.Log(BaseManager.gameObject.name + " Detected Binah");


        // Switch State
        BaseManager.SwitchToChasingState();
    }

    public override void LostBinah()
    {
        //Debug.Log(BaseManager.gameObject.name + " Lost Binah");

    }
}
