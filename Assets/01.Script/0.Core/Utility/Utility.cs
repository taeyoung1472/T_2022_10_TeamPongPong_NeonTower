using System;
using System.Collections;
using UnityEngine;

public static class Utility
{
    public static T ParseStringToEnum<T>(string s)
    {
        T returnValue;

        try
        {
            returnValue = (T)Enum.Parse(typeof(T), s);
        }
        catch
        {
            Error($"Enum({nameof(T)}) 에는 {s}가 없음");
            return default(T);
        }

        return returnValue;
    }
    public static int ParseStringToInt(string s)
    {
        int returnValue;

        try
        {
            returnValue = Convert.ToInt32(s);
        }
        catch
        {
            Error($"{s} 를 Int32 형으로 바꿀수 없음");
            return -1;
        }

        return returnValue;
    }
    public static void Invoke(this MonoBehaviour mb, Action ac, float delay)
    {
        mb.StartCoroutine(InvokeRoutine(ac, delay));
    }
    public static void DrawRay(Vector3 originPos, Vector3 dir, float distance = 10f, float duration = 1f, Color color = default(Color))
    {
        Debug.DrawRay(originPos, dir * distance, color, duration);
    }
    private static IEnumerator InvokeRoutine(Action ac, float delay)
    {
        yield return new WaitForSeconds(delay);
        ac();
    }
    public static Vector3 GetVecByAngle(float degrees, bool isLocalAngle = false, Transform refTrans = null) 
    { 
        if (isLocalAngle)
        { 
            degrees += refTrans.eulerAngles.y; 
        }
        return new Vector3(Mathf.Sin(degrees * Mathf.Deg2Rad), 0, Mathf.Cos(degrees * Mathf.Deg2Rad)); 
    }
    private static void Error(string errorString)
    {
        Debug.LogError($"Utility : {errorString}");
    }
}
