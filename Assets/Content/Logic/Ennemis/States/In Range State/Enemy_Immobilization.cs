using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Enemy_Immobilization : Enemy_InRangeState
{
    public override void EnterState()
    {
        UtilityAI_Manager binahManager = BaseManager.BinahManager;
        binahManager.SwitchState(binahManager.ImmobilizedState);

        BaseManager.MoveAgentTo(BaseManager.transform.position);
        Debug.Log("IMMOBILIZING PLAYER");
    }




    #region Useless
    public override void ExitState()
    {

    }

    public override void FixedUpdateState()
    {
        return;
    }

    public override void UpdateState()
    {
        return;
    }



    public override void HeardSuspectNoise()
    {
        //Debug.Log(BaseManager.gameObject.name + " Heard Something");

    }

    public override void SeenSuspectThing()
    {
        //Debug.Log(BaseManager.gameObject.name + " Seen Something");

    }

    public override void DetectedBinah()
    {
        //Debug.Log(BaseManager.gameObject.name + " Detected Binah");

    }

    public override void LostBinah()
    {
        //Debug.Log(BaseManager.gameObject.name + " Lost Binah");

    }
    #endregion
}
