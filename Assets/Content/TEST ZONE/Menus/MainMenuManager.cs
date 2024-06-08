using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{


    [Space(20)]

    [SerializeField]
    private TMP_Dropdown dropdown;





    public void Play()
    {
        SceneManager.LoadSceneAsync(0);

    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(dropdown.value + 2);
    }

    public void Credits()
    {
        Debug.Log("WHAT TO DO WITH THE CREDIT ?!");
    }


    public void Quit()
    {
        Application.Quit();
    }
}
