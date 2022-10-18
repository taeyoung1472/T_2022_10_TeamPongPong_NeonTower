using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleTon<GameManager>
{
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
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadTuto()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void LoadEnding()
    {
        SceneManager.LoadScene("Ending");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}