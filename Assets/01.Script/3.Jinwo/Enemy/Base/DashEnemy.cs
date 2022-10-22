using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : EnemyBase<DashEnemy>
{
    [SerializeField]
    private ParticleSystem particle;
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
        particle.gameObject.SetActive(false);
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

    public override void EnableAttack()
    {
        base.EnableAttack();
        particle.gameObject.SetActive(true);
        particle.Play();
    }
    public override void DisableAttack()
    {
        base.DisableAttack();
        //Debug.Log("change");
        particle.gameObject.SetActive(false);
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
