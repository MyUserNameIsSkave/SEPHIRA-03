using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraOutlineVisualChange : MonoBehaviour
{
    public enum OutlineSprite
    {
        OnScreen,
        InMargin
    }

    [SerializeField]
    private Color alreadyUsedColor;


    [Space(10)]


    [SerializeField]
    private Sprite onScreenUsed;

    [SerializeField]
    private Sprite onScreenNotUsed;

    [SerializeField]
    private Sprite inMargin;



    private OutlineSprite currentOutlineType;

    private Image image;




    private void Awake()
    {
        image = GetComponent<Image>();
    }




    public void ChangeOutlineSprite(OutlineSprite currentState, bool alreadyUsed)
    {
        currentOutlineType = currentState;

        switch (currentOutlineType)
        {
            case OutlineSprite.OnScreen:

                if (alreadyUsed)
                {
                    if (image == null)
                    {
                        return;
                    }

                    image.sprite = onScreenUsed;
                    image.color = alreadyUsedColor;
                }
                else
                {
                    if (image == null)
                    {
                        return;
                    }

                    image.sprite = onScreenNotUsed;
                    image.color = Color.white;
                }
                break;

            case OutlineSprite.InMargin:

                if (alreadyUsed)
                {
                    if (image == null)
                    {
                        return;
                    }

                    image.sprite = inMargin;
                    image.color = alreadyUsedColor;

                }
                else
                {
                    if (image == null)
                    {
                        return;
                    }

                    image.sprite = inMargin;
                    image.color = Color.white;
                }
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
