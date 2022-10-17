using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public static class DangerZone
{
    static GameObject boxDangerZone;
    static GameObject arcDangerZone;
    static GameObject circleDangerZone;

    static GameObject ArcDangerZone { get { if (arcDangerZone == null) { arcDangerZone = Resources.Load("Danger/Arc") as GameObject; } return arcDangerZone; } }
    static GameObject BoxDangerZone { get { if (boxDangerZone == null) { boxDangerZone = Resources.Load("Danger/Box") as GameObject; } return boxDangerZone; } }
    static GameObject CircleDangerZone { get { if (circleDangerZone == null) { circleDangerZone = Resources.Load("Danger/Circle") as GameObject; } return circleDangerZone; } }

    public static void DrawBox(Vector3 startPos, Quaternion rot, Vector3 size, float duration)
    {
        GameObject dangerObj = MonoHelper._Instantiate(BoxDangerZone, startPos, rot);
        dangerObj.transform.localScale = size;
        MonoHelper._Destroy(dangerObj, duration);
    }
    public static void DrawArc(Vector3 startPos, Vector3 normal, float angle, Vector3 size, float duration)
    {
        GameObject dangerObj = MonoHelper._Instantiate(ArcDangerZone, startPos, Quaternion.LookRotation(normal));
        dangerObj.transform.localScale = size;
        MonoHelper._Destroy(dangerObj, duration);
    }
    public static void DrawCircle(Vector3 startPos, float radius, float duration)
    {
        GameObject dangerObj = MonoHelper._Instantiate(CircleDangerZone, startPos, Quaternion.identity);
        dangerObj.transform.localScale = Vector3.one * radius;
        MonoHelper._Destroy(dangerObj, duration);
    }
}
public class MonoHelper : MonoBehaviour
{
    public static GameObject _Instantiate(GameObject obj, Vector3 pos, Quaternion rot)
    {
        GameObject returnObj = Instantiate(obj, pos, rot);
        return returnObj;
    }
    public static void _Destroy(GameObject obj, float dur)
    {
        Destroy(obj, dur);
    }
}