using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoSingleTon<Define>
{
    public PlayerController playerController;
    private int difficulty = -1;
    public int Difficulty { 
        get 
        {
            if(difficulty == -1)
            {
                difficulty = PlayerPrefs.GetInt("resurrectionCount");
            }
            return difficulty;
        }
    }
}