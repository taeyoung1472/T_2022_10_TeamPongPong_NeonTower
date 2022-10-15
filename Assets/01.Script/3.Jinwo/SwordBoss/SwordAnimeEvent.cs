using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimeEvent : MonoBehaviour
{

    public delegate void AnimationStartEvent();
    public delegate void AnimationEndEvent();
    public AnimationStartEvent startAnime;
    public AnimationEndEvent endAnime;

    public void EnableEffect()
    {
        startAnime();
    }
    public void DisableEffect()
    {
        endAnime();
    }
}
