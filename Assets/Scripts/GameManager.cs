using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject sndManager;
    //LEVEL STATES
    public bool isGameOver = false;
    public bool isLevelRestarted = false;
    public bool isLevelCompleted = false;
    public bool isGameCompleted = false;
    public int playerLifes = 3;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += ResetPlayerLifes;
    }

    void Start()
    {
        //SOUNDMANAGER
        InitSoundManager();
    }

    void InitSoundManager()
    {
        sndManager = GameObject.FindGameObjectWithTag("SoundManager");
        sndManager.GetComponent<SoundManager>().SetMusicVolume(PlayerPrefs.GetFloat("musicVolume"));
        sndManager.GetComponent<SoundManager>().fxVolume = PlayerPrefs.GetFloat("fxVolume");
        sndManager.GetComponent<SoundManager>().SetEnableMusic(PlayerPrefs.GetInt("music") == 1 ? true : false);
        sndManager.GetComponent<SoundManager>().isFXEnabled = PlayerPrefs.GetInt("fx") == 1 ? true : false;
        sndManager.GetComponent<SoundManager>().PlayMusic(0);
    }

    void Reset()
    {
        isGameOver = false;
        isLevelRestarted = false;
        isLevelCompleted = false;
        isGameCompleted = false;
        playerLifes = 3;
    }

    void OnGUI()
    {
        if (isGameCompleted || isGameOver)
        {
            GUIStyle myButtonStyle = new(GUI.skin.button)
            {
                fontSize = 25
            };
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 8, Screen.height / 2 - Screen.height / 8, Screen.width / 4, Screen.height / 4), isGameCompleted ? "CONGRATULATIONS!!" : "GAMEOVER!!", myButtonStyle))
            {
                Reset();
                SceneManager.LoadScene(1);
            }
        }
    }

    public void PlayerDeath()
    {
        playerLifes--;
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CompleteLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            isGameCompleted = true;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void ResetPlayerLifes(Scene arg0, Scene arg1)
    {
        if(playerLifes != 3 && arg1.buildIndex == 1 && arg0.buildIndex != 5)
        {
            playerLifes = 3;
        }
    }
}