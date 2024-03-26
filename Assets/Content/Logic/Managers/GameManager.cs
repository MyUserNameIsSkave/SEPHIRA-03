using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;




    public GameObject Binah;
    public UtilityAI_Manager BinahManager;

    public GameObject Player;
    public CameraController CameraController;
    //each components

    public Camera mainCamera;

    public PlayerStamina PlayerStam;


    private void Awake()
    {
        Instance = this;
        Player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
        PlayerStam = Player.GetComponent<PlayerStamina>();


        CameraController = Player.GetComponent<CameraController>();

        Binah = GameObject.FindGameObjectWithTag("Binah");
        BinahManager = Binah.GetComponent<UtilityAI_Manager>();


    }




}
