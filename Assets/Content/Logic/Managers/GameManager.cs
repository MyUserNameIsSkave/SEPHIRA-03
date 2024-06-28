using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public GameObject StrugglingWith;

    public GameObject Binah;
    public UtilityAI_Manager BinahManager;

    public GameObject Player;
    public CameraController CameraController;
    //each components

    public Camera mainCamera;

    public PlayerStamina PlayerStam;


    //Checkpoints
    private CheckpointCollision[] checkpoints = null;

    public static List<int> ValidatedCheckpoints = new List<int>();
    public static int CurrentIndex = -1;

    public bool JoinedFirstCamera = false;



    public bool playerInputLocked
    {
        get
        {
            return _playerInputLocked;
        }
        set
        {
            _playerInputLocked = value;

            if (value)
            {
                GameObject.FindWithTag("CameraOutlineParent").GetComponent<RectTransform>().localPosition = Vector3.one * 10000;

            }
            else
            {
                GameObject.FindWithTag("CameraOutlineParent").GetComponent<RectTransform>().localPosition = Vector3.zero;

            }
        }
    }

    private bool _playerInputLocked;


    private void Awake()
    {
        Screen.SetResolution(1440, 1080, true);




        DontDestroyOnLoad(gameObject);   //NE DOIS PAS ETRE L'ENFANT D'UN AUTRE OBJET

        GameManager[] managers = FindObjectsOfType<GameManager>();
        foreach (GameManager manager in managers)
        {
            if (manager != this)
            {
                Destroy(manager.gameObject);
            }
        }



        Instance = this;
        Player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
        PlayerStam = Player.GetComponent<PlayerStamina>();


        CameraController = Player.GetComponent<CameraController>();

        Binah = GameObject.FindGameObjectWithTag("Binah");
        BinahManager = Binah.GetComponent<UtilityAI_Manager>();



        checkpoints = FindObjectsOfType<CheckpointCollision>();
        SpawnToCheckpoint();
    }

    //DEBUG
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }







    /// <summary>
    /// Used only to Respawn to the Last Checkpoint.
    /// </summary>
    private void SpawnToCheckpoint()
    {
        if (CurrentIndex < 0 || CurrentIndex >= checkpoints.Length)
        {
            return;
        }
        
        CheckpointCollision currentCheckpoint = checkpoints[CurrentIndex];

        CameraController.CurrentCamera = currentCheckpoint.checkpointCamera;
        Binah.transform.position = currentCheckpoint.transform.position;
    }

    /// <summary>
    /// Used only to Change Level.
    /// </summary>
    public void ChangeScene(string sceneName)
    {
        ValidatedCheckpoints = new List<int>();
        CurrentIndex = -1;
        SceneManager.LoadSceneAsync(sceneName);
    }



    /// <summary>
    /// First spawn the Game Over UI
    /// </summary>
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
