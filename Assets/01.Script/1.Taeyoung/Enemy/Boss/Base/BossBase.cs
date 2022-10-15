using UnityEngine;
using UnityEngine.Events;

public abstract class BossBase<T> : Boss
{
    [Header("[FSM]")]
    protected BossStateMachine<T> bossFsm;
    public BossStateMachine<T> BossFsm => bossFsm;

    public void LookTarget()
    {
        transform.rotation = Quaternion.LookRotation(Target.position - transform.position);
    }

    protected virtual void Update()
    {
        bossFsm.Execute();
    }
}