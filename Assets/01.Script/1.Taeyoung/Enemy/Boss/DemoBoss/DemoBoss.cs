using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DemoBoss : BossBase<DemoBoss>
{
    private void Start()
    {
        bossFsm = new BossStateMachine<DemoBoss>(this, new Idle<DemoBoss>());
        bossFsm.AddStateList(new CircleAttack<DemoBoss>());
    }
}
