using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : PoolAbleObject
{
    [SerializeField] private float poolTime;
    ParticleSystem particle = null;
    public override void Init_Pop()
    {
        if(particle == null)
        {
            particle = GetComponent<ParticleSystem>();
        }
        particle.Play();
        StartCoroutine(Wait());
    }

    public override void Init_Push()
    {
        particle.Stop();
    }

    public void Set(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);//transform.position = info.point + info.normal * 0.15f;
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(poolTime);
        PoolManager.Instance.Push(PoolType, gameObject);
    }
}
