using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMeleeAttack : State<CommonEnemy>
{
    private Animator animator;
    private Transform characterTransform;

    private float rotateSpeed = 10.0f;

    private int hashAttack = Animator.StringToHash("Attack");

    public override void OnAwake()
    {
        animator = stateMachineOwnerClass.GetComponent<Animator>();
        characterTransform = stateMachineOwnerClass.GetComponent<Transform>();
    }
    public override void Enter()
    {
        animator?.SetTrigger(hashAttack);
    }
    public override void Execute()
    {
        
    }
    public override void Exit()
    {

    }
    

}
