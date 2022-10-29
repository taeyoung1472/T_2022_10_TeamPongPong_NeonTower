using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeMap : MonoBehaviour
{
    protected List<Transform> prevPattern = new();

    protected List<Pattern> patternList = new();
    protected int prevIdx = -1;
    protected virtual void OnEnable()
    {
        if (patternList.Count == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                patternList.Add(new Pattern(transform.GetChild(i)));
            }
        }

        StartCoroutine(MapCycle());
    }
    protected virtual void OnDisable()
    {
        foreach (Transform cube in prevPattern)
        {
            cube.gameObject.SetActive(false);
        }
        prevPattern.Clear();
    }
    [ContextMenu("GenerateSeed")]
    public void GenerateSeed()
    {
        if (patternList.Count == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                patternList.Add(new Pattern(transform.GetChild(i)));
            }
        }
        for (int i = 0; i < patternList.Count; i++)
        {
            Pattern targetPattern = patternList[i];
            List<Transform> seedTargets = new();
            List<Transform> totalIndex = new();

            Debug.Log(targetPattern.Cubes.Length);
            //큐브들 집어넣기
            for (int j = 0; j < targetPattern.Cubes.Length; j++)
            {
                totalIndex.Add(targetPattern.Cubes[j]);
                targetPattern.Cubes[j].gameObject.SetActive(true);
                targetPattern.Cubes[j].transform.localScale = Vector3.one;
            }

            //셔플
            int n = totalIndex.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n);
                Transform value = totalIndex[k];
                totalIndex[k] = totalIndex[n];
                totalIndex[n] = value;
            }

            //SEED 만들기
            for (int j = 0; j < 10; j++)
            {
                GameObject seed = new GameObject();
                seed.name = $"SEED {j}";
                seed.transform.SetParent(targetPattern.PatternRoot);
                seed.transform.localScale = new Vector3(1, 1, 1);
                seed.gameObject.SetActive(false);
                seedTargets.Add(seed.transform);
            }

            //SEED 수 만큼
            int count = targetPattern.Cubes.Length / 10;
            for (int j = 0; j < 10; j++)
            {
                List<Transform> seedInputs = new();
                if (j == 9)
                {
                    count = totalIndex.Count;
                    Debug.Log($"마무리 : {count}");
                    for (int l = 0; l < count; l++)
                    {
                        seedInputs.Add(totalIndex[0]);
                        totalIndex.Remove(totalIndex[0]);
                    }
                }
                else
                {
                    Debug.Log($"프로그래스 : {count}, Total Count : {totalIndex.Count}");
                    for (int l = 0; l < count; l++)
                    {
                        seedInputs.Add(totalIndex[0]);
                        totalIndex.Remove(totalIndex[0]);
                    }
                }

                for (int k = 0; k < seedInputs.Count; k++)
                {
                    seedInputs[k].transform.SetParent(seedTargets[j]);
                }
                Debug.Log("--------------------");
            }
        }
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
        seq.AppendCallback(() => cube.gameObject.SetActive(true));
        seq.Append(cube.DOScaleY(0.1f + Random.Range(0, 0.1f), 1.5f));
        seq.Join(cube.DOShakePosition(1.5f, 0.1f));
        seq.Append(cube.DOScaleY(3 + Random.Range(0, 2f), 1f));
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
