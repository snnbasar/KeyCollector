using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool PlayerAlive;

    public int totalKeys;

    public Button restartButton; //sets on inspector

    public Button nextLevelButton; //sets on inspector

    //private bool firstStart;


    private int _collectedKeys;
    public int collectedKeys
    {
        get
        {
            return _collectedKeys;
        }
        set
        {
            _collectedKeys = value;
            CheckIfGameOver();
        }
    }

    

    public event Action OnStartGame;

    public event Action OnPlayerDied;

    public event Action OnMissionCompleted;
    public bool GetPlayerAlive
    {
        get
        {
            if (PlayerAlive == true)
                return true;
            else
                return false;
        }
        set
        {
            PlayerAlive = value;
        }
    }
    private void Awake()
    {
        //Screen.SetResolution(1280, 720, Screen.fullScreen);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        PlayerAlive = true;
        /*firstStart = true;
        if (!firstStart)
        {
            StartGame();
        }*/
    }

    

    public void StartGame()
    {
        OnStartGame?.Invoke();
    }


    public void PlayerIsDead()
    {
        OnPlayerDied?.Invoke();
        restartButton.gameObject.SetActive(true);
    }


    public void KillPlayer()
    {
        GetPlayerAlive = false;
        PlayerIsDead();
    }

    private void CheckIfGameOver()
    {
        if (collectedKeys < totalKeys)
            return;
        else
            MissionCompleted();
    }

    public void MissionCompleted()
    {
        Debug.Log("Mission Completed");
        OnMissionCompleted?.Invoke();
        SoundManager.instance.PlayMusic(Soundlar.Victory);
        nextLevelButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        collectedKeys = 0;
        totalKeys = 0;
        int sceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneindex);
    }

    public void NextGame()
    {
        RestartGame();
    }
}
