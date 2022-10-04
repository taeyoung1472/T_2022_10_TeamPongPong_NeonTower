using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ColorCanvasEffect : MonoSingleTon<ColorCanvasEffect>
{
    [SerializeField] private Image colorPanel;

    public void Active(Color color, float duration = 1f, float intencity = 0.75f)
    {
        colorPanel.DOKill();

        color.a = intencity;

        colorPanel.color = color;
        colorPanel.DOFade(0, duration);
    }
}
