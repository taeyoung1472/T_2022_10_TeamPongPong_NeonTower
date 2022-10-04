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
            Error($"Enum({nameof(T)}) ���� {s}�� ����");
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
            Error($"{s} �� Int32 ������ �ٲܼ� ����");
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
