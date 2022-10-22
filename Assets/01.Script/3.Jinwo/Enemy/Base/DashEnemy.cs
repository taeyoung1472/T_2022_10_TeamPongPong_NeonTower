using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : EnemyBase<DashEnemy>
{
    [SerializeField]
    private MeshAfterImage motionTrail;
    protected override void Awake()
    {
        base.Awake();
        motionTrail = GetComponentInChildren<MeshAfterImage>();

        fsmManager = new StateMachine<DashEnemy>(this, new StateMove<DashEnemy>());

        fsmManager.AddStateList(new StateDashAttack<DashEnemy>());
        fsmManager.AddStateList(new StateMeleeAttack<DashEnemy>());
        //fsmManager.AddStateList(new StateMeleeAttack<DashEnemy>());

        //fsmManager.ReturnDic();

    }
    private void Start()
    {
        StopMotionTrail();
    }
    void Update()
    {
        fsmManager.Execute();
    }
    public override void ChangeAttack()
    {
        FsmManager.ChangeState<StateDashAttack<DashEnemy>>();
    }


    public override void DisableAttack()
    {
        base.DisableAttack();
        //Debug.Log("change");
        FsmManager.ChangeState<StateMove<DashEnemy>>();
    }
    public override void StartMotionTrail()
    {
        motionTrail.isMotionTrail = true;
    }
    public override void StopMotionTrail()
    {
        motionTrail.isMotionTrail = false;

    }
}
