using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IO_Objective : InteractiveObject
{
    // ----- VARIABLES -----

    [Space (15f)]
    [Header ("     OBJECT ACTION")]
    [Space(7f)]

    [SerializeField]
    private Outcomes detectionOutcome;
    public enum Outcomes
    {
        None,
        Reset
    };



    // ----- INTERFACE METHODS -----

    public override void Interaction()
    {
        switch (detectionOutcome)
        {
            case Outcomes.None:
                Debug.Log("You've reached the Objective !");
                break;

            case Outcomes.Reset:
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }

}
