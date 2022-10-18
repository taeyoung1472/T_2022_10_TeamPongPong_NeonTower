using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordMove<T> : BossState<T> where T : Sword
{

    private Animator animator;

    private NavMeshAgent agent;

    private int hashMove = Animator.StringToHash("Move");

    private Vector3 target;

    private Transform characterTrm;

    private bool isTeleport = false;
    public override void OnAwake()
    {
        characterTrm = stateMachineOwnerClass.GetComponent<Transform>();
        animator = stateMachineOwnerClass.Animator;

        agent = stateMachineOwnerClass.Agent;

        //agent.stoppingDistance = 7f;

    }
    public override void Enter()
    {
        animator?.SetBool(hashMove, true);

        agent.speed = stateMachineOwnerClass.dashspeed;
        agent.isStopped = false;
        isTeleport = false;
        agent.stoppingDistance = 1f;

        target = stateMachineOwnerClass.Target.transform.position;
        stateMachineOwnerClass.attackCoolTime = 0;
        stateMachineOwnerClass.motionTrail.isMotionTrail = true;
        CoroutineHelper.StartCoroutine(SwordDash());
    }

    public override void Execute()
    {
        //if (isTeleport == true)
        //{
        //    stateMachineOwnerClass.LookTarget();
        //    characterTrm.position += Vector3.Lerp(characterTrm.position, target, Time.deltaTime * 0.25f);
        //}
    }

    public override void Exit()
    {
        animator?.SetBool(hashMove, false);

        //stateMachineOwnerClass.motionTrail.isMotionTrail = false;
        //agent.ResetPath();
    }
    public IEnumerator SpawnDangerZome(int a)
    {
        Vector3 d = target - characterTrm.position;
        d.Normalize();
        switch (a)
        {
            case 1:
                DangerZone.DrawCircle(target, stateMachineOwnerClass.radius[a - 1], 1.5f); // 외곡 찍기
                break;
            case 2:
                yield return new WaitForSeconds(1.5f);
                agent.isStopped = true;
                target = stateMachineOwnerClass.Target.position;
                d = target - characterTrm.position;
                d.Normalize();
                DangerZone.DrawArc(characterTrm.position, d, 5, new Vector3(2, 0.1f, 7), 2f); // 내려찍기
                break;

            case 3:
                yield return new WaitForSeconds(1.5f);
                agent.isStopped = true;
                target = stateMachineOwnerClass.Target.position;
                d = target - characterTrm.position;
                d.Normalize();
                DangerZone.DrawArc(characterTrm.position, d, 4, new Vector3(2.5f, 0.1f, 14), 2f); //회오리 참격베기
                break;
            case 4:
                DangerZone.DrawCircle(target, stateMachineOwnerClass.radius[a - 1], 1.5f);  //연속베기 (콤보1)
                break;
            case 5:
                DangerZone.DrawCircle(target, stateMachineOwnerClass.radius[a - 1], 1.5f);  //연속베기 (콤보2)
                break;
            case 6:
                DangerZone.DrawCircle(target, stateMachineOwnerClass.radius[a - 1], 1.5f); //원 범위 공격 ( 텔, 대쉬해서 공격)
                break;
            case 7:
                yield return new WaitForSeconds(1.5f);
                agent.isStopped = true;
                target = stateMachineOwnerClass.Target.position;
                d = target - characterTrm.position;
                d.Normalize();
                DangerZone.DrawArc(characterTrm.position, d, 2, new Vector3(4, 0.1f, 7), 2f); //발도 기술
                break;
        }
    }
    
    public IEnumerator SwordDash()
    {
        float startTime = Time.time;
        
        int a = stateMachineOwnerClass.SelectAttackType();

        

        target = stateMachineOwnerClass.Target.position;
        Vector3 dangerzonePos = target;
        dangerzonePos.y = 0.1f;
        switch (a)
        {
            case 1:
                DangerZone.DrawCircle(dangerzonePos, stateMachineOwnerClass.radius[a - 1], 1.5f); // 외곡 찍기
                break;
            case 4:
                DangerZone.DrawCircle(dangerzonePos, stateMachineOwnerClass.radius[a - 1], 1.5f);  //연속베기 (콤보1)
                break;
            case 5:
                DangerZone.DrawCircle(dangerzonePos, stateMachineOwnerClass.radius[a - 1], 1.5f);  //연속베기 (콤보2)
                break;
            case 6:
                DangerZone.DrawCircle(dangerzonePos, stateMachineOwnerClass.radius[a - 1], 1.5f); //원 범위 공격 ( 텔, 대쉬해서 공격)
                break;

        }
        //CoroutineHelper.StartCoroutine(SpawnDangerZome(a));
        while (Time.time < startTime + stateMachineOwnerClass.dashTime)
        {
            agent?.SetDestination(target);

            yield return null;
        }
        agent.isStopped = true;

        target = stateMachineOwnerClass.Target.position;
        Vector3 d = target - characterTrm.position;
        d.Normalize();
        d.y = 0;
        characterTrm.rotation = Quaternion.LookRotation(d);
        dangerzonePos = characterTrm.position;
        dangerzonePos.y = 0.1f;
        switch (a)
        {
            case 2:
                DangerZone.DrawArc(dangerzonePos, d, 5, new Vector3(2, 0.1f, 7), 2f); // 내려찍기
                break;

            case 3:
                DangerZone.DrawArc(dangerzonePos, d, 4, new Vector3(3.5f, 0.1f, 14), 2f); //회오리 참격베기
                break;
            case 7:
                DangerZone.DrawArc(dangerzonePos, d, 2, new Vector3(5, 0.1f, 12), 2f); //발도 기술
                break;
        }


        if (a == 2 || a == 3 || a == 7)
        {
            yield return new WaitForSeconds(0.5f);
        }

        stateMachineOwnerClass.ChangeAttack();
    }

}
