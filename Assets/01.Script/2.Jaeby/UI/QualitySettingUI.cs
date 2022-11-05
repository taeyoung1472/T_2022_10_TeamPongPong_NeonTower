using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QualitySettingUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _qualitySettingText = null;
    private int _qualityIndex = 0;

    private void Start()
    {
        InitUI();
    }

    private void InitUI()
    {
        _qualityIndex = PlayerPrefs.GetInt("QUALITY", (int)QualitySettings.names.Length - 1);
        QualitySettings.SetQualityLevel(_qualityIndex);
        ChangeQualityText();
    }

    private void ChangeQualityText()
    {
        string str = "";
        switch (_qualityIndex)
        {
            //case 0:
            //    str = "ǰ��:����";
            //    break;
            case 0:
                str = "ǰ��:��";
                break;
            //case 2:
            //    str = "ǰ��:����";
            //    break;
            case 1:
                str = "ǰ��:��";
                break;
            //case 4:
            //    str = "ǰ��:�߻�";
            //    break;
            case 2:
                str = "ǰ��:��";
                break;
            //case 6:
            //    str = "ǰ��:�ֻ�";
            //    break;
        }
        _qualitySettingText.SetText(str);
    }

    public void ChangeQuality()
    {
        _qualityIndex = (_qualityIndex + 1) % (int)QualitySettings.names.Length;
        QualitySettings.SetQualityLevel(_qualityIndex);
        PlayerPrefs.SetInt("QUALITY", _qualityIndex);
        ChangeQualityText();
    }
}
