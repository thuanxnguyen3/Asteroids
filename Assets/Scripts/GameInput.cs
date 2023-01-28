using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameInput : MonoBehaviour
{
    public AudioSource backgroundAudio;
    public AudioSource deathAudio;
    public AudioSource winAudio;

    public bool backgroundMusic = true;
    public bool deathMusic = false;
    public bool winMusic = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ReloadLevel();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void BackgroundAudio()
    {
        backgroundMusic = true;
        deathMusic = false;
        winMusic = false;
        backgroundAudio.Play();
    }

    public void DeathAudio()
    {
        if(backgroundAudio.isPlaying)
        {
            backgroundMusic = false;
            backgroundAudio.Stop();
        }
        if(!deathAudio.isPlaying && deathMusic == false)
        {
            deathAudio.Play();
            deathMusic = true;
        }
    }

    public void WinAudio()
    {
        if (backgroundAudio.isPlaying)
        {
            backgroundMusic = false;
            backgroundAudio.Stop();
        }
        if (!winAudio.isPlaying && winMusic == false)
        {
            winAudio.Play();
            winMusic = true;
        }
    }


    //restart
    public void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    //quit
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    
}
