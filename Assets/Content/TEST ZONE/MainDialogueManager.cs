using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.ProBuilder.MeshOperations;
using Unity.VisualScripting;
using Autodesk.Fbx;

public class MainDialogueManager : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    private bool lockGameplay = false;

    [SerializeField]
    private bool selfTrigger = false;


    [SerializeField]
    private float initialDelay;

    [SerializeField]
    private SerializedDictionary<string, float> subtitles;

    [SerializeField]
    private MonoBehaviour[] events;





    private TextMeshProUGUI subtitleText;
    private bool skipNext;



    private void Start()
    {
        subtitleText = GameObject.FindGameObjectWithTag("General Subtitle").GetComponent<TextMeshProUGUI>();

        if (selfTrigger)
        {
            TriggerEvent();
        }
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

        skipNext = false;
        StartCoroutine(DialogueLoop());
    }


    IEnumerator DialogueLoop()
    {
        
        yield return new WaitForSeconds(initialDelay);


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

            time = 0;

        }
        StartCoroutine(DisplaySubtitles(""));

        if (lockGameplay)
        {
            GameManager.Instance.playerInputLocked = false;
        }

    }

    IEnumerator DisplaySubtitles(string sub)
    {
        subtitleText.text = sub;
        yield return 0;
    }

}
