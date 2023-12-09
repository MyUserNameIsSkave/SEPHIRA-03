using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider))]
public class Enemy_VisualDetection : Enemy_BaseDetection
{
    private GameObject binah;


    [SerializeField]
    private float VisualDetectionDistanceThreshold = 1f;

    private void Awake()
    {
        binah = GameObject.FindGameObjectWithTag("Binah");
    }




    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        LostBinah();
    }


    private void OnTriggerStay(Collider other)
    {
     
        if (!other.CompareTag("Binah"))
        {
            return;
        }


        if (Vector3.Distance(transform.position, other.transform.position) >= VisualDetectionDistanceThreshold)
        {
            //See Something
            SeenSomething();
        }
        else
        {
            //Detect Binah
            DetectedBinah();
        }
    }


    private void FixedUpdate()
    {

    }



    //Conditions:
    //  SeenSomething();
    //  DetectedBinah();
    //  LostBinah();
}
