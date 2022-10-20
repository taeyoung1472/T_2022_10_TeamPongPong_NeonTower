using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoundAttack_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.StartCoroutine(WaitAnimation());
    }

    private IEnumerator WaitAnimation()
    {
        stateMachineOwnerClass.AttackPositionObj.transform.SetPositionAndRotation(stateMachineOwnerClass.transform.position, stateMachineOwnerClass.transform.rotation);
        DangerZone.DrawCircle(stateMachineOwnerClass.transform.position, stateMachineOwnerClass.AttackDataSO.groundPoundSize * 2f + 1f, 1f);
        yield return new WaitForSeconds(0.5f);
        stateMachineOwnerClass.Animator.Play("GroundPound");
        stateMachineOwnerClass.Animator.Update(0);
        yield return new WaitUntil(()=> stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).IsName("GroundPound") == false);
        CameraManager.Instance.CameraShake(12f, 30f, 0.23f);
        stateMachineOwnerClass.ExplosionEffect(stateMachineOwnerClass.transform.position);
        List<Collider> l = EnemyAttackCollisionCheck.CheckSphere(stateMachineOwnerClass.AttackPositionObj.transform.position, stateMachineOwnerClass.AttackDataSO.groundPoundSize, 1 << 8);
        EnemyAttackCollisionCheck.ApplyDamaged(l, 1);

        yield return new WaitForSeconds(1f);
        stateMachine.ChangeState<Idle_RushBoss<RushBoss>>();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
