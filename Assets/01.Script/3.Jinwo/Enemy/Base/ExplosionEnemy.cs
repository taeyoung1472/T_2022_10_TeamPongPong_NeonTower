using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEnemy : EnemyBase<ExplosionEnemy>
{
    public GameObject bombEffect;

    public Collider col;
    protected override void Awake()
    {
        base.Awake();

        col = GetComponent<Collider>();

        bombEffect.SetActive(false);

        fsmManager = new StateMachine<ExplosionEnemy>(this, new StateMove<ExplosionEnemy>());

        fsmManager.AddStateList(new StateExplosion<ExplosionEnemy>());

        //fsmManager.ReturnDic();
    }

    void Update()
    {
        fsmManager.Execute();
        //Debug.Log(fsmManager.getNowState.ToString());

        if(Dead && isAttack)
        {
            Attack();
        }

    }
    private void OnEnable()
    {
        bombEffect.SetActive(false);
        col.enabled = true;
    }
    public override void ChangeAttack()
    {
        //Debug.Log("�ڽ� ����");

        Die();
    }
    public override void EnableAttack()
    {
        base.EnableAttack();

        bombEffect.SetActive(true);
    }

    public override void DisableAttack()
    {
        base.DisableAttack();

        //���⼭ Ǯ�� �ٽ� Ǫ���ϸ� �ɵ�
        gameObject.SetActive(false);
    }

    public override void Die()
    {
        base.Die();

        col.enabled = false;

        fsmManager.ChangeState<StateExplosion<ExplosionEnemy>>();
    }
}
