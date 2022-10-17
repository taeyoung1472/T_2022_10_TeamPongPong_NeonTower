using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExpBall : MonoBehaviour
{
    private float time;
    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        time += Time.deltaTime;
        transform.position = Vector3.Slerp(startPos, Define.Instance.playerController.transform.position, time);
    }
}
