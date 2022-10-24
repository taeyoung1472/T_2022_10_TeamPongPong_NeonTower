using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotarBullet : MonoBehaviour
{
    private PoolAbleObject motarEffect;

    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private AudioClip boomClip;

    private Collider col;

    [SerializeField]
    private float CircleSize = 3f;

    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            StartCoroutine(CreateMotarEffect());
        }

        AttackDamage();
    }
    public void AttackDamage()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, CircleSize, targetLayer);

        AudioManager.PlayAudio(boomClip);
        foreach (var col in cols)
        {
            IDamageable damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.ApplyDamage(1);
                Debug.Log("¹Ú°ÝÆ÷" + damageable);
                break;
            }
        }
    }

    IEnumerator CreateMotarEffect()
    {
        GameObject effect = PoolManager.Instance.Pop(PoolType.BulletBossMortarEffect).gameObject;
        effect.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        CameraManager.Instance.CameraShake(25f, 30f, 0.2f);
        yield return new WaitForSeconds(1f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, CircleSize);
    }
#endif
}