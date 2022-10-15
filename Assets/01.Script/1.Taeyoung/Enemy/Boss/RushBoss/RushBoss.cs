using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RushBoss : BossBase<RushBoss>
{
    private float movement;
    private float movementGoal = 0;
    public float MovementGoal { get { return movementGoal; } set { movementGoal = value; } }

    private void Start()
    {
        bossFsm = new BossStateMachine<RushBoss>(this, new Idle_RushBoss<RushBoss>());
        bossFsm.AddStateList(new MeleeAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new WaveAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new RushAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new JumpAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new Move_RushBoss<RushBoss>());
    }

    protected override void Update()
    {
        base.Update();
        movement = Mathf.Lerp(movement, movementGoal, Time.deltaTime);
        animator.SetFloat("Movement", movement);
    }
}
