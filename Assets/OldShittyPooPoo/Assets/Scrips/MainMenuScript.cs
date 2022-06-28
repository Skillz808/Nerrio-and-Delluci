using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
    private int firstLaunch;

    public void PlayCredits()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadSceneAsync(0);
    }

    public void PlayTheGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    
    public void QuitTheGame()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Debug.Log("Quit Game");
        }
        else
        {
            Application.Quit();
            Debug.Log("Quit Game");
        }
    }

        
    void Start()
    {
        firstLaunch = PlayerPrefs.GetInt("firstLaunch");
        if (firstLaunch == 0)
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
            PlayerPrefs.SetFloat("sfxVolume", 1f);
            PlayerPrefs.SetInt("firstLaunch", 1);
        }
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
 }


