using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeMap : MonoBehaviour
{
    protected List<Transform> prevPattern = new();

    protected List<Pattern> patternList = new();
    protected int prevIdx = -1;
    protected virtual void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            patternList.Add(new Pattern(transform.GetChild(i)));
        }

        StartCoroutine(MapCycle());
    }
    protected virtual IEnumerator MapCycle()
    {
        while (true)
        {
            int randIdx = Random.Range(0, patternList.Count);
            while (randIdx == prevIdx)
            {
                randIdx = Random.Range(0, patternList.Count);
            }
            prevIdx = randIdx;

            foreach (Transform cube in prevPattern)
            {
                DeActiveCube(cube);
            }

            prevPattern.Clear();

            foreach (Transform cube in patternList[randIdx].Cubes)
            {
                ActiveCube(cube);
                prevPattern.Add(cube);
            }

            float cycleTime = Random.Range(7.5f, 12.5f);
            yield return new WaitForSeconds(cycleTime);
        }
    }

    protected virtual void ActiveCube(Transform cube)
    {
        Sequence seq = DOTween.Sequence();
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>();
        seq.AppendCallback(() => cube.gameObject.SetActive(true));
        seq.Append(cube.DOScaleY(0.1f + Random.Range(0, 0.1f), 1.5f));
        seq.Join(DOTween.To(() => renderer.material.color, x => renderer.material.color = x, Color.red, 0.75f));
        seq.Join(cube.DOShakePosition(1.5f, 0.1f));
        seq.Append(cube.DOScaleY(3 + Random.Range(0, 2f), 1f));
        seq.Join(DOTween.To(() => renderer.material.color, x => renderer.material.color = x, Color.yellow, 1));
    }
    protected virtual void DeActiveCube(Transform cube)
    {
        Sequence seq = DOTween.Sequence();
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>();
        seq.Append(cube.DOScaleY(0, 1f));
        seq.Join(DOTween.To(() => renderer.material.color, x => renderer.material.color = x, Color.white, 1));
        seq.AppendCallback(() => cube.gameObject.SetActive(false));
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(25, 5, 25));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(21, 5, 21));
    }
#endif

    protected class Pattern
    {
        public Pattern(Transform root)
        {
            patternRoot = root;
        }

        private Transform patternRoot;
        public Transform PatternRoot { get { return patternRoot; } set { patternRoot = value; } }
        private List<Transform> cubes = new();
        public Transform[] Cubes 
        { 
            get
            {
                if (cubes.Count == 0)
                {
                    for (int i = 0; i < patternRoot.childCount; i++)
                    {
                        cubes.Add(patternRoot.GetChild(i).transform);
                    }
                }
                return cubes.ToArray();
            }
        }
    }
}
