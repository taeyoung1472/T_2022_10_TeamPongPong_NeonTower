using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    public void Start()
    {
        AudioManager.PlayAudio(clip);
    }
}
