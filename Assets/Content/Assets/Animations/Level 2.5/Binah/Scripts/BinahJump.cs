using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BinahJump : MonoBehaviour, IEventTriggerable
{
    public GameObject Binah;
    public Vector3 targetPosition;
    public Vector3 intermediateDestination;
    public float delayTime = 2f;
    public float moveDuration1 = 3f;
    public float moveDuration2 = 3f;
    public RuntimeAnimatorController firstAnimatorController;
    public RuntimeAnimatorController secondAnimatorController;
   

    private Animator originalAnimator;

    public void TriggerEvent()
    {
        // Stocker l'Animator d'origine
        originalAnimator = Binah.GetComponent<Animator>();

        StartCoroutine(MoveObjectCoroutine());
    }

    IEnumerator MoveObjectCoroutine()
    {
        yield return new WaitForSeconds(delayTime);
        // Remplacer l'Animator d'origine par le deuxième Animator

        originalAnimator.runtimeAnimatorController = secondAnimatorController;
        // Tourner l'objet vers la destination intermédiaire
        Vector3 targetDirection = (intermediateDestination - Binah.transform.position).normalized;
        targetDirection.y = 0f; // Ignorer l'axe z pour le calcul de la rotation
        float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        Binah.transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.up);

        // Déplacement vers la destination intermédiaire
        float startTime1 = Time.time;
        Vector3 startPosition1 = Binah.transform.position;

        while (Time.time < startTime1 + moveDuration1)
        {
            float t = (Time.time - startTime1) / moveDuration1;
            Vector3 newPosition = Vector3.Lerp(startPosition1, intermediateDestination, t);
            newPosition.y = Binah.transform.position.y; // Conserver la position Y actuelle
            Binah.transform.position = newPosition;
            yield return null;
        }

        Binah.transform.position = new Vector3(intermediateDestination.x, Binah.transform.position.y, intermediateDestination.z);

        // Définir SecondAnimation à true
        originalAnimator.SetBool("Transition", true);
        yield return new WaitForSeconds(1.5f); ;

     

        // Tourner l'objet vers la position cible
        targetDirection = (targetPosition - Binah.transform.position).normalized;
        targetDirection.y = 0f; // Ignorer l'axe z pour le calcul de la rotation
        targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        Binah.transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.up);

        // Déplacement vers la position cible
        float startTime2 = Time.time;
        Vector3 startPosition2 = Binah.transform.position;

        while (Time.time < startTime2 + moveDuration2)
        {
            float t = (Time.time - startTime2) / moveDuration2;
            Vector3 newPosition = Vector3.Lerp(startPosition2, targetPosition, t);
            newPosition.y = Binah.transform.position.y; // Conserver la position Y actuelle
            Binah.transform.position = newPosition;
            yield return null;
        }

        Binah.transform.position = new Vector3(targetPosition.x, Binah.transform.position.y, targetPosition.z);

       


        // Attendre la fin de la deuxième animation
        yield return new WaitForSeconds(3f);

        // Restaurer l'Animator d'origine
        originalAnimator.runtimeAnimatorController = firstAnimatorController;
    }
}