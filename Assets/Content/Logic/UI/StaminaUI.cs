using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.PlayerLoop.PreUpdate;


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
    private bool useCostPreview;

    [SerializeField]
    private Color previewColor;

    [SerializeField]
    private Color lowOpacityPreviewColor;


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



    [Range(0, 9)]
    public int currentCost;







    #region Debugging
    [Header ("  DEBUG")]



    [SerializeField, Range(0, 9)]
    private int testValue;

    private void OnValidate()
    {
        UpdateVisuals(testValue, true);

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

        StartCoroutine(UpdateVisuals(GameManager.Instance.PlayerStam.CurrentStam, true));

    }



    public void ValueChanged(bool gainUpdate)
    {
        StopAllCoroutines();
        StartCoroutine(UpdateVisuals(GameManager.Instance.PlayerStam.CurrentStam, gainUpdate));
    }









    IEnumerator UpdateVisuals(int currentPoints, bool gainUpdate)
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
                if (gainUpdate)
                {
                    if (i == currentPoints - 1 && currentPoints > previousPoints)
                    {
                        if (previousGainAnimated != null)
                        {
                            previousGainAnimated.rectTransform.sizeDelta = Vector2.one * 22;
                        }

                        StartCoroutine(GainAnimation(points[i]));
                    }
                }
            }


            //Preview Cost - Have the Points
            if (useCostPreview)
            {
                if (i >= currentPoints - currentCost)
                {
                    points[i].color = previewColor;
                    points[i].transform.GetChild(0).GetComponent<Image>().color = previewColor;

                }
            }
        }


        //Preview Cost - Missing Points

        if (useCostPreview)
        {
            int i = 0;
            foreach (Image image in points)
            {
                if (currentPoints - currentCost < 0 && i < currentCost)
                {
                    if (i! < currentPoints)
                    {
                        image.color = previewColor;
                        image.transform.GetChild(0).GetComponent<Image>().color = previewColor;
                    }
                    else
                    {
                        image.color = lowOpacityPreviewColor;
                        image.transform.GetChild(0).GetComponent<Image>().color = lowOpacityPreviewColor;
                    }
                }

                i += 1;
            }
        }

        



        yield return new WaitForSeconds(timeBeforeFading);
        FadeOut();
    }




    private Image previousGainAnimated;


    //Voir pour ajouter un buffer du dernier Gained Point et le forcement le reset a l'activation de la Coroutine (Pour régler un bug ou un point vide peut rester grand)
    IEnumerator GainAnimation(Image gainedPoint)
    {
        previousGainAnimated = gainedPoint;
        gainedPoint.rectTransform.sizeDelta = Vector2.one * 22 * gainAnimationSizeMultiplier;

        yield return new WaitForSeconds(gainAnimationDuration);

        gainedPoint.rectTransform.sizeDelta = Vector2.one * 22;
    }









    public void FadeIn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }


    public void FadeOut()
    {
        if (currentCost != 0)
        {
            return;
        }

        transform.GetChild(0).gameObject.SetActive(false);
    }

}
