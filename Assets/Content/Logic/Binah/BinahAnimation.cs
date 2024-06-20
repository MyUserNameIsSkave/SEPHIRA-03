using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BinahAnimation : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;
    private UtilityAI_Manager utilityAI_Manager;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        utilityAI_Manager = GetComponent<UtilityAI_Manager>(); 
    }

    private void Update()
    {
        if (animator.runtimeAnimatorController == null || animator.runtimeAnimatorController.name != "Binah")
        {
            return;
        }
        float maxSpeed = agent.speed;
        float currentSpeed = agent.velocity.magnitude;

        animator.SetFloat("Speed", currentSpeed / maxSpeed);
        animator.SetBool("IsCrouching", utilityAI_Manager.isCrouched);
   
    }



}
