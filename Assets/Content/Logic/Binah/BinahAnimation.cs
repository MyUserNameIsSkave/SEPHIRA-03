using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BinahAnimation : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator Animator;
    private UtilityAI_Manager utilityAI_Manager;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        utilityAI_Manager = GetComponent<UtilityAI_Manager>(); 
    }

    private void Update()
    {
        float maxSpeed = agent.speed;
        float currentSpeed = agent.velocity.magnitude;

        Animator.SetFloat("Speed", currentSpeed / maxSpeed);
        Animator.SetBool("IsCrouching", utilityAI_Manager.isCrouched);
    }



}
