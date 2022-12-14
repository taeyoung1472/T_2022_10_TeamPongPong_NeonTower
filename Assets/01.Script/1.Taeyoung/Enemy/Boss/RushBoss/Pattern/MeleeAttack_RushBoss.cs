using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeAttack_RushBoss<T> : BossState<RushBoss> where T : BossBase<T>
{
    public override void Enter()
    {
        if (stateMachineOwnerClass.IsFirst == true)
            stateMachineOwnerClass.IsFirst = false;

        stateMachineOwnerClass.ModelReset();
        stateMachineOwnerClass.Agent.ResetPath();
        stateMachineOwnerClass.Agent.isStopped = true;
        stateMachineOwnerClass.Agent.velocity = Vector3.zero;
        stateMachineOwnerClass.StartCoroutine(PunchCoroutine());
    }

    private IEnumerator PunchCoroutine()
    {
        stateMachineOwnerClass.LookTarget();
        DangerZone.DrawBox(stateMachineOwnerClass.transform.position + stateMachineOwnerClass.transform.forward * 5f, stateMachineOwnerClass.transform.rotation,
            new Vector3(4f, 0.1f, 9f), stateMachineOwnerClass.AttackDataSO.punchDelays[0]);
        stateMachineOwnerClass.AttackPositionObj.transform.SetPositionAndRotation(stateMachineOwnerClass.transform.position, stateMachineOwnerClass.transform.rotation);

        yield return new WaitForSeconds(stateMachineOwnerClass.AttackDataSO.punchDelays[0]);
        List<Collider> l = EnemyAttackCollisionCheck.CheckCube(stateMachineOwnerClass.AttackPositionObj.transform, 4f, 9.5f, 1 << 8);
        EnemyAttackCollisionCheck.ApplyDamaged(l, 1);

        stateMachineOwnerClass.Animator.SetTrigger("Punch");
        stateMachineOwnerClass.Animator.Update(0);
        CameraManager.Instance.CameraShake(5f, 20f, 0.2f);
        AudioManager.PlayAudio(stateMachineOwnerClass.PunchClip);
        yield return new WaitUntil(() => stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f);
        stateMachineOwnerClass.StopParticle();

        stateMachineOwnerClass.LookTarget();
        DangerZone.DrawArc(stateMachineOwnerClass.transform.position, stateMachineOwnerClass.transform.forward, 180f,
            new Vector3(6.5f, 0.1f, 14f), stateMachineOwnerClass.AttackDataSO.punchDelays[1]);
        stateMachineOwnerClass.AttackPositionObj.transform.SetPositionAndRotation(stateMachineOwnerClass.transform.position, stateMachineOwnerClass.transform.rotation);
        yield return new WaitForSeconds(stateMachineOwnerClass.AttackDataSO.punchDelays[1]);
        stateMachineOwnerClass.Animator.SetTrigger("Punch");
        stateMachineOwnerClass.Animator.Update(0);
        stateMachineOwnerClass.ArcAttack();
        CameraManager.Instance.CameraShake(7f, 20f, 0.3f);
        AudioManager.PlayAudio(stateMachineOwnerClass.PunchClip);
        List<Collider> list = EnemyAttackCollisionCheck.CheckArc(stateMachineOwnerClass.AttackPositionObj.transform, 40f, 12.5f, 1 << 8);
        EnemyAttackCollisionCheck.ApplyDamaged(list, 1);
        yield return new WaitUntil(() => stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f);
        stateMachineOwnerClass.StopParticle();


        stateMachineOwnerClass.LookTarget();
        DangerZone.DrawBox(stateMachineOwnerClass.transform.position + stateMachineOwnerClass.transform.forward * 8f, stateMachineOwnerClass.transform.rotation,
            new Vector3(4.5f, 0.1f, 17f), stateMachineOwnerClass.AttackDataSO.punchDelays[2]);
        stateMachineOwnerClass.AttackPositionObj.transform.SetPositionAndRotation(stateMachineOwnerClass.transform.position, stateMachineOwnerClass.transform.rotation);

        yield return new WaitForSeconds(stateMachineOwnerClass.AttackDataSO.punchDelays[2]);
        stateMachineOwnerClass.Animator.SetTrigger("Punch");
        stateMachineOwnerClass.Animator.Update(0);
        CameraManager.Instance.CameraShake(10f, 30f, 0.23f);
        AudioManager.PlayAudio(stateMachineOwnerClass.PunchClip);
        List<Collider> l2 = EnemyAttackCollisionCheck.CheckCube(stateMachineOwnerClass.AttackPositionObj.transform, 4.5f, 16.5f, 1 << 8);
        EnemyAttackCollisionCheck.ApplyDamaged(l2, 1);
        yield return new WaitUntil(() => stateMachineOwnerClass.Animator.GetCurrentAnimatorStateInfo(0).IsName("Punch2") == false);
        stateMachineOwnerClass.StopParticle();

        if (stateMachineOwnerClass.GetDistance() < stateMachineOwnerClass.AttackDataSO.groundPoundSize)
        {
            stateMachine.ChangeState<GroundPoundAttack_RushBoss<RushBoss>>();
        }
        else
        {
            stateMachine.ChangeState<WaveAttack_RushBoss<RushBoss>>();
        }
    }

    public override void Execute()
    {

    }

    /*public void AttackDamageArc()
    {
        Vector3 attackDir = Vector3.zero;
        attackDir = Define.Instance.playerController.transform.position - stateMachineOwnerClass.transform.position;
        attackDir.y = 0;
        // target?? ?? ?????? ?????? radius ???? ??????
        if (attackDir.magnitude <= 120f)
        {
            // '????-?? ????'?? '?? ???? ????'?? ????
            float dot = Vector3.Dot(stateMachineOwnerClass.transform.forward, attackDir.normalized);
            // ?? ???? ???? ???? ?????????? ???? ?????? cos?? ???? ?????? theta?? ????
            float theta = Mathf.Acos(dot);
            // angleRange?? ???????? ???? degree?? ????
            float degree = Mathf.Rad2Deg * theta;

            Debug.Log("???????? ???????? degree: " + degree);

            // ?????? ????
            if (degree <= 120f / 2f)
            {
                Debug.Log("???????? ???? ??????????");
                Collider[] col = Physics.OverlapSphere(st transform.position, 14f, playerLayer);

                if (col.Length > 0)
                {
                    var attackTargetEntity = col[0].GetComponent<IDamageable>();
                    if (!lastAttackedTargets.Contains(attackTargetEntity))
                    {
                        lastAttackedTargets.Add(attackTargetEntity);
                        attackTargetEntity.ApplyDamage((int)data.damage);
                    }
                }
            }
        }
    }*/

    public override void Exit()
    {
        stateMachineOwnerClass.Agent.isStopped = false;
        stateMachineOwnerClass.ModelReset();
    }

}
