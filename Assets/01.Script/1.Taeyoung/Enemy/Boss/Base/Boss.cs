using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Boss : MonoBehaviour, IDamageable
{
    #region 변수
    [Header("상태변수")]
    protected float curHp;
    protected bool isDead;
    public float CurHp { get { return curHp; } set { curHp = value; } }
    public bool IsDead { get { return isDead; } set { isDead = value; } }

    [Header("[Ref]")]
    [SerializeField] protected BossDataSO data;
    public BossDataSO Data { get { return data; } }

    protected Transform target;
    public Transform Target { get { if (target == null) { target = FindObjectOfType<PlayerController>().transform; } return target; } }

    protected NavMeshAgent agent;
    public NavMeshAgent Agent { get { return agent; } }

    protected Animator animator;
    public Animator Animator { get { return animator; } }

    [Header("[Event]")]
    public UnityEvent OnDeathEvent;
    #endregion

    public virtual void ApplyDamage(float dmg)
    {
        DamagePopup.PopupDamage(transform.position + Vector3.up * 2, dmg);
        Debug.Log("나 아야 했어");

        CurHp -= dmg;
        if(CurHp < 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {

    }
}
