using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.ComponentModel.Design;
using UnityEngine.AI;

public class Enemy : PoolAbleObject, IDamageable
{
    protected float health;// ���� ü��
    protected bool dead = false; // ��� ����
    public float Health { get => health; set => health = value; } // ���� ü��
    public bool Dead { get => dead; set => dead = value; } // ��� ����

    public UnityEvent OnDeath; // ����� �ߵ��� �̺�Ʈ



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

    protected LayerMask whatIsTarget; // ���� ��� ���̾�

    protected Transform attackRoot;

    protected bool isAttack = false;
    public bool IsAttack { get { return isAttack; } set { isAttack = value; } }

    protected const float minTimeBetDamaged = 0.1f;
    protected float lastDamagedTime;

    // ��� ���� �ð�
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

        // onDeath �̺�Ʈ�� ��ϵ� �޼��尡 �ִٸ� ����
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
