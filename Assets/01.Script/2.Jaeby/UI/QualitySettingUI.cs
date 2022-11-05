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
            //    str = "품질:최하";
            //    break;
            case 0:
                str = "품질:하";
                break;
            //case 2:
            //    str = "품질:중하";
            //    break;
            case 1:
                str = "품질:중";
                break;
            //case 4:
            //    str = "품질:중상";
            //    break;
            case 2:
                str = "품질:상";
                break;
            //case 6:
            //    str = "품질:최상";
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
