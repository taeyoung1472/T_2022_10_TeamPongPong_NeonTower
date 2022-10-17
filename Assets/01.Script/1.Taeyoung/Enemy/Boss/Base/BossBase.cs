using UnityEngine;
using UnityEngine.AI;

public abstract class BossBase<T> : Boss
{
    [Header("[FSM]")]
    protected BossStateMachine<T> bossFsm;
    public BossStateMachine<T> BossFsm => bossFsm;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    public void LookTarget()
    {
        transform.rotation = Quaternion.LookRotation(Target.position - transform.position);
    }

    public Vector3 GetDirToTarget()
    {
        return (Target.position - transform.position).normalized;
    }

    protected virtual void Update()
    {
        bossFsm.Execute();
    }
}