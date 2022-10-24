using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerModel : MonoBehaviour
{
    [SerializeField]
    private AudioClip _laserClip = null;

    public void StartLaserClip()
    {
        AudioManager.PlayAudio(_laserClip);
    }
}
