using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleTon<GameManager>
{
    public GameObject a;
    public void Awake()
    {
        JsonManager.Load();
        JsonManager.DisplayData();
        GameObject obj = new GameObject("@MonoHelper");
        obj.AddComponent<MonoHelper>();
    }

    public void OnApplicationQuit()
    {
        JsonManager.Save();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void LoadTuto()
    {
        SceneManager.LoadScene("Tutorial");
        Time.timeScale = 1;
    }
    public void LoadEnding()
    {
        SceneManager.LoadScene("Ending");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}