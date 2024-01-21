using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatrolRoute : MonoBehaviour
{

    [Header ("    DEBUG SETTINGS")]
    [Space (15)]

    [SerializeField]
    private bool alwaysShowPointsOrder;

    [SerializeField]
    private Color debugColor;



    [Space(30)]
    [Header ("    NE PAS TOUCHER - DEBUG ONLY")]
    [Space (15)]

    public GameObject[] PatrolPoints;
    public bool isAvaliable = true;




    private void Awake()
    {
        UpdatePatrolPoints();
    }



    private void OnDrawGizmos()
    {
        //Variables
        UpdatePatrolPoints();


        //Debug
        if (!alwaysShowPointsOrder)
        {
            return;
        }

        DrawPointOrder();

    }

    private void OnDrawGizmosSelected()
    {
        DrawPointOrder();
    }






    public void DrawPointOrder()
    {
        int length = PatrolPoints.Length;
        int nextIndex = 0;


        for (int i = 0; i < length; i++)
        {
            nextIndex = i + 1;

            if (nextIndex == length)
            {
                nextIndex = 0;
            }



            Gizmos.color = debugColor;
            Gizmos.DrawLine(PatrolPoints[i].transform.position, PatrolPoints[nextIndex].transform.position);
        }
    }

    private void UpdatePatrolPoints()
    {

        int childCount = transform.childCount;

        // Initialisez le tableau avec la taille du nombre d'enfants
        PatrolPoints = new GameObject[childCount];

        // Remplissez le tableau avec les objets enfants
        for (int i = 0; i < childCount; i++)
        {
            PatrolPoints[i] = transform.GetChild(i).gameObject;
        }
    }
}
