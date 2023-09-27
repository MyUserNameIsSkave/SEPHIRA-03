using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class UitlityAI_DebugTool : MonoBehaviour
{
    // ----- SETTINGS VARIABLES -----

    [SerializeField]
    bool drawActionDetectionRadius = true;

    [SerializeField, Tooltip("Heavy on performances")]
    private bool displayScore = true;

    [SerializeField]
    private bool displayPath = true;

    [SerializeField]
    bool logChosenAction = true;

    [SerializeField]
    bool logInitiatives = true;

    [Space (20)]

    public bool logFlee = true;

    [SerializeField]
    bool drawEnemyToFleeDetectionRadius = true;

    [SerializeField]
    bool drawSearchForCoverRadius = true;

    [SerializeField]
    bool drawSearchForHideSpotRadius = true;


    [Space(20)]


    //Prefab
    [SerializeField]
    private GameObject scoreVisualization;

    [SerializeField]
    public LineRenderer lineRendererPrefab;


    //Settings
    [SerializeField]
    public Color pathColor = Color.white;






    // ----- WORKING VARIABLES-----
    [HideInInspector]
    public UtilityAI_Manager UtilityAI_Manager;

    private LineRenderer lineRenderer;
    private List<GameObject> scoresObject = new List<GameObject>();




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            DebugRemoveVizualizers();
        }
    }





    private void OnDrawGizmos()
    {
        if (drawActionDetectionRadius && UtilityAI_Manager.UseDebugTool)
        {
            //Draw Search Zone
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, UtilityAI_Manager.ActionDetectionRadius);
        }

        if (drawEnemyToFleeDetectionRadius && UtilityAI_Manager.UseDebugTool)
        {
            //Draw Search Zone
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, UtilityAI_Manager.DetectEnemyToFleeRadius);
            Gizmos.DrawWireSphere(transform.position, UtilityAI_Manager.MinDistanceBeforeFlee);
        }

        if (drawSearchForCoverRadius && UtilityAI_Manager.UseDebugTool)
        {
            //Draw Search Zone
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, UtilityAI_Manager.SearchCoverRadius);

        }

        if (drawSearchForHideSpotRadius && UtilityAI_Manager.UseDebugTool)
        {
            //Draw Search Zone
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, UtilityAI_Manager.SearchHideSpotRadius);

        }

    }



    public void LogChosedAction(AI_Interaction action)
    {
        if (!logChosenAction)
        {
            return;
        }


        Debug.Log(action.gameObject.name);
    }



    public void DebugScoreVisualizers(AI_Interaction action, float _score)
    {
        if (!displayScore)
        {
            return;
        }



        //Instanciate the Object
        GameObject display = Instantiate(scoreVisualization);
        TextMeshPro text = display.transform.GetChild(0).GetComponent<TextMeshPro>();


        //Set Transform
        display.transform.position = action.transform.position;
        display.transform.rotation = action.transform.rotation;
        display.transform.localScale = Vector3.one;


        //Set Text
        text.SetText(Math.Round(_score, 2).ToString());
        scoresObject.Add(display);
        }







    public void DebugPathVisualizer(AI_Interaction bestAction)
    {
        if (!displayPath)
        {
            return;
        }



        if (lineRenderer != null)
        {
            Destroy(lineRenderer.gameObject);
        }


        NavMeshPath path = new NavMeshPath();


        // Create and set up the Line Renderer
        lineRenderer = Instantiate(lineRendererPrefab);
        lineRenderer.startColor = pathColor;
        lineRenderer.endColor = pathColor;



        if (UtilityAI_Manager.Agent.CalculatePath(bestAction.transform.position, path))
        {
            // Set Line Renderer positions based on path corners
            lineRenderer.positionCount = path.corners.Length;
            lineRenderer.SetPositions(path.corners);
        }
    }



    public void DebugRemoveVizualizers()
    {
        foreach (GameObject score in scoresObject)
        {
            Destroy(score);
        }

        if (lineRenderer != null)
        {
            Destroy(lineRenderer.gameObject);
        }
    }



    public void ShowInitiativeLogs(string toWrite)
    {
        if (!logInitiatives)
        {
            return;
        }


        Debug.Log(toWrite);
    }

}
