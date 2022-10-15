using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RushBoss : BossBase<RushBoss>
{
    private void Start()
    {
        bossFsm = new BossStateMachine<RushBoss>(this, new Idle_RushBoss<RushBoss>());
        bossFsm.AddStateList(new MeleeAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new WaveAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new RushAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new JumpAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new Move_RushBoss<RushBoss>());
    }
}
