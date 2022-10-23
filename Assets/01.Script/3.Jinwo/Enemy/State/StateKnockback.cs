using UnityEngine.AI;

public class StateKnockback<T> : State<T> where T : EnemyBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.GetComponent<NavMeshAgent>().enabled = false;
    }

    public override void Execute()
    {
        if (stateMachine.getStateDurationTime > UpgradeManager.Instance.GetUpgradeValue(UpgradeType.BulletKnockback))
        {
            stateMachine.ChangeState<StateMove<T>>();
        }
    }

    public override void Exit()
    {
        stateMachineOwnerClass.GetComponent<NavMeshAgent>().enabled = true;
    }
}
