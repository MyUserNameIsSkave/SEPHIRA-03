using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour, IEventTriggerable
{
    //VARIABLES

    [SerializeField]
    private int audioIndex = 0;

    [SerializeField]
    private int subtitbleIndex = 0;





    [SerializeField]
    private DialogueData dialogueData = null;



    [SerializeField]
    private AudioSource audioSource;





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
