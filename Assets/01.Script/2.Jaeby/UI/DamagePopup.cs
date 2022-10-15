using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamagePopup
{
    public static void PopupDamage(Vector3 startPos, string text)
    {
        PopupPoolObject popupPoolObj = PoolManager.Instance.Pop(PoolType.PopupText) as PopupPoolObject;
        popupPoolObj.PopupTextCritical(startPos, text);
    }
}
