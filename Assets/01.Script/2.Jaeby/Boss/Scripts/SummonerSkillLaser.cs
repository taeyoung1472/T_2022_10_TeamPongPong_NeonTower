using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerSkillLaser : BossState<SummonerBoss>
{
    public override void Enter()
    {
        stateMachineOwnerClass.ModelReset();

        stateMachineOwnerClass.Agent.ResetPath();
        stateMachineOwnerClass.Agent.isStopped = true;
        stateMachineOwnerClass.Agent.updatePosition = false;

        stateMachineOwnerClass.StartCoroutine(DangerAndLaser());
    }

    private IEnumerator DangerAndLaser()
    {
        DangerZone.DrawArc(stateMachineOwnerClass.transform.position + stateMachineOwnerClass.transform.forward * 0.2f, stateMachineOwnerClass.transform.forward, 130f, Vector3.one * 5f, 0.5f);
        yield return new WaitForSeconds(stateMachineOwnerClass.AttackDataSO.laserAttackDangerInterval);
        stateMachineOwnerClass.LaserModel.SetActive(true);
        stateMachineOwnerClass.Animator.Play("Laser");
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
        stateMachineOwnerClass.ModelReset();
        stateMachineOwnerClass.LaserModel.SetActive(false);

        stateMachineOwnerClass.Agent.updatePosition = true;
        stateMachineOwnerClass.Agent.isStopped = false;
    }
}
