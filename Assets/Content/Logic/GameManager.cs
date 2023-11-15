using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;




    public GameObject Binah;
    //each components

    public GameObject Player;
    public CameraController CameraController;
    //each components


    private void Awake()
    {
        Instance = this;

        Binah = GameObject.FindGameObjectWithTag("Binah");

        Player = GameObject.FindGameObjectWithTag("Player");
        CameraController = Player.GetComponent<CameraController>();
    }
}
