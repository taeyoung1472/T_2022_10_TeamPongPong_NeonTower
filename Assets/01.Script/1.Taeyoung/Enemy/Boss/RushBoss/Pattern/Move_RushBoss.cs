using UnityEngine;

public class Move_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    //Vector3 goalPoint;
    Transform transform;
    Transform target;
    public override void Enter()
    {
        target = stateMachineOwnerClass.Target;

        stateMachineOwnerClass.MovementGoal = 1;
        stateMachineOwnerClass.Agent.enabled = true;
        transform = stateMachineOwnerClass.transform;
    }

    public override void Execute()
    {
        stateMachineOwnerClass.Agent.SetDestination(target.position);
        if (Vector3.Distance(transform.position, target.position) < 1)
        {
            stateMachine.ChangeState<Idle_RushBoss<RushBoss>>();
        }
    }

    public override void Exit()
    {
        stateMachineOwnerClass.Agent.enabled = false;
    }
}
