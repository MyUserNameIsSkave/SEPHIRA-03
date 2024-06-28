using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinahKillKether : MonoBehaviour, IEventTriggerable
{
    public float descendDistance = 5f; // Distance à parcourir sur l'axe Y
    public float descendTime = 2f; // Temps pour parcourir la distance
    public Animator roomAnimator;
    public Animator roomAnimator2;
    public Animator ketherAnimator;

    private Vector3 startPosition; // Position initiale de l'objet

    public void TriggerEvent()
    {
        startPosition = transform.position; // Enregistre la position initiale de l'objet
        StartCoroutine(Descend()); // Démarre la coroutine pour faire descendre l'objet
        ketherAnimator.SetBool("IsFlying", false);
        if (roomAnimator != null && roomAnimator2 != null)
        {
            Destroy(roomAnimator);
            Destroy(roomAnimator2);
        }
    }

    IEnumerator Descend()
    {
        float elapsedTime = 0f; // Temps écoulé depuis le début de la descente

        while (elapsedTime < descendTime)
        {
            elapsedTime += Time.deltaTime; // Incrémente le temps écoulé
            float progress = elapsedTime / descendTime; // Calcule la progression de la descente (entre 0 et 1)

            // Déplace l'objet sur l'axe Y en fonction de la progression
            transform.position = new Vector3(
                startPosition.x,
                 startPosition.y - descendDistance * progress,
                startPosition.z              
            );

            yield return null; // Attend le prochain frame
        }

        // Assure que l'objet soit à la bonne position finale
        transform.position = new Vector3(
            startPosition.x,
             startPosition.y - descendDistance,
            startPosition.z
           
        );
    }
}
