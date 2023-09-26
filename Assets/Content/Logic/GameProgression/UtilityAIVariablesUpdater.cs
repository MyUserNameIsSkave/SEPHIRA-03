using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAIVariablesUpdater : MonoBehaviour
{
    // ---------- VARIABLES ----------

    [Space(15)]
    [Header("     STATE VARIABLES")]
    [Space(7)]

    public ModificationType modificationType;
    public enum ModificationType
    {
        None,
        Add,
        Set
    }


    public StateVariablesModification variablesModification;
    private UtilityAI_Manager UtilityAI_Manager;





    // ---------- LOGIC ----------

    private void Start()
    {
        UtilityAI_Manager = FindAnyObjectByType<UtilityAI_Manager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Apply Modification
            ModifyStateVariables();


            //Destroy
            Destroy(gameObject);
        }
    }



    [Space(15)]
    [Header("     AI VARIABLES")]
    [Space(7)]

    [Space(15)]
    [Header("ACTION")]
    [Space(7)]



    [SerializeField]
    private bool changeActionDetectionradius = false;
    [SerializeField]
    private float newActionDetectionradius;

    [Space(10)]

    [SerializeField]
    private bool changeMaxPathLength = false;
    [SerializeField]
    private float newMaxPathLength;

    [Space(10)]

    [SerializeField]
    private bool changeInidcatedActionMinScore;
    [SerializeField]
    private float newInidcatedActionMinScore;


    [Space(15)]
    [Header("INDEPENDENCE")]
    [Space(7)]

    [SerializeField]
    private bool changeIndependenceCurve;
    [SerializeField, Tooltip("From 0 to 100")]
    private AnimationCurve newIndependenceCurve;

    [Space(10)]

    [SerializeField]
    private bool changeMinIndependenceInitiative;
    [SerializeField]
    private float newMinIndependenceInitiative;

    [Space(10)]

    [SerializeField]
    private bool changeIndependenceMinScore;
    [SerializeField]
    private float newIndependenceMinScore;





    [Space(15)]
    [Header("FLEE")]
    [Space(7)]

    [Header("     General")]

    [SerializeField]
    private bool changeDetectEnemyToFleeRadius;
    [SerializeField]
    private float newDetectEnemyToFleeRadius;


    [Space(10)]

    [SerializeField]
    private bool changeMinDistanceBeforeFlee;
    [SerializeField]
    private float newMinDistanceBeforeFlee;


    [Space(10)]
    [Header("     Hide")]

    [SerializeField]
    private bool changeSearchHideSpotRadius;
    [SerializeField]
    private float newSearchHideSpotRadius;

    [Space(10)]

    [SerializeField]
    private bool changeSearchDensity;
    [SerializeField]
    private float newSearchDensity;



    [Space(10)]
    [Header("     Cover")]

    [SerializeField]
    private bool changeSearchCoverRadius;
    [SerializeField]
    private float newSearchCoverRadius;



    [Space(10)]
    [Header("     Flee")]

    [SerializeField]
    private bool changeFleeDistance;
    [SerializeField]
    private float newFleeDistance;




    /// <summary>
    /// Method to use in order to modify the State Variables Values and other AI's Variables
    /// </summary>
    public void ModifyStateVariables()
    {
        if (modificationType == ModificationType.None)
        {
            //None
            return;
        }

        if (modificationType == ModificationType.Add)
        {
            //Add
            UtilityAI_Manager.StateVariablesDictionnary.AddStateVariables(variablesModification.Courage,
                                                                          variablesModification.Determination,
                                                                          variablesModification.Curiosity,
                                                                          variablesModification.Independence);

        }

        if (modificationType == ModificationType.Set)
        {
            //Set
            UtilityAI_Manager.StateVariablesDictionnary.SetStateVariables(variablesModification.Courage,
                                                                          variablesModification.Determination,
                                                                          variablesModification.Curiosity,
                                                                          variablesModification.Independence);
        }





        //Action Settings
        if (changeActionDetectionradius)
            UtilityAI_Manager.ActionDetectionRadius = newActionDetectionradius;

        if (changeMaxPathLength)
            UtilityAI_Manager.MaxPathLength = newMaxPathLength;

        if (changeInidcatedActionMinScore)
            UtilityAI_Manager.IndicatedActionMinScore = newInidcatedActionMinScore;



        //Independence Settings
        if (changeIndependenceCurve)
            UtilityAI_Manager.IndependenceCurve = newIndependenceCurve;

        if (changeMinIndependenceInitiative)
            UtilityAI_Manager.MinIndependenceForInitiative = newMinIndependenceInitiative;

        if (changeIndependenceMinScore)
            UtilityAI_Manager.IndependenceMinScore = newIndependenceMinScore;



        //Flee Settings
        if (changeDetectEnemyToFleeRadius)
            UtilityAI_Manager.DetectEnemyToFleeRadius = newDetectEnemyToFleeRadius;

        if (changeMinDistanceBeforeFlee)
            UtilityAI_Manager.MinDistanceBeforeFlee = newMinDistanceBeforeFlee;

        if (changeSearchHideSpotRadius)
            UtilityAI_Manager.SearchHideSpotRadius = newSearchHideSpotRadius;

        if (changeSearchDensity)
            UtilityAI_Manager.SearchDensity = newSearchDensity;


        if (changeSearchCoverRadius)
            UtilityAI_Manager.SearchCoverRadius = newSearchCoverRadius;

        if (changeFleeDistance)
            UtilityAI_Manager.FleeDistance = newFleeDistance;
    }
}
