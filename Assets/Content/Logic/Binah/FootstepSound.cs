using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip footstepClip1; // premier clip audio � jouer
    public AudioClip footstepClip2; // deuxi�me clip audio � jouer
    public float stepInterval = 0.5f; // l'intervalle entre chaque pas en secondes
    public float moveThreshold = 0.1f; // seuil de d�placement pour consid�rer que le personnage a boug�

    private AudioSource audioSource;
    private float nextStepTime;
    private Vector3 lastPosition;

    void Start()
    {
        // le script � besoin d'une AudioSource, � mettre sur le personnage concern�
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    // si l'on veut faire en sorte que le son se fasse � une intervalle r�guli�re, utiliser le voide Update()
   // void Update()
  //  {
        // v�rifier si le personnage est au sol
      //  if (IsGrounded())
      //  {
            // v�rifier si le personnage s'est d�plac� suffisamment loin depuis la derni�re fois
         //   if (Vector3.Distance(transform.position, lastPosition) > moveThreshold)
          //  {
                // mettre � jour la derni�re position du personnage
             //   lastPosition = transform.position;

                // v�rifier si suffisamment de temps s'est �coul� depuis le dernier pas
             //   if (Time.time > nextStepTime)
               // {
                    // s�lectionner al�atoirement un clip audio � jouer
                 //   AudioClip clipToPlay = Random.Range(0, 2) == 0 ? footstepClip1 : footstepClip2;

                    // jouer le son de pas
                  //  audioSource.PlayOneShot(clipToPlay);

                    // mettre � jour le temps du prochain pas
                //    nextStepTime = Time.time + stepInterval;
              //  }
          //  }
      //  }
    //}

    bool IsGrounded()
    {
        // v�rifier si le personnage est au sol
        // � modifier en fonction de votre propre impl�mentation de d�tection au sol
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }
    public void PlayFootstepSound()
    {
        // v�rifier si le personnage est au sol
        if (IsGrounded())
        {
            // v�rifier si le personnage s'est d�plac� suffisamment loin depuis la derni�re fois
            if (Vector3.Distance(transform.position, lastPosition) > moveThreshold)
            {// s�lectionner al�atoirement un clip audio � jouer
                AudioClip clipToPlay = Random.Range(0, 2) == 0 ? footstepClip1 : footstepClip2;

                // jouer le son de pas
                audioSource.PlayOneShot(clipToPlay);
            }
        }
    }
}
