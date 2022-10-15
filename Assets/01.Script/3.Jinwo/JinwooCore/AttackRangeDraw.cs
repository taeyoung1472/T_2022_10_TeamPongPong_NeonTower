using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeDraw : MonoBehaviour
{
    [Range(0f, 15f)]public float raidus = 0;
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, raidus);

    }
#endif
}
