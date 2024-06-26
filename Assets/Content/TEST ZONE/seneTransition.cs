using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class seneTransition : MonoBehaviour, IEventTriggerable
{
    public int NextScene;

    public void TriggerEvent()
    {
        SceneManager.LoadSceneAsync(NextScene);
    }
}
