using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


[RequireComponent(typeof(BoxCollider))]
public class Enemy_VisualDetection : Enemy_BaseDetection
{
    [SerializeField]
    private SphereCollider overallCollider;

    private GameObject binah;


    private void Awake()
    {
        overallCollider = GetComponent<SphereCollider>();
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

        if (Vector3.Distance(transform.position, other.transform.position) >= 4f)
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
        if (Vector3.Distance(transform.position, binah.transform.position) < 4f)
        {
            //Detect Binah
            SeenSomething();
            DetectedBinah();
        }
    }



    //Conditions:
    //  SeenSomething();
    //  DetectedBinah();
    //  LostBinah();
}
