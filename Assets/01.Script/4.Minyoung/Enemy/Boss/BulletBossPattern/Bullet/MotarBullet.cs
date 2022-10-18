using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotarBullet : MonoBehaviour
{
    private PoolAbleObject motarEffect;

    private void Start()
    {
        //motarEffect = Resources.Load("")
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Debug.Log("Ãæµ¹");
           StartCoroutine( CreateMotarEffect());
        }
    }
    IEnumerator CreateMotarEffect()
    {
        GameObject effect = PoolManager.Instance.Pop(PoolType.BulletBossMortarEffect).gameObject;
        Debug.Log(effect);
        effect.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        CameraManager.Instance.CameraShake(25f, 30f, 0.2f);
        yield return new WaitForSeconds(1f);
    }
}
