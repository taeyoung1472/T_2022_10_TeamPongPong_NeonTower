using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEnemy : EnemyBase<ExplosionEnemy>
{
    protected override void Awake()
    {
        base.Awake();

        fsmManager = new StateMachine<ExplosionEnemy>(this, new StateMove<ExplosionEnemy>());

        fsmManager.AddStateList(new StateExplosion<ExplosionEnemy>());
        fsmManager.AddStateList(new StateKnockback<ExplosionEnemy>());
    }

    void Update()
    {
        fsmManager.Execute();

        if(Dead && isAttack)
        {
            Attack();
        }

    }
    public override void ChangeAttack()
    {
        //Debug.Log("자식 실행");

        fsmManager.ChangeState<StateExplosion<ExplosionEnemy>>();
        //Die();
    }
    public override void EnableAttack()//터지기
    {
        base.EnableAttack();
    }

    public override void DisableAttack()//오브제긑 끄기
    {
        base.DisableAttack();
        Die();
    }

    public override void ApplyDamage(float dmg)
    {
        health -= dmg;
        AudioManager.PlayAudioRandPitch(enemyData.hitClip);
        DamagePopup.PopupDamage(transform.position, dmg);
        if ((int)UpgradeManager.Instance.GetUpgradeValue(UpgradeType.BulletKnockback) != 0)
        {
            fsmManager.ChangeState<StateKnockback<ExplosionEnemy>>();
        }
        if (health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        GameObject effect = PoolManager.Instance.Pop(PoolType.EnemyExplosionEffect).gameObject;
        effect.transform.position = transform.position;

        base.Die();
    }
}
