using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimeEvent : MonoBehaviour
{

    public delegate void AnimationStartEvent();
    public delegate void AnimationEndEvent();
    public AnimationStartEvent startAnime;
    public AnimationEndEvent endAnime;
    public delegate void StartApplyDamageEvent();
    public StartApplyDamageEvent damageEvent;

    public delegate void DieEvent();
    public DieEvent dieEvent;

    public delegate void SoundEvent();
    public SoundEvent soundEvent;

    public void SoundPlayEvent()
    {
        soundEvent();
    }
    public void EnableEffect()
    {
        startAnime();
    }
    public void DisableEffect()
    {
        endAnime();
    }
    public void DamageEvent()
    {
        damageEvent();
    }
    public void StartDieEvent()
    {
        dieEvent();
    }
    public void CameraEffect()
    {
        CameraManager.Instance.CameraShake(25f, 30f, 0.2f);
    }

}
