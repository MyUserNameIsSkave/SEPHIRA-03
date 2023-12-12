using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_GettingCloser : Enemy_NeutralState
{

    public override void EnterState()
    {
        BaseManager.ChangeAgentSpeed(BaseManager.WalkSpeed);
    }


    #region Useless
    public override void ExitState()
    {

    }
    #endregion


    public override void FixedUpdateState()
    {
        // SE RENSEIGNER SUR "NavMesh.SamplePosition" POUR TROUVER UNE POSITION VALIDE PROCHE
        BaseManager.MoveAgentTo(BaseManager.Binah.transform.position);
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
        //Switch State
        BaseManager.SwitchToInvestingatingState();

    }


    #region Useless
    public override void DetectedBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Detected Binah");
    }

    public override void LostBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Lost Binah");

    }
    #endregion
}
