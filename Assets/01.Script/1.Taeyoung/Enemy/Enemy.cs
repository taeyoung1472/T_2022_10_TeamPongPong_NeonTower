using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.ComponentModel.Design;
using UnityEngine.AI;

public class Enemy : PoolAbleObject, IDamageable
{
    protected float health;// 현재 체력
    protected bool dead = false; // 사망 상태
    public float Health { get => health; set => health = value; } // 현재 체력
    public bool Dead { get => dead; set => dead = value; } // 사망 상태

    public UnityEvent OnDeath; // 사망시 발동할 이벤트



    [SerializeField]
    protected EnemyDataSO enemyData;

    public EnemyDataSO EnemyData
    {
        get => enemyData;
        set => enemyData = value;
    }

    protected GameObject target;
    public GameObject Target
    {
        get => target;
        set => target = value;
    }

    protected LayerMask whatIsTarget; // 추적 대상 레이어

    protected Transform attackRoot;

    protected bool isAttack = false;
    public bool IsAttack { get { return isAttack; } set { isAttack = value; } }

    protected const float minTimeBetDamaged = 0.1f;
    protected float lastDamagedTime;

    // 잠깐 무적 시간
    protected bool IsInvulnerable
    {
        get
        {
            if (Time.time >= lastDamagedTime + minTimeBetDamaged) return false;

            return true;
        }
    }

    public virtual void Die()
    {
        dead = true;

        // onDeath 이벤트에 등록된 메서드가 있다면 실행
        if (OnDeath != null) OnDeath?.Invoke();
    }

    public void Init(Vector3 initPos, GameObject target)
    {
        GetComponent<NavMeshAgent>().enabled = false;
        transform.position = initPos;
        Target = target.gameObject;
        GetComponent<NavMeshAgent>().enabled = true;
    }

    #region PoolAble
    public override void Init_Pop()
    {
        health = enemyData.maxHealth;
        dead = false;
    }

    public override void Init_Push()
    {

    }

    public void ApplyDamage(int dmg)
    {
        health -= dmg;
        DamagePopup.PopupDamage(transform.position, dmg);
        if(health <= 0)
        {
            Die();
        }
    }
    #endregion
}
