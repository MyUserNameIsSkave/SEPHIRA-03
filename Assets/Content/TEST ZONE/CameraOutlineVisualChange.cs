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


    [Space (10)]


    [SerializeField]
    private Sprite onScreen;

    [SerializeField]
    private Sprite inMargin;





    private Image image;




    private void Awake()
    {
        image = GetComponent<Image>();
    }




    public void ChangeOutlineSprite(OutlineSprite currentState, bool alreadyUsed)
    {
        switch (currentState)
        {
            case OutlineSprite.OnScreen:

                if (alreadyUsed)
                {
                    if (image == null)
                    {
                        return;
                    }

                    image.sprite = onScreen;
                    image.color = alreadyUsedColor;
                    image.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                }
                else
                {
                    if (image == null)
                    {
                        return;
                    }

                    image.sprite = onScreen;
                    image.color = Color.white;
                    image.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
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
                    image.GetComponent<RectTransform>().sizeDelta = new Vector2(44, 44);

                }
                else
                {
                    if (image == null)
                    {
                        return;
                    }

                    image.sprite = inMargin;
                    image.color = Color.white;
                    image.GetComponent<RectTransform>().sizeDelta = new Vector2(44, 44);
                }
                break;
        }
    }
}
