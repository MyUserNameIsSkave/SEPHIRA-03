using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class DialogueManager : MonoBehaviour, IEventTriggerable
{
    //VARIABLES

    [Header ("  DEBUG")]
    [SerializeField]
    private int audioIndex = 0;

    [SerializeField]
    private int subtitbleIndex = 0;

    [SerializeField]
    private AudioSource audioSource;



    [Space (40)]



    [SerializeField]
    private DialogueData dialogueData = null;


    [Space (20)]
    [Header ("  DONT CHANGE LENGHT")]
    public DialogueManager[] nextInterlocutor;                      // Must use IEventTriggerable - Either Next Dialogue or External Event
    public MonoBehaviour[] eventToTrigger;                          // Must use IEventTriggerable - Either Next Dialogue or External Event




    private void OnValidate()
    {

        //Check if not Empty
        if (dialogueData.audioLine == null)
        {
            return;
        }




        //Arrays Creation
        if (dialogueData.audioLine.Length != nextInterlocutor.Length)
        {
            nextInterlocutor = new DialogueManager[dialogueData.audioLine.Length];
        }

        if (dialogueData.audioLine.Length != eventToTrigger.Length)
        {
            eventToTrigger = new MonoBehaviour[dialogueData.audioLine.Length];
        }


        //Check Validity of Arrays
        foreach (MonoBehaviour toTrigger in eventToTrigger)
        {
            if (toTrigger != null)
            {
                int index = 0;

                if (!(toTrigger is IEventTriggerable eventTriggableInterface))
                {
                    eventToTrigger[index] = null;
                }

                index += 1;
            }
        }
    }









    public void TriggerEvent()
    {
        PlayerNextLine();
    }


    private void PlayerNextLine()
    {
        //Play Sound
        audioSource.clip = dialogueData.audioLine[audioIndex];
    }






    IEnumerator SubtitleCoroutine()
    {
        yield return null;
    }



}
