using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{

    [SerializeField]
    private Slider _hpSlider = null;

    private int _hpMaxValue = 0;
    public int HPMaxValue
    {
        get => _hpMaxValue;
        set
        {
            _hpMaxValue = value;
            _hpSlider.maxValue = _hpMaxValue;
        }
    }

    private int _hpMinValue = 0;
    public int HPMinValue
    {
        get => _hpMinValue;
        set
        {
            _hpMinValue = value;
            _hpSlider.minValue = _hpMinValue;
        }
    }

    private int _hpValue = 0;
    public int HPValue
    {
        get => _hpValue;
        set
        {
            _hpValue = value;
            _hpSlider.value = _hpValue;
        }
    }

    [SerializeField]
    private TextMeshProUGUI _dashText = null;
    [SerializeField]
    private Color _EnableColor = Color.white;
    [SerializeField]
    private Color _DisableColor = Color.white;

    public void SetDashValue(int value, int maxValue)
    {
        if (value < 0) return;

        print($"Value : {value}, MaxValue : {maxValue}");

        string str = "";
        str += $"<#{ColorUtility.ToHtmlStringRGBA(_EnableColor)}>";
        for (int i = value; i < maxValue; i++)
        {
            str += "¢Â ";
        }
        str += "</color>";

        str += $"<#{ColorUtility.ToHtmlStringRGBA(_DisableColor)}>";
        for (int i = 0; i < value; i++)
        {
            str += "¢Â ";
        }
        str += "</color>";

        _dashText.text = str;
    }

    public void HpSliderInit(int minValue, int maxValue, int Value)
    {
        HPMinValue = minValue;
        HPMaxValue = maxValue;
        HPValue = Value;
    }
}
