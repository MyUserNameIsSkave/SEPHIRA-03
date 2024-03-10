using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour, IEventTriggerable
{
    //VARIABLES

    [Header("  DEBUG")]
    [SerializeField]
    private int audioIndex = -1;


    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private bool manualyActivated = false;


    [Space(40)]



    [SerializeField]
    private DialogueData dialogueData = null;


    [Space(20)]
    [Header("  DONT CHANGE LENGTH")]
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

                //Erase non valid reference
                if (!(toTrigger is IEventTriggerable eventTriggableInterface))
                {
                    eventToTrigger[index] = null;
                }
            }
        }
    }





    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && manualyActivated)
        {
            PlayerNextLine();
        }
    }




    public void TriggerEvent()
    {
        PlayerNextLine();
    }


    private void PlayerNextLine()
    {
        print("PUTE");






        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            StopAllCoroutines();
        }
        




        if (dialogueData.audioLine[audioIndex] != null)
        {
            audioSource.clip = dialogueData.audioLine[audioIndex];
            audioSource.Play();
        }

        StartCoroutine(SubtitleCoroutine());
    }






    IEnumerator SubtitleCoroutine()
    {

        int dictionnaryIndex = 0;

        foreach (KeyValuePair<string[], float[]> subtitles in dialogueData.subtitles)
        {
            if (dictionnaryIndex == audioIndex)
            {
                int subIndex = 0;
                foreach (string sub in subtitles.Key)
                {
                    //Display Subtitle
                    DisplaySubtitles(sub);

                    print(subtitles.Value[subIndex]);
                    yield return new WaitForSeconds(subtitles.Value[subIndex]);

                    subIndex += 1;
                }
            }


            
            dictionnaryIndex += 1;
        }

        audioIndex += 1;

        StartEvents();

        yield return null;
    }





    private void DisplaySubtitles(string sub)
    {
        print(sub);
    }





    private void StartEvents()
    {
        //Next Dialogue
        if (nextInterlocutor.Length <= audioIndex)
        {
            return;
        }

        if (nextInterlocutor[audioIndex] != null)
        {
            IEventTriggerable eventInterface = nextInterlocutor[audioIndex] as IEventTriggerable;
            eventInterface.TriggerEvent();
        }


        //Trigger Event
        if (eventToTrigger.Length <= audioIndex)
        {
            return;
        }

        if (eventToTrigger[audioIndex] != null)
        {
            IEventTriggerable eventInterface = eventToTrigger[audioIndex] as IEventTriggerable;
            eventInterface.TriggerEvent();
        }
    }
}
