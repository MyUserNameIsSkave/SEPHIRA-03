using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PI_EnemyDistraction : Player_Interaction
{
    [SerializeField]
    private LayerMask layerToDetect;

    [SerializeField]
    private float detectionRadius = 5f;

    [SerializeField, Tooltip("That value is added to the detection of the Enemy. The higher the value the longer the enemy looks at the Distraction, the enemy leaving distraction State when his detection value get to 0")]
    private float Suspiciousness = 20f;

    [SerializeField]
    private float cooldown = 5f;

    private bool underCooldown = false;

    public override void Interaction()
    {
        if (underCooldown)
        {
            return;
        }

        StartCoroutine(DistractEnemies());
    }


    IEnumerator DistractEnemies()
    {
        underCooldown = true;

        GetComponent<AudioSource>().Play();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, layerToDetect);


        //Simple delai pour rendre la réaction du Yakuza moins vive
        yield return new WaitForSeconds(0.5f);


        foreach (Collider hitCollider in hitColliders)
        {
            Enemy_BaseManager manager = hitCollider.GetComponent<Enemy_BaseManager>();

            if (manager.CurrentState == manager.IdleState || manager.CurrentState == manager.PatrolState || manager.CurrentState == manager.LostState)
            {
                manager.DetectionProgression += Suspiciousness;
                manager.DistractedState.DistractionPosition = transform.position;
                manager.SwitchState(manager.DistractedState);
            }
        }

        yield return new WaitForSeconds(cooldown);

        underCooldown = false;

    }




    // Affiche la sphère de détection dans l'éditeur pour la visualisation
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }


    public override void TriggerEvent()
    {
        return; 
    }
}



