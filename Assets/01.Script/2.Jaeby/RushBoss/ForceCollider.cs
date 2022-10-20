using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>()?.ApplyDamage(1);
        }
    }
}
