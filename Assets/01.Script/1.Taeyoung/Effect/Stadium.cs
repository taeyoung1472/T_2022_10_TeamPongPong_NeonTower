using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Stadium : CubeMap
{
    private Pattern stadium;

    protected override void OnEnable()
    {
        //DoNothing
    }

    public void Active()
    {
        gameObject.SetActive(true);
        if (stadium == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == 0)
                {
                    stadium = new Pattern(transform.GetChild(i));
                }
                else
                {
                    patternList.Add(new Pattern(transform.GetChild(i)));
                }
            }
        }
        foreach (var cube in stadium.Cubes)
        {
            ActiveCube(cube);
        }
        StadiumManager.Instance.ActivePattern();
    }
    public void ActivePattern(int index)
    {
        foreach (var cube in prevPattern)
        {
            DeActiveCube(cube);
        }

        prevPattern.Clear();

        foreach (var cube in patternList[index].Cubes)
        {
            ActiveCube(cube);
        }
    }
    public void DeActive()
    {
        foreach (var cube in stadium.Cubes)
        {
            DeActiveCube(cube);
        }
        this.Invoke(() => StadiumManager.Instance.DeActivePattern(), 4);
        this.Invoke(() => gameObject.SetActive(false), 5);
    }
#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(50, 5, 50));
        // Do Nothing
    }
#endif
}