using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTakeDownAttack<T> : BossState<T> where T : Sword
{
    //2¹øÀÓ
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
        if (!stateMachineOwnerClass.isAttacking)
        {
            stateMachineOwnerClass.BossFsm.ChangeState<SwordMove<Sword>>();
        }
    }

    public override void Exit()
    {

    }

}
