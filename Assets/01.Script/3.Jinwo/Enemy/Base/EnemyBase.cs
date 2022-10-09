using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase<T> : MonoBehaviour
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

    public LayerMask whatIsTarget; // ���� ��� ���̾�


    public Transform attackRoot;

    public bool isAttack = false;


    [Range(0.01f, 2f)] public float turnSmoothTime = 0.1f;
    protected float turnSmoothVelocity;


    protected StateMachine<T> fsmManager;
    public StateMachine<T> FsmManager => fsmManager;


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (attackRoot != null)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            Gizmos.DrawSphere(attackRoot.position, EnemyData.attackRadius);
        }

        if(enemyData.dashSpeed != 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * enemyData.dashDistance);
            //Gizmos.DrawLine(transform.forward, new Vector3(transform.position.x, transform.position.y  , transform.position.z + enemyData.dashDistance));
        }
        
    }
#endif

    protected virtual void Awake()
    {
        health = EnemyData.maxHealth;
        isAttack = false;
    }
    public virtual void FixedUpdate()
    {
        if (dead) return;

        var lookRotation =
                Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
        var targetAngleY = lookRotation.eulerAngles.y;

        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleY,
                                    ref turnSmoothVelocity, turnSmoothTime);

        if(isAttack)
        {
            Attack();
        }
    }
    public virtual void ChangeAttack()
    {
        //Debug.Log("�θ� ����");
    }
    public virtual void Attack()
    {
        Collider[] cols = Physics.OverlapSphere(attackRoot.position, enemyData.attackRadius, whatIsTarget);

        if (cols.Length > 0)
        {
            Debug.Log("���� ����!!");
            isAttack = false;
        }
    }

    public virtual void EnableAttack()
    {
        Debug.Log("���� ����");
        isAttack = true;
    }

    public virtual void DisableAttack()
    {
        Debug.Log("���� ��");
        isAttack = false;
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
