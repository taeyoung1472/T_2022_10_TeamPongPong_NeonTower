using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class MenuButton : InteractiveButton
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (MenuManager.Instance.IsClicked)
        {
            Debug.Log("Eenter");
            return;
        }

        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (MenuManager.Instance.IsClicked)
        {
            Debug.Log("EXX");
            return;
        }

        base.OnPointerExit(eventData);
    }
}
