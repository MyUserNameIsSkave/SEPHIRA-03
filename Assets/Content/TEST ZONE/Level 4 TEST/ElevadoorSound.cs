using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevadoorSound : MonoBehaviour
{
   
    public float delay = 3f; // Nombre de secondes avant de lancer le son

    public AudioSource audioSource;

    public void Start()
    {
       
        if (audioSource == null)
        {
            Debug.LogError("Aucun AudioSource trouvé sur cet objet.");
        }
        else
        {
            StartCoroutine(LancerSon());
        }
    }

    IEnumerator LancerSon()
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}
