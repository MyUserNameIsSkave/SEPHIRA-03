using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public void Continue()
    {
        GetComponent<Canvas>().enabled = false;
    }
    public void QuitToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
