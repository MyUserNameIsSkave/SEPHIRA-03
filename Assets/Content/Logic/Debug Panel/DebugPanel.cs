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





    public AudioSource audioSource;
    private Canvas canvas;
    private Canvas GameOverCanvas;



    private void Start()
    {
        canvas = GetComponent<Canvas>();
        GameOverCanvas = Transform.FindObjectOfType<GameOverMenuManager>().gameObject.GetComponent<Canvas>();
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Transform.FindObjectOfType<GameOverMenuManager>().gameObject.GetComponent<Canvas>().enabled)
            {
                canvas.enabled = !canvas.isActiveAndEnabled;
                gammaSlider.value = gammaSettings.gammaValue;
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
              
            }
        }

        if (GameOverCanvas.enabled && canvas.enabled)
        {
            canvas.enabled = false;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }



    public void UpdateLuminosity()
    {
        gammaSettings.gammaValue = gammaSlider.value;
        gammaSettings.UpdateVolume();
    }



}
