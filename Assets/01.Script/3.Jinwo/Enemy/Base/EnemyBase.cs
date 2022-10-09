using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour
{
    private float health;// ���� ü��
    private bool dead = false; // ��� ����
    public float Health { get => health; set => health = value; } // ���� ü��
    public bool Dead { get => dead; set => dead = value; } // ��� ����

    public UnityEvent OnDeath; // ����� �ߵ��� �̺�Ʈ

    [SerializeField]
    private EnemyDataSO enemyData;

    public EnemyDataSO EnemyData
    {
        get => enemyData;
        set => enemyData = value;
    }
    [SerializeField]
    private GameObject target;
    public GameObject Target
    {
        get => target;
        protected set => target = value;
    }

    public Transform attackRoot;

    [Range(0.01f, 2f)] public float turnSmoothTime = 0.1f;
    protected float turnSmoothVelocity;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (attackRoot != null)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            Gizmos.DrawSphere(attackRoot.position, EnemyData.attackRadius);
        }
        
    }
#endif

   
    public virtual void FixedUpdate()
    {
        if (dead) return;

        var lookRotation =
                Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
        var targetAngleY = lookRotation.eulerAngles.y;

        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleY,
                                    ref turnSmoothVelocity, turnSmoothTime);

    }
    // ü���� ȸ���ϴ� ���
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead) return;

        health += newHealth;
    }
    public virtual void Die()
    {
        // onDeath �̺�Ʈ�� ��ϵ� �޼��尡 �ִٸ� ����
        if (OnDeath != null) OnDeath?.Invoke();


        dead = true;
    }

}
