using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushBossModelAnimationEvent : MonoBehaviour
{
    private RushBoss _rushBoss = null;

    private void Start()
    {
        _rushBoss = transform.parent.GetComponent<RushBoss>();
    }

    public void Punch()
    {
        _rushBoss.PunchParticlePlay();
    }

    public void Thunder()
    {
        _rushBoss.ThunderParticlePlay();
    }
}
