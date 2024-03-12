using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip footstepClip1; // premier clip audio à jouer
    public AudioClip footstepClip2; // deuxième clip audio à jouer
    public float stepInterval = 0.5f; // l'intervalle entre chaque pas en secondes
    public float moveThreshold = 0.1f; // seuil de déplacement pour considérer que le personnage a bougé

    private AudioSource audioSource;
    private float nextStepTime;
    private Vector3 lastPosition;

    void Start()
    {
        // le script à besoin d'une AudioSource, à mettre sur le personnage concerné
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    // si l'on veut faire en sorte que le son se fasse à une intervalle régulière, utiliser le voide Update()
   // void Update()
  //  {
        // vérifier si le personnage est au sol
      //  if (IsGrounded())
      //  {
            // vérifier si le personnage s'est déplacé suffisamment loin depuis la dernière fois
         //   if (Vector3.Distance(transform.position, lastPosition) > moveThreshold)
          //  {
                // mettre à jour la dernière position du personnage
             //   lastPosition = transform.position;

                // vérifier si suffisamment de temps s'est écoulé depuis le dernier pas
             //   if (Time.time > nextStepTime)
               // {
                    // sélectionner aléatoirement un clip audio à jouer
                 //   AudioClip clipToPlay = Random.Range(0, 2) == 0 ? footstepClip1 : footstepClip2;

                    // jouer le son de pas
                  //  audioSource.PlayOneShot(clipToPlay);

                    // mettre à jour le temps du prochain pas
                //    nextStepTime = Time.time + stepInterval;
              //  }
          //  }
      //  }
    //}

    bool IsGrounded()
    {
        // vérifier si le personnage est au sol
        // à modifier en fonction de votre propre implémentation de détection au sol
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }
    public void PlayFootstepSound()
    {
        // vérifier si le personnage est au sol
        if (IsGrounded())
        {
            // vérifier si le personnage s'est déplacé suffisamment loin depuis la dernière fois
            if (Vector3.Distance(transform.position, lastPosition) > moveThreshold)
            {// sélectionner aléatoirement un clip audio à jouer
                AudioClip clipToPlay = Random.Range(0, 2) == 0 ? footstepClip1 : footstepClip2;

                // jouer le son de pas
                audioSource.PlayOneShot(clipToPlay);
            }
        }
    }
}
