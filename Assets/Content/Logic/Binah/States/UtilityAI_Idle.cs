using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;


public class UtilityAI_Idle : UtilityAI_BaseState
{
    // ----- WORKING VARIABLES -----
    private float inactivityStartTime;
    private float timeLeftBeforeInitiative;

    private AI_Interaction bestAction;












    // ---------- INHERITED METHODS ----------


    /// <summary>
    /// The IA is not doing anything
    /// </summary>
    public override void EnterState()
    {
        inactivityStartTime = Time.time;
        UtilityAI_Manager.CanRecieveInput = true;

    }

    public override void ExitState()
    {

    }
    


    public override void FixedUpdateState()
    {

    }

    public override void UpdateState()
    {

    }



    public override void CustomUdpateState()
    {
        //TEMPORAIRE EN ATTENDANT LA MISE ENPLACE D'UN HIDING STATE FONCTIONNEL
        if (UtilityAI_Manager.isHidenBehindCover) 
        {
            return;
        }

        CheckIfInDanger();
        CheckforInitiative();
    }





    // ---------- CUSTOM METHODS ----------



    /// <summary>
    /// Called in CustomUpdate to Check the Danger. If in Danger, switch to Flee State
    /// </summary>
    private void CheckIfInDanger()
    {
        //Time Constraint

        if (UtilityAI_Manager.IdleTimeBeforeFlee == 0)
        {
            return;
        }

        if (UtilityAI_Manager.IdleTimeBeforeFlee >= Time.time - inactivityStartTime)
        {
            #region debug
            if (UtilityAI_Manager.UseDebugTool && UtilityAI_Manager.DebugTool.logFlee)
            {
                Debug.Log("To Early to Flee, Idle Time Remaning : " + Mathf.Abs(UtilityAI_Manager.IdleTimeBeforeFlee - (Time.time - inactivityStartTime)));
            }
            #endregion

            return;
        }

        #region debug
        if (UtilityAI_Manager.UseDebugTool && UtilityAI_Manager.DebugTool.logFlee)
        {
            Debug.Log("Looking for Danger");
        }
        #endregion







        //Get All Colliding Object
        Collider[] colliders = Physics.OverlapSphere(UtilityAI_Manager.Object.transform.position, UtilityAI_Manager.DetectEnemyToFleeRadius, LayerMask.GetMask("Enemy"));

        //UtilityAI_Manager.EnemyToFlee.Clear();

        foreach (Collider collider in colliders)
        {
            Vector3 enemyPosition = collider.gameObject.transform.position;
            Vector3 playerPosition = UtilityAI_Manager.Object.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(playerPosition, (enemyPosition - playerPosition).normalized, out hit))
            {

                //Direct Sight
                if (hit.collider.CompareTag("Enemy"))
                {
                    UtilityAI_Manager.EnemyToFlee.Add(collider.gameObject);
                    Debug.Log("Vue direct");
                }
            }            
        }

