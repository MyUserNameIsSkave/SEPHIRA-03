using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAnimation : MonoBehaviour, IEventTriggerable
{

    public AudioSource audioSource;

    [SerializeField]
    private float trainSpeed;

    [SerializeField]
    private float timeBeforeDestruction;


    private bool canMove = false;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    
    }
    public void TriggerEvent()
    {
        canMove = true;
        audioSource.Play();
        Invoke("DestroyTrain", timeBeforeDestruction);
    }


    void Update()
    {
        if (!canMove)
        {
            return;
        }

        transform.Translate(-Vector3.forward * trainSpeed * Time.deltaTime);
    }


    private void DestroyTrain()
    {
        Destroy(gameObject);
    }

}
