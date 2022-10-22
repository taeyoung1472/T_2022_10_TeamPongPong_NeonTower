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
        stateMachineOwnerClass.StartCoroutine(SwordDash());
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
                DangerZone.DrawCircle(dangerzonePos, stateMachineOwnerClass.radius[a - 1] *2, 1.5f); // 외곡 찍기
                break;
            case 4:
                DangerZone.DrawCircle(dangerzonePos, stateMachineOwnerClass.radius[a - 1] *2, 1.5f);  //연속베기 (콤보1)
                break;
            case 5:
                DangerZone.DrawCircle(dangerzonePos, stateMachineOwnerClass.radius[a - 1] * 2, 1.5f);  //연속베기 (콤보2)
                break;
            case 6:
                DangerZone.DrawCircle(dangerzonePos, stateMachineOwnerClass.radius[a - 1] * 2, 1.5f); //원 범위 공격 ( 텔, 대쉬해서 공격)
                break;

        }

        while (Time.time < startTime + stateMachineOwnerClass.dashTime)
        {
            agent?.SetDestination(target);

            yield return null;
        }
        agent.isStopped = true;

        target = stateMachineOwnerClass.Target.position;
        Vector3 d = target - characterTrm.position;
        stateMachineOwnerClass.attackDir = d;
        d.Normalize();
        d.y = 0;
        characterTrm.rotation = Quaternion.LookRotation(d);
        dangerzonePos = characterTrm.position;
        dangerzonePos.y = 0.1f;
        switch (a)
        {
            case 2:
                DangerZone.DrawArc(dangerzonePos, d, 5, new Vector3(2, 0.1f, 8), 1.25f); // 내려찍기
                stateMachineOwnerClass.arcangle = 15f;
                break;
            case 3:
                DangerZone.DrawArc(dangerzonePos, d, 4, new Vector3(3.5f, 0.1f, 14), 1.75f); //회오리 참격베기
                stateMachineOwnerClass.arcangle = 18f;
                break;
            case 7:
                DangerZone.DrawArc(dangerzonePos, d, 2, new Vector3(7, 0.1f, 10), 1.75f); //발도 기술
                stateMachineOwnerClass.arcangle = 40f;
                break;
        }
        

        if (a == 2 || a == 3 || a == 7)
        {
            yield return new WaitForSeconds(0.5f);
        }

        stateMachineOwnerClass.ChangeAttack();
    }

    

}
