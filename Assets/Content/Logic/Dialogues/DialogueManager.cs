using DigitalOpus.MB.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour, IEventTriggerable
{
    //VARIABLES



    [Header("  DEBUG")]
    [SerializeField]
    private int audioIndex = 0;


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





    [Space(40)]



    [SerializeField]
    private bool isBackgroundDialogue;

    [SerializeField]
    private GameObject backgroundDialoguePrefab;



    private Transform backgroundDialogueParent;


    private GameObject currentBackgroundDialogue;


    [SerializeField]
    private GameObject anchorPoint;







    private TextMeshProUGUI subtitleText;






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



    private void Start()
    {
        subtitleText = GameObject.FindGameObjectWithTag("General Subtitle").GetComponent<TextMeshProUGUI>();
        backgroundDialogueParent = GameObject.FindGameObjectWithTag("Background Subtitle").transform;
    }


    private void Update()
    {

    }













    public void TriggerEvent()
    {
        PlayerNextLine();
    }


    private void PlayerNextLine()
    {

        if (isBackgroundDialogue && currentBackgroundDialogue == null)
        {
            currentBackgroundDialogue = Instantiate(backgroundDialoguePrefab, backgroundDialogueParent);
            currentBackgroundDialogue.GetComponent<BackgroundSubtitle>().AnchorPoint = anchorPoint;
        }




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
                    StartCoroutine(DisplaySubtitles(sub));
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





    private void StartEvents()
    {
        int eventIndex = audioIndex - 1;


        if (audioIndex >= nextInterlocutor.Length)
        {
            if (isBackgroundDialogue)
            {
                Destroy(currentBackgroundDialogue);
                currentBackgroundDialogue = null;
            }
            else
            {
                DisplaySubtitles("");
            }
        }




        //Next Dialogue
        if (nextInterlocutor.Length == audioIndex)
        {
            if (isBackgroundDialogue)
            {
                Destroy(currentBackgroundDialogue);
                currentBackgroundDialogue = null;
            }
            else
            {
                DisplaySubtitles("");
            }
        }

        if (nextInterlocutor.Length <= eventIndex)
        {
            return;
        }

        if (nextInterlocutor[eventIndex] != null)
        {
            IEventTriggerable eventInterface = nextInterlocutor[eventIndex] as IEventTriggerable;
            eventInterface.TriggerEvent();
        }


        //Trigger Event
        if (eventToTrigger.Length <= eventIndex)
        {
            return;
        }

        if (eventToTrigger[eventIndex] != null)
        {
            IEventTriggerable eventInterface = eventToTrigger[eventIndex] as IEventTriggerable;
            eventInterface.TriggerEvent();
        }
    }








    IEnumerator DisplaySubtitles(string sub)
    {
        yield return new WaitForSeconds(0.5f);


        if (!isBackgroundDialogue)
        {
            subtitleText.text = sub;
        }
        else
        {
            currentBackgroundDialogue.GetComponent<BackgroundSubtitle>().SetSubtittleTo(sub);
        }

    }    
}
