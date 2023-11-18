using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SecurityCameraEventTrigger_Camera : CameraBase
{


    [Space(40)]
    [Header("  EVENT SETTINGS")]
    [Space(10)]


    [SerializedDictionary("New Camera Script", "Trigger Delay")]
    public SerializedDictionary<CameraBase, float> NewCamera;

    [Space(10)]

    [SerializedDictionary("Event to Trigger", "Trigger Delay")]
    public SerializedDictionary<MonoBehaviour, float> EventToTrigger;



    protected override void Transitionned()
    {
        CheckForCameraChange();
        CheckForEventsToDo();
    }





    private void CheckForCameraChange()
    {
        foreach (KeyValuePair<CameraBase, float> kvp in NewCamera)
        {
            StartCoroutine(ChangeCameraEvent(kvp));
        }
    }


    IEnumerator ChangeCameraEvent(KeyValuePair<CameraBase, float> kvp)
    {

        yield return new WaitForSeconds(kvp.Value);
        cameraController.CurrentCamera = kvp.Key;
    }





    private void CheckForEventsToDo()
    {
        print("ola");
        foreach (KeyValuePair<MonoBehaviour, float> kvp in EventToTrigger)
        {
            print("que");

            StartCoroutine(ExecuteEvents(kvp));
        }
    }


    IEnumerator ExecuteEvents(KeyValuePair<MonoBehaviour, float> kvp)
    {
        print("tal");


        IEventTriggerable EvenTriggerInterface = kvp.Key.GetComponent<IEventTriggerable>();
        if (EvenTriggerInterface != null)
        {
            print(kvp.Value);
            yield return new WaitForSeconds(kvp.Value);
            print("!");
            EvenTriggerInterface.TriggerEvent();
        }

        yield return null;

    }

    
}
