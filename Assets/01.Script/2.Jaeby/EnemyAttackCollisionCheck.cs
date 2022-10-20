using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAttackCollisionCheck : MonoBehaviour
{
    public static List<Collider> CheckSphere(Vector3 pos, float radius, LayerMask layerMask)
    {
        List<Collider> cols = new List<Collider>(Physics.OverlapSphere(pos, radius, layerMask));

        return cols;
    }

    public static List<Collider> CheckCube(Transform trm, float xSize, float zSize, LayerMask layerMask)
    {
        Vector3 center = trm.position + trm.forward * (zSize * 0.5f);
        Vector3 halfSize = new Vector3(xSize, 0f, zSize) * 0.5f;
        halfSize.y = 3f;
        List<Collider> cols = new List<Collider>(Physics.OverlapBox(center, halfSize, trm.rotation, layerMask));
        if(cols.Count > 0)
        {
        }
        return cols;
    }

    public static List<Collider> CheckArc(Transform trm, float angle, float radius, LayerMask layerMask)
    {
        Collider[] cols = Physics.OverlapSphere(trm.position, radius, layerMask);

        List<Collider> result = new List<Collider>();
        for (int i = 0; i < cols.Length; i++)
        {
            Vector3 forward = trm.forward;
            Vector3 target = cols[i].transform.position - trm.position;
            forward.y = 0f;
            target.y = 0f;

            float targetAngle = Vector3.Angle(forward, target);
            if (targetAngle < angle / 2f)
            {
                result.Add(cols[i]);
            }
        }
        if (result.Count > 0)
        {
        }

        return result;
    }

    public static void ApplyDamaged(List<Collider> list, int damage)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<IDamageable>()?.ApplyDamage(damage);
        }
    }
}
