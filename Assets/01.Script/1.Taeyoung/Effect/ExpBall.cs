using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.U2D;

public class ExpBall : PoolAbleObject
{
    float speed = 0;
    void Update()
    {
        speed += Time.deltaTime;
        transform.position = Vector3.Slerp(transform.position, Define.Instance.playerController.transform.position, Time.deltaTime * (2.5f + speed));
        if(Vector3.Distance(transform.position, Define.Instance.playerController.transform.position) < 1f)
        {
            PoolManager.Instance.Push(PoolType, gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        PoolManager.Instance.Push(PoolType, gameObject);
        EXPManager.Instance.AddExp();
    }

    public override void Init_Pop()
    {
        speed = 0;
    }

    public override void Init_Push()
    {

    }
}