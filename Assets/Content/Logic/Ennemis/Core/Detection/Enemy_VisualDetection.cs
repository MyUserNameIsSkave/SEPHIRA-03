using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


[RequireComponent(typeof(SphereCollider))]
public class Enemy_VisualDetection : Enemy_BaseDetection
{
    [SerializeField]
    private SphereCollider overallCollider;




    private void Awake()
    {
        overallCollider = GetComponent<SphereCollider>();
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

        if (Vector3.Distance(transform.position, other.transform.position) >= overallCollider.radius / 2)
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




    //Conditions:
    //  SeenSomething();
    //  DetectedBinah();
    //  LostBinah();
}
