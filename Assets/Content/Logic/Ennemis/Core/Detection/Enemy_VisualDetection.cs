using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


[RequireComponent(typeof(SphereCollider))]
public class Enemy_VisualDetection : Enemy_BaseDetection
{
    [SerializeField]
    private SphereCollider overalCollision;




    private void Awake()
    {
        overalCollision = GetComponent<SphereCollider>();
    }




    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }





    //Conditions:
    //  SeenSomething();
    //  DetectedBinah();
    //  LostBinah();
}
