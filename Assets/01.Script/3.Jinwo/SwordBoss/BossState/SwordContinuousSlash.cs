using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordContinuousSlash<T> : BossState<T> where T : Sword
{
    //2¹øÀÓ
    private Animator animator;
    private Transform characterTransform;


    private int hashAttack = Animator.StringToHash("Attack");

    public override void OnAwake()
    {
        animator = stateMachineOwnerClass.GetComponentInChildren<Animator>();
        characterTransform = stateMachineOwnerClass.GetComponent<Transform>();

    }
    public override void Enter()
    {
        animator.SetInteger(hashAttack, 2);
    }


    public override void Execute()
    {

    }

    public override void Exit()
    {

    }

}
