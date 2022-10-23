using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ApplyAudioSound : MonoSingleTon<ApplyAudioSound>
{
    [SerializeField]
    private AudioMixer _audioMixer = null;
    [SerializeField]
    private float _maxVol = 0f;
    [SerializeField]
    private float _minVol = 0f;

    private readonly string _masterSoundKey = "VOL_MASTER";
    private readonly string _bgmSoundKey = "VOL_BGM";
    private readonly string _sfxSoundKey = "VOL_SFX";

    private float _masterSound = 0f;
    private float _bgmSound = 0f;
    private float _sfxSound = 0f;

    public float SfxSound
    {
        get => _sfxSound;
        set
        {
            _sfxSound = value;
            if (_sfxSound >= _maxVol)
            {
                _sfxSound = _maxVol;
            }
            else if (_sfxSound <= _minVol)
            {
                _sfxSound = _minVol;
            }
        }
    }

    public float BGMSound
    {
        get => _bgmSound;
        set
        {
            _bgmSound = value;
            if (_bgmSound >= _maxVol)
            {
                _bgmSound = _maxVol;
            }
            else if (_bgmSound <= _minVol)
            {
                _bgmSound = _minVol;
            }
        }
    }

    public float MasterSound
    {
        get => _masterSound;
        set
        {
            _masterSound = value;
            if (_masterSound >= _maxVol)
            {
                _masterSound = _maxVol;
            }
            else if (_masterSound <= _minVol)
            {
                _masterSound = _minVol;
            }
        }
    }

    private void Start()
    {
        Init();
        Apply();
        DontDestroyOnLoad(Instance);
    }

    private void Init()
    {
        _masterSound = PlayerPrefs.GetFloat(_masterSoundKey, (_maxVol + _minVol) * 0.5f);
        _bgmSound = PlayerPrefs.GetFloat(_bgmSoundKey, (_maxVol + _minVol) * 0.5f);
        _sfxSound = PlayerPrefs.GetFloat(_sfxSoundKey, (_maxVol + _minVol) * 0.5f);
    }

    private void Apply()
    {
        _audioMixer.SetFloat("MASTER", _masterSound);
        _audioMixer.SetFloat("BGM", _bgmSound);
        _audioMixer.SetFloat("SFX", _sfxSound);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(_masterSoundKey, _masterSound);
        PlayerPrefs.SetFloat(_bgmSoundKey, _bgmSound);
        PlayerPrefs.SetFloat(_sfxSoundKey, _sfxSound);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
