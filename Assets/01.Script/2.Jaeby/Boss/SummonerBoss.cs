using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerBoss : BossBase<SummonerBoss>
{


    private void Start()
    {
        //bossFsm = new BossStateMachine<SummonerBoss>(this, new SummonerIdle());
        //BossFsm.AddStateList(new SummonerWalk());


    }

    public void Instan(GameObject obj)
    {
        Instantiate(obj);
    }
}

/*public class SummonerIdle : BossState<SummonerBoss>
{
    public override void Enter()
    {
        Debug.Log("���̵�");
        stateMachineOwnerClass.StartCoroutine(DDD());
    }

    private IEnumerator DDD()
    {
        yield return new WaitForSeconds(2f);
        stateMachineOwnerClass.Instan(new GameObject());
        stateMachine.ChangeState<SummonerWalk>();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}

public class SummonerWalk : BossState<SummonerBoss>
{
    public override void Enter()
    {
        Debug.Log("��");
        stateMachineOwnerClass.StartCoroutine(DDD());
    }

    private IEnumerator DDD()
    {
        yield return new WaitForSeconds(2f);
        stateMachine.ChangeState<SummonerIdle>();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
*/