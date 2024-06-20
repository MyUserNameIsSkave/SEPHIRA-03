using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DroneMovementTutorialManager : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    private Material uiZ, uiQ, uiS, uiD;

    


    [SerializeField]
    private Image uiZ_image_component, uiQ_image_component, uiS_image_component, uiD_image_component;

    [SerializeField]
    private Texture2D[] uiZ_sprites, uiQ_sprites, uiS_sprites, uiD_sprites;

    [SerializeField]
    private Texture2D uiZ_pressed_sprites, uiQ_pressed_sprites, uiS_pressed_sprites, uiD_pressed_sprites;

    private bool uiZ_pressed, uiQ_pressed, uiS_pressed, uiD_pressed;



    [SerializeField]
    private float frame_duration;

    int i = 0;

    private void Awake()
    {
        uiZ.SetTexture("_base_texture", uiZ_pressed_sprites);
        uiQ.SetTexture("_base_texture", uiQ_pressed_sprites);
        uiS.SetTexture("_base_texture", uiS_pressed_sprites);
        uiD.SetTexture("_base_texture", uiD_pressed_sprites);

        StartCoroutine(Animation());
    }



    private void Update()
    {
        if (!GetComponent<Canvas>().isActiveAndEnabled)
        {
            return;
        }



        if (Input.GetKeyDown(KeyCode.W))        //Z
        {
            uiZ.SetTexture("_base_texture", uiZ_pressed_sprites);
            uiZ_pressed = true;
            //i = 0;
        }
        if (Input.GetKeyUp(KeyCode.W))          //Z
        {
            uiZ_image_component.color = Color.clear;
        }



        if (Input.GetKeyDown(KeyCode.A))        //Q
        {
            uiQ.SetTexture("_base_texture", uiQ_pressed_sprites);
            uiQ_pressed = true;
            //i = 0;
        }
        if (Input.GetKeyUp(KeyCode.A))          //Q
        {
            uiQ_image_component.color = Color.clear;
        }



        if (Input.GetKeyDown(KeyCode.S))        //S
        {
            uiS.SetTexture("_base_texture", uiS_pressed_sprites);
            uiS_pressed = true;
            //i = 0;
        }
        if (Input.GetKeyUp(KeyCode.S))    //S
        {
            uiS_image_component.color = Color.clear;
        }



        if (Input.GetKeyDown(KeyCode.D))        //D
        {
            uiD.SetTexture("_base_texture", uiD_pressed_sprites);
            uiD_pressed = true;
            //i = 0;
        }
        if (Input.GetKeyUp(KeyCode.D))   //D
        {
            uiD_image_component.color = Color.clear;
        }



        if (uiZ_pressed && uiQ_pressed && uiS_pressed && uiD_pressed)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

    }








    IEnumerator Animation()
    {


        while (true)
        {
            if (i == 2)
            {
                i = 0;
            }


            if (uiZ_pressed)
            {
                uiZ.SetTexture("_base_texture", uiZ_pressed_sprites);
            }
            else
            {
                uiZ.SetTexture("_base_texture", uiZ_sprites[i]);
            }






            if (uiQ_pressed)
            {
                uiQ.SetTexture("_base_texture", uiQ_pressed_sprites);

            }
            else
            {
                uiQ.SetTexture("_base_texture", uiQ_sprites[i]);
            }




            if (uiS_pressed)
            {
                uiS.SetTexture("_base_texture", uiS_pressed_sprites);
            }
            else
            {
                uiS.SetTexture("_base_texture", uiS_sprites[i]);
            }





            if (uiD_pressed)
            {
                uiD.SetTexture("_base_texture", uiD_pressed_sprites);
            }
            else
            {
                uiD.SetTexture("_base_texture", uiD_sprites[i]);
            }


            i++;
            yield return new WaitForSeconds(frame_duration);
        }
    }








    public void TriggerEvent()
    {
        print("PUTE");
        Destroy(gameObject);
    }
}
