using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaUI : MonoBehaviour
{
    [SerializeField]
    private Sprite empty; 
    
    [SerializeField]
    private Sprite full;


    [Space(20)]







    [SerializeField]
    private float timeBeforeFading;


    [Space(20)]


    [SerializeField, Tooltip ("Must be in order 1 to 9")]
    private Image[] points;







    [Space(20)]
    [Header("  EXPERIMENTATION")]

    [SerializeField]
    private bool useVariableOpacity;

    [SerializeField]
    private Color lowOpacityColor;

    [Space(10)]

    [SerializeField]
    private bool useGainAnimation;

    [SerializeField]
    private float gainAnimationSizeMultiplier;

    [SerializeField]
    private float gainAnimationDuration;

    [HideInInspector]
    public int previousPoints;



    [Space(20)]



    #region Debugging
    [Header ("  DEBUG")]



    [SerializeField, Range(0, 9)]
    private int testValue;

    private void OnValidate()
    {
        UpdateVisuals(testValue);

        foreach (var image in points)
        {
            image.sprite = empty;
        }

        //Set All Full Points
        for (int i = 0; i < testValue; i++)
        {
            points[i].sprite = full;
        }
    }

    #endregion






    private void Start()
    {
        if (GameManager.Instance.PlayerStam.CurrentStam == GameManager.Instance.PlayerStam.maxStam)
        {
            FadeOut();
            return;
        }


        StartCoroutine(UpdateVisuals(GameManager.Instance.PlayerStam.CurrentStam));
    }



    public void ValueChanged()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateVisuals(GameManager.Instance.PlayerStam.CurrentStam));
    }









    IEnumerator UpdateVisuals(int currentPoints)
    {
        FadeIn();





        //Reset All Points
        foreach (Image image in points)
        {
            //Base
            image.sprite = empty;

            // ---------------------- Tests ----------------------

            //Variable Opacity
            if (useVariableOpacity)
            {
                image.color = lowOpacityColor;
                image.transform.GetChild(0).GetComponent<Image>().color = lowOpacityColor;
            }
        }

        //Set All Full Points
        for (int i = 0; i < currentPoints; i++)
        {
            //Base
            points[i].sprite = full;

            // ---------------------- Tests ----------------------

            //Variable Opacity
            if (useVariableOpacity)
            {
                points[i].color = Color.white;
                points[i].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }

            //Gain Animation
            if (useGainAnimation)
            {
                if (i == currentPoints - 1 && currentPoints > previousPoints)
                {
                    StartCoroutine(GainAnimation(points[i]));
                }
            }
        }

        yield return new WaitForSeconds(timeBeforeFading);
        FadeOut();
    }




    IEnumerator GainAnimation(Image gainedPoint)
    {
        gainedPoint.rectTransform.sizeDelta = Vector2.one * 22 * gainAnimationSizeMultiplier;

        yield return new WaitForSeconds(gainAnimationDuration);

        gainedPoint.rectTransform.sizeDelta = Vector2.one * 22;




    }









    private void FadeIn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }


    private void FadeOut()
    {
        transform.GetChild(0).gameObject.SetActive(false);

    }

}
