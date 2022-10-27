using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResolutionUI : MonoBehaviour
{
    private List<Resolution> _resolutions = new List<Resolution>();
    [SerializeField]
    private TMP_Dropdown _resolutionDropDown = null;
    public int resolutionNum;
    private bool _fullScreen = false;

    [SerializeField]
    private TextMeshProUGUI _fullScreenText = null;

    private int _width = 1920;
    private int _height = 1080;
    private int _rate = 144;

    private void Start()
    {
        InitUI();
    }

    private void InitUI()
    {
        _fullScreen = PlayerPrefs.GetInt("FULL_SCREEN", 1) == 1 ? true : false;
        _width = PlayerPrefs.GetInt("WIDTH", Screen.currentResolution.width);
        _height = PlayerPrefs.GetInt("HEIGHT", Screen.currentResolution.height);
        _rate = PlayerPrefs.GetInt("RATE", Screen.currentResolution.refreshRate);
        OkBtnClick();

        _resolutions.AddRange(Screen.resolutions);
        _resolutionDropDown.options.Clear();
        int optionNum = 0;
        foreach (Resolution r in _resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = r.width + "x" + r.height + " " + r.refreshRate;
            _resolutionDropDown.options.Add(option);

            if (r.width == _width && r.height == _height)
            {
                _resolutionDropDown.value = optionNum;
            }

            optionNum++;
        }
        _resolutionDropDown.RefreshShownValue();
    }

    public void DropBoxOptionChange(int x)
    {
        resolutionNum = x;
        _width = _resolutions[resolutionNum].width;
        _height = _resolutions[resolutionNum].height;
        _rate = _resolutions[resolutionNum].refreshRate;
        PlayerPrefs.SetInt("WIDTH", _width);
        PlayerPrefs.SetInt("HEIGHT", _height);
        PlayerPrefs.SetInt("RATE", _rate);
        OkBtnClick();
    }

    public void FullScreenBtn()
    {
        _fullScreen = !_fullScreen;
        if(_fullScreen)
        {
            _fullScreenText.SetText("전체화면");
        }
        else
        {
            _fullScreenText.SetText("창화면");
        }
        PlayerPrefs.SetInt("FULL_SCREEN", _fullScreen == true ? 1 : 0);

        OkBtnClick();
    }

    private void OkBtnClick()
    {
        Screen.SetResolution(_width, _height, _fullScreen, _rate);
    }
}
