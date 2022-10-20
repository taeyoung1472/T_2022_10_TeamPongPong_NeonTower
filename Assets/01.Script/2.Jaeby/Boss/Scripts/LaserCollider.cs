using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollider : MonoBehaviour
{
    private float _curTime = 0.45f;
    private float _damageCooltime = 0.45f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_curTime > _damageCooltime)
            {
                _curTime = 0f;
                other.GetComponent<IDamageable>()?.ApplyDamage(1);
            }
        }
    }

    private void Update()
    {
        _curTime += Time.deltaTime;
    }
}
