using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementTutorialAnimationUI : MonoBehaviour, IEventTriggerable
{
    private Material ui_material;

    [SerializeField]
    private Texture2D[] unpressed_sprites;

    [SerializeField]
    private Texture2D[] pressed_sprites;

    private bool pressed = false;


    [SerializeField]
    private float frame_duration;

    int i = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            pressed = true;
            ui_material.SetTexture("_base_texture", pressed_sprites[0]);
            int i = 0;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            pressed = false;
            ui_material.SetTexture("_base_texture", unpressed_sprites[0]);
            int i = 0;
        }
    }



    private void Awake()
    {
        ui_material = GetComponent<Image>().material;
        StartCoroutine(Animation());


    }

    IEnumerator Animation()
    {


        while (true)
        {
            if (pressed)
            {
                if (i == pressed_sprites.Length)
                {
                    i = 0;
                }

                ui_material.SetTexture("_base_texture", pressed_sprites[i]);
                i++;

                yield return new WaitForSeconds(frame_duration);
            }

            if (!pressed)
            {
                if (i == unpressed_sprites.Length)
                {
                    i = 0;
                }

                ui_material.SetTexture("_base_texture", unpressed_sprites[i]);
                i++;

                yield return new WaitForSeconds(frame_duration);
            }
        }
    }


    int y = 0;

    public void TriggerEvent()
    {
        y++;



        switch (y)
        {
            case 1:
                ui_material.SetTexture("_base_texture", unpressed_sprites[0]);
                GetComponent<Image>().enabled = true;
                break;
            case 2:
                Destroy(transform.parent.gameObject);
                break;
        }
    }
}
