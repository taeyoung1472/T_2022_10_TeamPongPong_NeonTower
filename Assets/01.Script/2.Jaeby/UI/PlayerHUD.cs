using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    private Slider _hpSlider = null;
    [SerializeField]
    private TextMeshProUGUI _hpText;

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
            _hpText.SetText($"{_hpValue}/{_hpMaxValue}");
        }
    }

    [SerializeField]
    private TextMeshProUGUI _dashText = null;
    [SerializeField]
    private Color _EnableColor = Color.white;
    [SerializeField]
    private Color _DisableColor = Color.white;

    StringBuilder _sb = null;

    private void Start()
    {
        _sb = new StringBuilder();
    }

    public void SetDashValue(int value, int maxValue)
    {
        if (value < 0) return;

        print($"Value : {value}, MaxValue : {maxValue}");

        _sb.Append($"<#{ColorUtility.ToHtmlStringRGBA(_EnableColor)}>");
        for (int i = 0; i < value; i++)
        {
            _sb.Append("¢Â");
        }
        _sb.Append("</color>");

        _sb.Append($"<#{ColorUtility.ToHtmlStringRGBA(_DisableColor)}>");
        for (int i = 0; i < maxValue - value; i++)
        {
            _sb.Append("¢Â");
        }
        _sb.Append("</color>");

        _dashText.text = _sb.ToString();
        _sb.Clear();
    }

    public void HpSliderInit(int minValue, int maxValue, int Value)
    {
        HPMinValue = minValue;
        HPMaxValue = maxValue;
        HPValue = Value;

    }
}
