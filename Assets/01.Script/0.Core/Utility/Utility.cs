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
    private static IEnumerator InvokeRoutine(Action ac, float delay)
    {
        yield return new WaitForSeconds(delay);
        ac();
    }
    private static void Error(string errorString)
    {
        Debug.LogError($"Utility : {errorString}");
    }
}
