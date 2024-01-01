using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AYellowpaper.SerializedCollections.SerializedDictionarySample;

public class InfiltrationManager : MonoBehaviour
{
    [SerializedDictionary("Target Object", "Target Point")]
    public SerializedDictionary<GameObject, float> Targets;
}
