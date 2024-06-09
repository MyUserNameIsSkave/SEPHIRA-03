using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
    [SerializeField]
    private Slider gammaSlider;

    [SerializeField]
    private LuminositySettings gammaSettings;






    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Transform.FindObjectOfType<GameOverMenuManager>().gameObject.GetComponent<Canvas>().enabled)
            {
                canvas.enabled = !canvas.isActiveAndEnabled;
                gammaSlider.value = gammaSettings.gammaValue;
            }
        }

        if (Transform.FindObjectOfType<GameOverMenuManager>().gameObject.GetComponent<Canvas>().enabled && canvas.enabled)
        {
            canvas.enabled = false;
        }
    }



    public void UpdateLuminosity()
    {
        gammaSettings.gammaValue = gammaSlider.value;
        gammaSettings.UpdateVolume();
    }



}
