using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveAttack_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        stateMachineOwnerClass.transform.LookAt(stateMachineOwnerClass.Target.position);
        stateMachineOwnerClass.StartCoroutine(WaveCoroutine());
    }

    private IEnumerator WaveCoroutine()
    {
        float size = stateMachineOwnerClass.AttackDataSO.waveAttackSize;
        float z = stateMachineOwnerClass.AttackDataSO.waveAttackSize;
        Vector3 plus = stateMachineOwnerClass.transform.forward;

        List<Vector3> positions = new List<Vector3>();

        DangerZone.DrawCircle(stateMachineOwnerClass.transform.position + plus * (z * 0.5f), z, 1.5f);
        positions.Add(stateMachineOwnerClass.transform.position + plus * (z * 0.5f));
        DangerZone.DrawCircle(stateMachineOwnerClass.transform.position + plus * (z * 0.5f) + plus * size, z, 1.5f);
        positions.Add(stateMachineOwnerClass.transform.position + plus * (z * 0.5f) + plus * size);
        size += z;
        DangerZone.DrawCircle(stateMachineOwnerClass.transform.position + plus * (z * 0.5f) + plus * size, z, 1.5f);
        positions.Add(stateMachineOwnerClass.transform.position + plus * (z * 0.5f) + plus * size);
        size += z;
        DangerZone.DrawCircle(stateMachineOwnerClass.transform.position + plus * (z * 0.5f) + plus * size, z, 1.5f);
        positions.Add(stateMachineOwnerClass.transform.position + plus * (z * 0.5f) + plus * size);
        yield return new WaitForSeconds(0.8f);

        stateMachineOwnerClass.Animator.Play("GroundPound");
        stateMachineOwnerClass.Animator.Update(0);
        yield return new WaitUntil(() => stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).IsName("GroundPound") == false);
        CameraManager.Instance.CameraShake(12f, 30f, 0.23f);
        AudioManager.PlayAudio(stateMachineOwnerClass.ExploClip);
        List<Collider> cols = new List<Collider>();
        for (int i = 0; i < positions.Count; i++)
        {
            cols = EnemyAttackCollisionCheck.CheckSphere(positions[i], z / 2f, 1 << 8);
            stateMachineOwnerClass.ExplosionEffect(positions[i]);
            EnemyAttackCollisionCheck.ApplyDamaged(cols, 1);
            cols.Clear();
        }

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
