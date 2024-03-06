using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/DialogueData", order = 1)]
public class DialogueData : ScriptableObject
{

    public AudioClip[] audioLine;
    public SerializedDictionary<string[], float[]> subtitles;       // The Lines of Sub to play with lines
}
