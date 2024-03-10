using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RencontreBinah_Cinematic : MonoBehaviour
{
    public Animator Animator;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartCinematic",5f);
    }

    // Update is called once per frame
    void Update()
    {

        

    }
    void StartCinematic()
    {
        Animator.SetBool("StartCinematic", true);
        delay = 7f;
        Invoke("NextAnim1", delay);
    }
    void NextAnim1()
    {
        Animator.SetBool("NextAnim1", true);
        delay = 5f;
        Invoke("NextAnim2", delay);
    }

    void NextAnim2()
    {
        Animator.SetBool("NextAnim2", true);
        delay = 10f;
        Invoke("NextAnim3", delay);

    }
    void NextAnim3()
    {
        Animator.SetBool("NextAnim3", true);
        delay = 1f;
        Invoke("Idle", delay);
    }
    void Idle()
    {
        Animator.SetBool("Idle", true);

    }
}
