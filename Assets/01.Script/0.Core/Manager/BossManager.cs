using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossManager : MonoSingleTon<BossManager>
{
    [SerializeField] private Boss[] boss;
    private int index = 0;
    public void ActiveBoss()
    {
        boss[index].gameObject.SetActive(true);
        index++;
    }
}
