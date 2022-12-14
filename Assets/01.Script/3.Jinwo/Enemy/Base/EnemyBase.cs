using System.Collections;
using UnityEngine;

public class EnemyBase<T> : Enemy
{
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

        if (enemyData.dashSpeed != 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * enemyData.dashDistance);
            //Gizmos.DrawLine(transform.forward, new Vector3(transform.position.x, transform.position.y  , transform.position.z + enemyData.dashDistance));
        }

    }
#endif
    
    protected virtual void Awake()
    {
        isAttack = false;
        whatIsTarget |= 1 << LayerMask.NameToLayer("Player");
        attackRoot = transform.Find($"{enemyData.enemyName}/AttackRoot");
        OnDeath.AddListener(() => { PoolManager.Instance.Push(PoolType, gameObject); });
    }
    public virtual void FixedUpdate()
    {
        if (dead) return;

        var lookRotation =
                Quaternion.LookRotation((target.transform.position - transform.position).normalized, Vector3.up);
        var targetAngleY = lookRotation.eulerAngles.y;

        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleY,
                                    ref EnemyData.turnSmoothVelocity, EnemyData.turnSmoothTime);

        if (isAttack)
        {
            Attack();
        }
    }
    public virtual void ChangeAttack()
    {
        //Debug.Log("?θ? ????");
    }
    public virtual void Attack()
    {
        Collider[] cols = Physics.OverlapSphere(attackRoot.position, enemyData.attackRadius, whatIsTarget);

        foreach (var col in cols)
        {
            IDamageable damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.ApplyDamage(1);
                isAttack = false;
                break;
            }
        }
    }

    public virtual void EnableAttack()
    {
        isAttack = true;
    }

    public virtual void DisableAttack()
    {
        isAttack = false;
    }
    // ü???? ȸ???ϴ? ????
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead) return;

        health += newHealth;
    }
    public virtual void StartMotionTrail()
    {

    }
    public virtual void StopMotionTrail()
    {

    }

    public override void Init_Pop()
    {
        base.Init_Pop();
        StartCoroutine(ExcuteCor());
    }

    private IEnumerator ExcuteCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.15f);
            fsmManager.Execute();
        }
    }
}
