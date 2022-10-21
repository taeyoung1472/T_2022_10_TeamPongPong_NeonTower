using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBullet : MonoBehaviour
{
    public Collider col;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            Debug.Log(damageable);
            if (damageable != null)
            {
                damageable.ApplyDamage(1);
                Debug.Log("Ä¿¸ÕºÒ·¿" + damageable);
            }
        }
    }

}
