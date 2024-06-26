using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL1_Scene1_YMarchand : MonoBehaviour, IEventTriggerable
{
    public GameObject Yakuza; // L'objet cible contenant l'Animator
    public string booleanStart; // Le nom de la bool�enne � activer
    private float delay = 8.8f; // Le temps � attendre avant de lancer le son (en secondes)
    private float delay2 = 0.6f; // Le temps � attendre avant de lancer le deuxi�me son (en secondes)

    private Animator animatorY;
    public AudioSource audioSourcePunch;
    public AudioSource audioSourceFall;

    void Start()
    {
        // R�cup�re l'Animator du GameObject cible
        animatorY = Yakuza.GetComponent<Animator>();

    }

    public void TriggerEvent()
    {
        // Active la bool�enne dans l'Animator
        Debug.Log("La bool�enne " + booleanStart + " a �t� activ�e.");
        animatorY.SetBool(booleanStart, true);

        // V�rifie si la bool�enne est activ�e et si un clip audio est assign�
        if (animatorY.GetBool(booleanStart))
        {
            // Lance la coroutine pour attendre avant de lire le son
            StartCoroutine(PlaySoundWithDelay(delay, delay2));
        }
    }

    IEnumerator PlaySoundWithDelay(float waitTime1, float waitTime2)
    {
        // Attend pendant le temps sp�cifi�
     
        yield return new WaitForSeconds(waitTime1);
        audioSourcePunch.Play();

        yield return new WaitForSeconds(waitTime2);
        audioSourceFall.Play();
    }
}
