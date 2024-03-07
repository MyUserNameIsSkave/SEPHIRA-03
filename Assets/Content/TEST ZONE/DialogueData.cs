using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/DialogueData", order = 1)]
public class DialogueData : ScriptableObject
{

    [Tooltip("If True the Subtitles will be displaying at the bottom of the scree")]
    public bool isMainDialogue = true;
    public AudioClip[] audioLine;
    public SerializedDictionary<string[], float[]> subtitles;       // The Lines of Sub to play with lines
}
