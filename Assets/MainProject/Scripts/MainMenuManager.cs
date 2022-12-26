using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainMenuManager : MonoBehaviour
{

    public void PlayGame()
    {
        Debug.Log("hello");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadScene(int sceneNum)
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(sceneNum);
    }

}