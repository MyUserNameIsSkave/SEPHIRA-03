using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class SecurityCameraEventTrigger_Camera : CameraBase
{


    [SerializedDictionary("New Camera Script", "Trigger Delay")]
    public SerializedDictionary<CameraBase, float> NewCamera;

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
        cameraController.currentCamera = kvp.Key;
    }





    private void CheckForEventsToDo()
    {

    }

    IEnumerator ExecuteEvents(KeyValuePair<MonoBehaviour, float> kvp)
    {
        IEventTriggerable EvenTriggerInterface = kvp.Key.GetComponent<IEventTriggerable>();
        if (EvenTriggerInterface != null)
        {
            yield return new WaitForSeconds(kvp.Value);
            EvenTriggerInterface.TriggerEvent();
        }

        yield return null;
        
    }


}
