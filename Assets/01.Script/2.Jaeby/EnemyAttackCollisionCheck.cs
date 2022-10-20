using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAttackCollisionCheck : MonoBehaviour
{
    public static Collider[] CheckSphere(Vector3 position, float radius, LayerMask layerMask)
    {
        Collider[] cols = Physics.OverlapSphere(position, radius, layerMask);
        return cols;
    }
    public static bool CheckSphereBool(Vector3 position, float radius, LayerMask layerMask)
    {
        Collider[] cols = Physics.OverlapSphere(position, radius, layerMask);
        return cols.Length > 0;
    }

    public static Collider[] CheckCube(Vector3 center, Vector3 halfExtents, Quaternion orientation, LayerMask layerMask)
    {
        Collider[] cols = Physics.OverlapBox(center, halfExtents, orientation, layerMask);
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
            Debug.Log(targetAngle);
            if (targetAngle < angle / 2f)
            {
                result.Add(cols[i]);
            }
        }
        if (result.Count > 0)
        {
            Debug.Log("¸ÂÀ½");
        }

        return result;
    }

}
