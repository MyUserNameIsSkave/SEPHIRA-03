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
        
        //Stop Moving
        BaseManager.MoveAgentTo(BaseManager.transform.position);
        BaseManager.ChangeAgentSpeed(BaseManager.WalkSpeed);

        enterTime = Time.time;
    }


    #region Useless
    public override void ExitState()
    {

    }
    #endregion


    public override void FixedUpdateState()
    {
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



        //Faudrait que l'IA continue sa ronde tant que son niveau de suspicion n'est pas assez haut


        //Switch State
        BaseManager.SwitchToInvestingatingState();

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

