using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBaldoAttack<T> : BossState<T> where T : Sword
{
    private Animator animator;
    private Transform characterTransform;


    private int hashAttack = Animator.StringToHash("attackType");

    public override void OnAwake()
    {
        animator = stateMachineOwnerClass.Animator;
        characterTransform = stateMachineOwnerClass.GetComponent<Transform>();

    }
    public override void Enter()
    {
        animator.SetInteger(hashAttack, stateMachineOwnerClass.currentAttackType + 1);
    }


    public override void Execute()
    {
        //var size = Physics.SphereCastNonAlloc(attackRoot.position, attackRadius, direction, hits, deltaDistance,
        //       whatIsTarget);
        if (!stateMachineOwnerClass.isAttacking)
        {
            stateMachineOwnerClass.BossFsm.ChangeState<SwordMove<Sword>>();
        }
    }

    public override void Exit()
    {
    }
}
