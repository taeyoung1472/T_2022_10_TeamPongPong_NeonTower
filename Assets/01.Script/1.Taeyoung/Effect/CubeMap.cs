using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeMap : MonoBehaviour
{
    protected List<Transform> prevPattern = new List<Transform>();

    [SerializeField] protected Pattern[] pattern;
    protected int prevIdx = -1;
    protected virtual void Start()
    {
        StartCoroutine(MapCycle());
    }
    protected virtual IEnumerator MapCycle()
    {
        while (true)
        {
            int randIdx = Random.Range(0, pattern.Length);
            while (randIdx == prevIdx)
            {
                randIdx = Random.Range(0, pattern.Length);
            }
            prevIdx = randIdx;

            foreach (Transform item in prevPattern)
            {
                Sequence seq = DOTween.Sequence();
                MeshRenderer renderer = item.GetComponent<MeshRenderer>();
                seq.Append(item.DOScaleY(0, 1f));
                seq.Join(DOTween.To(() => renderer.material.color, x => renderer.material.color = x, Color.white, 1));
                seq.AppendCallback(() => item.gameObject.SetActive(false));
            }
            prevPattern.Clear();
            foreach (Transform item in pattern[randIdx].pattern)
            {
                Sequence seq = DOTween.Sequence();
                MeshRenderer renderer = item.GetComponent<MeshRenderer>();
                seq.AppendCallback(() => item.gameObject.SetActive(true));
                seq.Append(item.DOScaleY(0.1f + Random.Range(0, 0.1f), 1.5f));
                seq.Join(DOTween.To(() => renderer.material.color, x => renderer.material.color = x, Color.red, 0.75f));
                seq.Join(item.DOShakePosition(1.5f, 0.1f));
                seq.Append(item.DOScaleY(3 + Random.Range(0, 2f), 1f));
                seq.Join(DOTween.To(() => renderer.material.color, x => renderer.material.color = x, Color.yellow, 1));
                prevPattern.Add(item);
            }
            float cycleTime = Random.Range(7.5f, 12.5f);
            yield return new WaitForSeconds(cycleTime);
        }
    }
#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(15, 5, 15));
    }
#endif
    [Serializable]
    protected class Pattern
    {
        public Transform[] pattern;
    }
}
