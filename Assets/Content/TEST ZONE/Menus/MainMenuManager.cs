using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioSource clickSource;
    public AudioSource quitSource;

    [Space(20)]

    [SerializeField]
    private TMP_Dropdown dropdown;





    public void Play()
    {
        if (!clickSource.isPlaying)
        {
            clickSource.Play();
        }
        SceneManager.LoadSceneAsync(1);

    }

    public void LoadLevel()
    {
        if (!clickSource.isPlaying)
        {
            clickSource.Play();
        }
        SceneManager.LoadSceneAsync(dropdown.value + 2);
    }

    public void Credits()
    {
        if (!clickSource.isPlaying)
        {
            clickSource.Play();
        }
        Debug.Log("WHAT TO DO WITH THE CREDIT ?!");
    }


    public void Quit()
    {
        if (!quitSource.isPlaying)
        {
            quitSource.Play();
        }
        Application.Quit(1);
    }
}
