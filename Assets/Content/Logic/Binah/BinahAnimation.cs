using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BinahAnimation : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator Animator;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float maxSpeed = agent.speed;
        float currentSpeed = agent.velocity.magnitude;

        Animator.speed = currentSpeed / maxSpeed;
    }



}
