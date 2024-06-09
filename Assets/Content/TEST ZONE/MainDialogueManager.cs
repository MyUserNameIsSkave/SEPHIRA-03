using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.ProBuilder.MeshOperations;
using Unity.VisualScripting;

public class MainDialogueManager : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    private bool lockGameplay = false;

    [SerializeField]
    private SerializedDictionary<string, float> subtitles;

    [SerializeField]
    private MonoBehaviour[] events;




    private TextMeshProUGUI subtitleText;
    private bool skipNext;



    private void Start()
    {
        subtitleText = GameObject.FindGameObjectWithTag("General Subtitle").GetComponent<TextMeshProUGUI>();
    }





    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skipNext = true;
        }

    }


    public void TriggerEvent()
    {
        if (lockGameplay)
        {
            GameManager.Instance.playerInputLocked = true;
        }

        StartCoroutine(DialogueLoop());
    }


    IEnumerator DialogueLoop()
    {
        
        int subIndex = 0;
        foreach (KeyValuePair<string, float> subtitle in subtitles)
        {
            float time = 0;

            StartCoroutine(DisplaySubtitles(subtitle.Key));

            if (subIndex < events.Length)
            {
                if (events[subIndex] != null)
                {
                    IEventTriggerable eventInterface = events[subIndex] as IEventTriggerable;
                    eventInterface.TriggerEvent();
                }
            }

            subIndex += 1;


            while (time <= subtitle.Value)
            {
                if (skipNext)
                {
                    skipNext = false;
                    time = Mathf.Infinity;
                    
                }
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
            }
            
        }
        StartCoroutine(DisplaySubtitles(""));

        if (lockGameplay)
        {
            GameManager.Instance.playerInputLocked = false;
        }
    }

    IEnumerator DisplaySubtitles(string sub)
    {
        yield return new WaitForSeconds(0.2f);
        subtitleText.text = sub;
    }

}
