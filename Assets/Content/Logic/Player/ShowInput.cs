using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInput : MonoBehaviour
{
    [SerializeField]
    GameObject Panel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Panel.SetActive(!Panel.activeSelf);



        }

    }




}
