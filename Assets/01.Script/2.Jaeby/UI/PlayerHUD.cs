using DG.Tweening;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("[HP 관련]")]
    [SerializeField] private Transform hpRoot;
    [SerializeField] private Image hpImage;
    [SerializeField] private Color hpEnableColor = Color.white;
    [SerializeField] private Color hpDisableColor = Color.white;
    private List<Image> hpImageList = new();

    [Header("[DASH 관련]")]
    [SerializeField] private TextMeshProUGUI _dashText = null;
    [SerializeField] private Color _EnableColor = Color.white;
    [SerializeField] private Color _DisableColor = Color.white;

    StringBuilder _sb = null;

    public void SetDashValue(int value, int maxValue)
    {
        if (value < 0) return;

        print($"Value : {value}, MaxValue : {maxValue}");

        if (_sb == null) _sb = new();

        _sb.Append($"<#{ColorUtility.ToHtmlStringRGBA(_EnableColor)}>");
        for (int i = 0; i < value; i++)
        {
            _sb.Append("◈");
        }
        _sb.Append("</color>");

        _sb.Append($"<#{ColorUtility.ToHtmlStringRGBA(_DisableColor)}>");
        for (int i = 0; i < maxValue - value; i++)
        {
            _sb.Append("◈");
        }
        _sb.Append("</color>");

        _dashText.text = _sb.ToString();
        _sb.Clear();
    }

    public void SetHpUI(int curHp, int maxHp)
    {
        if (hpRoot.childCount < maxHp)
        {
            int temp = (maxHp - hpRoot.childCount) > 10 ? 10 : maxHp - hpRoot.childCount;
            for (int i = 0; i < temp; i++)
            {
                Image hpObj = Instantiate(hpImage, hpRoot);
                hpObj.color = hpDisableColor;
                hpImageList.Add(hpObj);
            }
        }

        int idx = 0;
        foreach (Image hpImage in hpImageList)
        {
            if (idx < curHp)
            {
                DOTween.To(() => hpImage.color, x => hpImage.color = x, hpEnableColor, 1f).SetUpdate(true);
            }
            else
            {
                DOTween.To(() => hpImage.color, x => hpImage.color = x, hpDisableColor, 1f);
            }

            idx++;
        }
    }
}
