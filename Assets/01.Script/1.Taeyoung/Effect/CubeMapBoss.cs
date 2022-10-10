using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Collections;
using Unity.VisualScripting;

public class CubeMapBoss : CubeMap
{
    [SerializeField] private Pattern stadium;

    [ContextMenu("¿¢Æ¼ºê")]
    public void Active()
    {
        gameObject.SetActive(true);
        foreach (var cube in stadium.pattern)
        {
            ActiveCube(cube);
            cube.AddComponent<PerinNoiseCube>();
        }
    }
    public void DeActive()
    {

    }

    public void ActivePattern(int index)
    {
        foreach (var cube in prevPattern)
        {
            DeActiveCube(cube);
        }

        prevPattern.Clear();

        foreach (var cube in pattern[index].pattern)
        {
            ActiveCube(cube);
        }
    }
    class PerinNoiseCube : MonoBehaviour
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
    }
}