using UnityEngine;

public class Move_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.After.isMotionTrail = true;

        stateMachineOwnerClass.ModelReset();
        stateMachineOwnerClass.Agent.ResetPath();
        BossUIManager.Instance.BossPopupText("보스가 움직입니다", 0.5f, 0);
        stateMachineOwnerClass.Animator.SetBool("Run", true);
        stateMachineOwnerClass.Agent.speed = stateMachineOwnerClass.AttackDataSO.normalSpeed;
        stateMachineOwnerClass.Agent.SetDestination(stateMachineOwnerClass.Target.position);
    }

    public override void Execute()
    {

        stateMachineOwnerClass.TargetLook();


        stateMachineOwnerClass.Agent.SetDestination(stateMachineOwnerClass.Target.position);
        if (stateMachineOwnerClass.Agent.remainingDistance <= stateMachineOwnerClass.Agent.stoppingDistance)
        {
            stateMachine.ChangeState<Idle_RushBoss<RushBoss>>();
        }
    }

    public override void Exit()
    {
        stateMachineOwnerClass.After.isMotionTrail = false;

        stateMachineOwnerClass.ModelReset();
        stateMachineOwnerClass.Agent.ResetPath();
        stateMachineOwnerClass.Animator.SetBool("Run", false);
    }
}