        //Start To Flee
        if (UtilityAI_Manager.EnemyToFlee.Count != 0)
        {
            
            UtilityAI_Manager.SwitchState(UtilityAI_Manager.FleeState);
        }
    }







    /// <summary>
    /// Called on CustomUpdate and check if the IA should search for an action to do by itself
    /// </summary>
    void CheckforInitiative()
    {
        //Point Check
        float independence = UtilityAI_Manager.StateVariablesDictionnary.StateVariables[3].Value;

        if (independence < UtilityAI_Manager.MinIndependenceForInitiative)
        {
            return;
        }



        //Time Check
        timeLeftBeforeInitiative = UtilityAI_Manager.IndependenceCurve.Evaluate(independence);


        //if (UtilityAI_Manager.UseDebugTool)
        //{
        //    UtilityAI_Manager.DebugTool.ShowInitiativeLogs("Trop tot pour prendre une initiative, temps restant : " + (Mathf.Abs((Time.time - inactivityStartTime) - timeLeftBeforeInitiative)).ToString());
        //}



        if (timeLeftBeforeInitiative <= Time.time - inactivityStartTime)
        {
            SearchForAction();
        }
    }



    /// <summary>
    /// Detect all Action in a Radius
    /// </summary>
    void SearchForAction()
    { 
        //Get All Colliding Object
        Collider[] colliders = Physics.OverlapSphere(UtilityAI_Manager.Object.transform.position, UtilityAI_Manager.ActionDetectionRadius, UtilityAI_Manager.AI_InteractionObject);

        List<AI_Interaction> derivedScripts = new List<AI_Interaction>();

        foreach (Collider collider in colliders)
        {
            AI_Interaction derivedScript = collider.GetComponent<AI_Interaction>();
            if (derivedScript != null)
            {
                derivedScripts.Add(derivedScript);
            }
        }

        //Look for best action
        DecideBestAction(derivedScripts.ToArray());
    }




    /// <summary>
    /// Chose the Best Action to Do
    /// </summary>
    void DecideBestAction(AI_Interaction[] actionAvaliable)
    {
            #region Debug
        if (UtilityAI_Manager.UseDebugTool)
        {
            UtilityAI_Manager.DebugTool.DebugRemoveVizualizers();
        }
            #endregion



        float score = 0f;
        int bestActionIndex = -1;

        for (int i = 0; i < actionAvaliable.Length; i++)
        {
            //Varaible Temporaire pour réduire le nombre d'appel de ScoreAction()
            float calculatedScore = ScoreDetectedActions(actionAvaliable[i]);

            if (calculatedScore > score)
            {
                bestActionIndex = i;
                score = calculatedScore;
            }
        }

        //Stop If No Desirable Action Found
        if (bestActionIndex == -1 || score < UtilityAI_Manager.IndependenceMinScore )
        {
            return;
        }



        bestAction = actionAvaliable[bestActionIndex];
        ExecuteAction(bestAction);
    }




    /// <summary>
    /// Call Scoring Logic
    /// </summary>
    public float ScoreDetectedActions(AI_Interaction action)
    {
        float score = RunThroughConsideration(false, action);



            #region Debug
        if (UtilityAI_Manager.UseDebugTool)
        {
            UtilityAI_Manager.DebugTool.DebugScoreVisualizers(action, score);
        }
            #endregion



        return score;
    }




    /// <summary>
    /// Call Scoring Logic
    /// </summary>
    public void ScoreIndicatedAction(AI_Interaction action)
    {
        float score = RunThroughConsideration(true, action);


            #region Debug
        if (UtilityAI_Manager.UseDebugTool)
        {
            UtilityAI_Manager.DebugTool.DebugRemoveVizualizers();
            UtilityAI_Manager.DebugTool.DebugScoreVisualizers(action, score);
        }
            #endregion


        //Execute l'action si le Threshold est dépassé
        if (score >= UtilityAI_Manager.IndicatedActionMinScore)
        {
            ExecuteAction(action);
        }
        else
        {
            action.ActionRefused(action);
            SearchForAction();
        }
    }




    /// <summary>
    /// Scoring Logic, use Consideration to define an Action Score between 0 & 1, called for AI Detected Action and Player Indicated Action
    /// </summary>
    private float RunThroughConsideration(bool IgnorPathLength, AI_Interaction action)
    {
        bool distanceEvaluated;

        if (IgnorPathLength)
        {
            distanceEvaluated = true;
        }
        else
        {
            distanceEvaluated = false;

        }



        float minScore = action.ScoreRange.x;
        float maxScore = action.ScoreRange.y;


        float score = 0f;
        Dictionary<string, AnimationCurve> variableSettings = action.Consideration.VariablesSettings;


        foreach (KeyValuePair<string, AnimationCurve> attachStat in variableSettings)
        {
            for (int i = 0; i < UtilityAI_Manager.StateVariablesDictionnary.StateVariables.Length; i++)
            {

                //Acount for distance
                if (attachStat.Key == "Distance" && !distanceEvaluated)
                {
                    //Prevent the distance being acounted multiple time 
                    distanceEvaluated = true;

                    score -= attachStat.Value.Evaluate(CalculatePathLength(action) / UtilityAI_Manager.MaxPathLength);
                }

                //Account for Stat Variables
                if (attachStat.Key == UtilityAI_Manager.StateVariablesDictionnary.StateVariables[i].Name)
                {
                    score += attachStat.Value.Evaluate(UtilityAI_Manager.StateVariablesDictionnary.StateVariables[i].Value / 100f);
                }
            }
        }
        //Normalized Score
        score = (score - minScore) / (maxScore - minScore);
        score = Mathf.Clamp01(score);

        return score;
    }




    /// <summary>
    /// Get the length of the path to the action to take it into account for the score
    /// </summary>
    private float CalculatePathLength(AI_Interaction action)
    {
        //Variables
        Vector3 endPoint = action.transform.position;
        NavMeshPath path = new NavMeshPath();

        float pathLength = 0f;



        if (UtilityAI_Manager.Agent.CalculatePath(endPoint, path))
        {
            //Add up each segments
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                pathLength += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
        }

        return Mathf.Clamp(pathLength, 0, UtilityAI_Manager.MaxPathLength);
    }




    /// <summary>
    /// Ask the UtilityAI_Action script to execute the selected Action
    /// </summary>
    public void ExecuteAction(AI_Interaction action)
    {
            #region Debug
        if (UtilityAI_Manager.UseDebugTool)
        {
            UtilityAI_Manager.DebugTool.LogChosedAction(action);
            UtilityAI_Manager.DebugTool.DebugPathVisualizer(action);
        }
        #endregion


        UtilityAI_Manager.ActionToDo = action;
        
        //Set State to DoingActionState
        UtilityAI_Manager.SwitchState(UtilityAI_Manager.DoingActionState);
    }

}

