using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BinahOutlineVisualChange : MonoBehaviour
{
    public enum OutlineSprite
    {
        OnScreen,
        InMargin
    }


    [Space(10)]


    [SerializeField]
    private Sprite onScreen;

    [SerializeField]
    private Sprite inMargin;



    private Image image;

    private OutlineSprite currentOutlineType;


    private void Awake()
    {
        image = GetComponent<Image>();
    }




    public void ChangeOutlineSprite(OutlineSprite currentState)
    {

        currentOutlineType = currentState;

        switch (currentState)
        {
            case OutlineSprite.OnScreen:
                image.sprite = onScreen;
                //image.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
                break;

            case OutlineSprite.InMargin:
                image.sprite = inMargin;
                //image.GetComponent<RectTransform>().sizeDelta = new Vector2(66, 66);
                break;
        }
    }



    public void ChangeOutlineSize(Vector2 visibleSize, Vector2 marginSize)
    {
        switch (currentOutlineType)
        {
            case OutlineSprite.OnScreen:

                image.GetComponent<RectTransform>().sizeDelta = visibleSize;
                break;

            case OutlineSprite.InMargin:

                image.GetComponent<RectTransform>().sizeDelta = marginSize;
                break;
        }
    }

}
