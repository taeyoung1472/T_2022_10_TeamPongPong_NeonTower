using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleTon<GameManager>
{
    public PlayerStat playerStat;
    public BulletStat bulletStat;

    public void Awake()
    {
        JsonManager.Load();
        JsonManager.DisplayData();
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

    public void QuitGame()
    {
        Application.Quit();
    }
}

[Serializable]
public class PlayerStat
{
    public int hp = 5;
    public int speed = 10;
    public int autoHealDelay = 16;
    public int stealHp;
    public int dashChance;
    public int dashGod;
}

[Serializable]
public class BulletStat
{
    public int knockback = 0;
    public int wallBounceCnt = 0;
    public int bulletSpd = 0;
    public int damage = 0;
    public int bulletDelay = 0;
    public int freeze = 0;

    public int multiShotCount = 0;
    public int penetrationShot = 0;
    public int explosion = 0;
}