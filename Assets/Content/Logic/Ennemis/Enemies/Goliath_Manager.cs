using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goliath_Manager : Enemy_BaseManager
{
    private void Awake()
    {
        BaseAwake();
    }


    private void Start()
    {
        //Set the base state
        SwitchState(IdleState);
    }


    private void Update()
    {
        BaseUpdate();


        if (Input.GetKeyDown(KeyCode.Alpha1) && NeutralStates.Count != 0)
        {
            SwitchState(NeutralStates[Random.Range(0, NeutralStates.Count)]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && InSightStates.Count != 0)
        {
            SwitchState(InSightStates[Random.Range(0, InSightStates.Count)]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && InRangeStates.Count != 0)
        {
            SwitchState(InRangeStates[Random.Range(0, InRangeStates.Count)]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && LostStates.Count != 0)
        {
            SwitchState(LostStates[Random.Range(0, LostStates.Count)]);
        }
    }

    private void FixedUpdate()
    {
        BaseFixedUpdate();
    }

}