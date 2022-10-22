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
    /*class PerinNoiseCube : MonoBehaviour
    {
        MeshRenderer meshRenderer;
        float perinValue;
        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        private void Update()
        {
            perinValue = Mathf.PerlinNoise((transform.position.x + Time.time * 5) * 0.1f, (transform.position.z + Time.time * 5) * 0.1f);

            transform.localScale = new Vector3(1, Mathf.Lerp(transform.localScale.y, 3 + perinValue * 2.5f, Time.deltaTime * 5), 1);
            meshRenderer.material.color = Color.Lerp(Color.white, Color.black, perinValue);
        }
    }*/

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(50, 5, 50));
        // Do Nothing
    }
#endif
}