using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL1_Scene1_YMarchand : MonoBehaviour, IEventTriggerable
{
    public GameObject Yakuza; // L'objet cible contenant l'Animator
    public string booleanStart; // Le nom de la booléenne à activer
    private float delay = 8.8f; // Le temps à attendre avant de lancer le son (en secondes)
    private float delay2 = 0.6f; // Le temps à attendre avant de lancer le deuxième son (en secondes)

    private Animator animatorY;
    public AudioSource audioSourcePunch;
    public AudioSource audioSourceFall;

    void Start()
    {
        // Récupère l'Animator du GameObject cible
        animatorY = Yakuza.GetComponent<Animator>();

    }

    public void TriggerEvent()
    {
        // Active la booléenne dans l'Animator
        Debug.Log("La booléenne " + booleanStart + " a été activée.");
        animatorY.SetBool(booleanStart, true);

        // Vérifie si la booléenne est activée et si un clip audio est assigné
        if (animatorY.GetBool(booleanStart))
        {
            // Lance la coroutine pour attendre avant de lire le son
            StartCoroutine(PlaySoundWithDelay(delay, delay2));
        }
    }

    IEnumerator PlaySoundWithDelay(float waitTime1, float waitTime2)
    {
        // Attend pendant le temps spécifié
     
        yield return new WaitForSeconds(waitTime1);
        audioSourcePunch.Play();

        yield return new WaitForSeconds(waitTime2);
        audioSourceFall.Play();
    }
}
