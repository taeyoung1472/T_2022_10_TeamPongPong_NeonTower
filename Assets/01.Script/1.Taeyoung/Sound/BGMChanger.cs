using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class BGMChanger : MonoSingleTon<BGMChanger>
{
    [SerializeField] private ClipMatch[] clipMatch;
    private Dictionary<BGMType, AudioClip> clipDic = new();
    AudioSource bgmSource;

    public void Awake()
    {
        bgmSource = GetComponent<AudioSource>();
        foreach (ClipMatch match in clipMatch)
        {
            clipDic.Add(match.type, match.clip);
        }
    }

    public void ActiveAudio(BGMType type)
    {
        StartCoroutine(ChangeBGM(clipDic[type]));
    }

    IEnumerator ChangeBGM(AudioClip newClip)
    {
        float vol;

        if(bgmSource.clip != null)
        {
            vol = 1;
            while (vol >= 0)
            {
                bgmSource.volume = vol;
                vol -= Time.deltaTime;
                yield return null;
            }
        }
        bgmSource.clip = newClip;
        bgmSource.Play();
        vol = 0;
        while (vol <= 1)
        {
            bgmSource.volume = vol;
            vol += Time.deltaTime;
            yield return null;
        }
    }

    [Serializable]
    public class ClipMatch
    {
        public BGMType type;
        public AudioClip clip;
    }
}
public enum BGMType
{
    Default,
    Boss,
}