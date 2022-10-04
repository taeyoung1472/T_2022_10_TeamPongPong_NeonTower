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
            return;

        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (MenuManager.Instance.IsClicked)
            return;

        base.OnPointerExit(eventData);
    }
}
