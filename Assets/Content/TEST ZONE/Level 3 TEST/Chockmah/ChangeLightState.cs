using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeLightState : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    private Material material;

    [SerializeField]
    private GameObject[] lights;

    private bool isLightsOn = true;


    public void TriggerEvent()
    {
        StartCoroutine(LightStateChange());

        material.color = Color.red;
    }


    private void OnApplicationQuit()
    {
        material.color = Color.green;
    }


    IEnumerator LightStateChange()
    {
        foreach (var light in lights)
        {
            light.SetActive(!isLightsOn);
            yield return new WaitForSeconds(Random.Range(0f, 0.3f));
        }
        yield return 0;
    }

}
