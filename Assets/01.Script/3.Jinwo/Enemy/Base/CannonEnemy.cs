using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonEnemy : EnemyBase<CannonEnemy>
{
    public GameObject muzzle;

    protected override void Awake()
    {
        base.Awake();

        muzzle.SetActive(false);

        fsmManager = new StateMachine<CannonEnemy>(this, new StateMove<CannonEnemy>());

        fsmManager.AddStateList(new StateCannonAttack<CannonEnemy>());

        //fsmManager.ReturnDic();
    }

    void Update()
    {
        fsmManager.Execute();
        //Debug.Log(fsmManager.getNowState.ToString());

    }
    private void OnEnable()
    {
        muzzle.SetActive(false);
    }
    public override void ChangeAttack()
    {
        //Debug.Log("자식 실행");

        fsmManager.ChangeState<StateCannonAttack<CannonEnemy>>();
    }


}
