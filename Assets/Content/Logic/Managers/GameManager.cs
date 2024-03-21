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



    private void Awake()
    {
        Instance = this;

        Binah = GameObject.FindGameObjectWithTag("Binah");
        BinahManager = Binah.GetComponent<UtilityAI_Manager>();

        Player = GameObject.FindGameObjectWithTag("Player");
        CameraController = Player.GetComponent<CameraController>();

        mainCamera = Camera.main;
    }




}
