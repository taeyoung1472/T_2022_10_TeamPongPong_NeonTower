using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer = null;
    [SerializeField]
    private Slider _masterSlider = null;
    [SerializeField]
    private Slider _bgmSlider = null;
    [SerializeField]
    private Slider _sfxSlider = null;

    public void Start()
    {
        _masterSlider.onValueChanged.AddListener(SetMasterVolume);
        _bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        _sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float value)
    {
        if (value == -30)
            value = -70;

        _audioMixer.SetFloat("Master", value);
    }
    public void SetBGMVolume(float value)
    {
        if (value == -30)
            value = -70;

        _audioMixer.SetFloat("BGM", value);
    }
    public void SetSFXVolume(float value)
    {
        if (value == -30)
            value = -70;

        _audioMixer.SetFloat("SFX", value);
    }
}
