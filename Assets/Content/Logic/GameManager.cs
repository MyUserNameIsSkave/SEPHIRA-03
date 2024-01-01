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


    private void Awake()
    {
        Instance = this;

        Binah = GameObject.FindGameObjectWithTag("Binah");
        BinahManager = GameManager.Instance.BinahManager;

        Player = GameObject.FindGameObjectWithTag("Player");
        CameraController = Player.GetComponent<CameraController>();
    }




}
