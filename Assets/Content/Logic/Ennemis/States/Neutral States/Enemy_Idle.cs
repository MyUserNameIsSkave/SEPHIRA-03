using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : Enemy_NeutralState
{
    public override void EnterState()
    {
        //Stop Moving
        BaseManager.MoveAgentTo(BaseManager.transform.position);
        BaseManager.ChangeAgentSpeed(BaseManager.WalkSpeed);
    }


    #region Useless
    public override void ExitState()
    {

    }
    #endregion


    public override void FixedUpdateState()
    {

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
        //Debug.Log(BaseManager.gameObject.name + " Seen Something");


        //Switch State
        BaseManager.SwitchToInvestingatingState();
    }

    public override void DetectedBinah()
    {
        //Debug.Log(BaseManager.gameObject.name + " Detected Binah");

        //Switch State
        BaseManager.SwitchToChasingState();

    }

    public override void LostBinah()
    {
        //Debug.Log(BaseManager.gameObject.name + " Lost Binah");

    }
}
