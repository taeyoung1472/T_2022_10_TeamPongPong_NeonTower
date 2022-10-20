using System;
using System.Collections.Generic;
using UnityEngine;

public class StadiumManager : MonoSingleTon<StadiumManager>
{
    [SerializeField] private Stadium[] stadiumMatches;
    public Stadium[] StadiumMatches { get { return stadiumMatches; } }

    [SerializeField] private CubeMap[] mapPatterns;
    public void ActivePattern()
    {
        foreach (var pattern in mapPatterns)
        {
            pattern.gameObject.SetActive(false);
        }
    }
    public void DeActivePattern()
    {
        foreach (var pattern in mapPatterns)
        {
            pattern.gameObject.SetActive(true);
        }
    }
}