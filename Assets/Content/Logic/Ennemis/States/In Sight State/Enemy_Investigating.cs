using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Enemy_Investigating : Enemy_InSightState
{
    public override void EnterState()
    {
        //Prevent bug
        BaseManager.DetectionProgression += BaseManager.detectionRate * Time.deltaTime;
        BaseManager.isLosingInterest = false;

        //Change MoveSpeed
        BaseManager.ChangeAgentSpeed(BaseManager.WalkSpeed);

        //Move to last seen position
        BaseManager.lastSeenPosition = BaseManager.Binah.transform.position;
        BaseManager.MoveAgentTo(BaseManager.lastSeenPosition);

    }


    #region Useless
    public override void ExitState()
    {

    }
    #endregion


    public override void FixedUpdateState()
    {
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
        

        //Disable Detection Decrease
        BaseManager.isLosingInterest = false;

        //Increase Detection Progression
        BaseManager.DetectionProgression += BaseManager.detectionRate * Time.deltaTime;

        //Move Agent
        BaseManager.lastSeenPosition = BaseManager.Binah.transform.position;
        BaseManager.MoveAgentTo(BaseManager.lastSeenPosition);
    }

    public override void DetectedBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Detected Binah");


        //Get the State List minus itself
        List<Enemy_InSightState> inSightStates = new List<Enemy_InSightState>(BaseManager.InSightStates);
        inSightStates.Remove(this);

        //Switch State
        BaseManager.SwitchState(inSightStates[Random.Range(0, inSightStates.Count)]);


    }



    public override void LostBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Lost Binah");


        //Enable Detection Decrease
        BaseManager.isLosingInterest = true;
    }
    

}
