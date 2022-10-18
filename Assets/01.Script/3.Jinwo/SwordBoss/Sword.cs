using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BossBase<Sword>
{
    public float attackCoolTime;

    public ParticleSystem[] attackEffect;

    public int currentAttackType = 0;

    public MeshAfterImage motionTrail;
    public SwordAnimeEvent animeEvent;

    public bool isAttacking = false;

    public float dashspeed = 10f;
    public float dashTime = 0;

    public float[] radius;

    public LayerMask playerLayer;

    public List<IDamageable> lastAttackedTargets = new List<IDamageable>();




#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, data.attackRange);
    }
#endif
    protected override void Awake()
    {
        base.Awake();

        bossFsm = new BossStateMachine<Sword>(this, new SwordIdle<Sword>());

        bossFsm.AddStateList(new SwordMove<Sword>());

        bossFsm.AddStateList(new SwordSpinningAttack<Sword>());
        bossFsm.AddStateList(new SwordTakeDownAttack<Sword>());
        bossFsm.AddStateList(new SwordComboAttack1<Sword>());
        bossFsm.AddStateList(new SwordDistortionSlash<Sword>());
        bossFsm.AddStateList(new SwordComboAttack2<Sword>());
        bossFsm.AddStateList(new SwordCircleRangeAttack<Sword>());
        bossFsm.AddStateList(new SwordBaldoAttack<Sword>());

        attackCoolTime = data.patternCoolTime;
        currentAttackType = 0;

       // motionTrail._data.
    }
    private void Start()
    {
        isAttacking = false;
        //motionTrail.isMotionTrail = true;
        animeEvent.startAnime = EnableEffect;
        animeEvent.endAnime = DisableEffect;
    }
    protected override void Update()
    {
        base.Update();
        
        Debug.Log(bossFsm.GetNowState);

    }
    protected void FixedUpdate()
    {
        if(isAttacking)
        {
            AttackDamageArc();
        }
    }
    public int SelectAttackType()
    {
        int attackType = Random.Range(1, 8);
        currentAttackType = attackType - 1;
        

        return attackType;
    }
    public void ChangeAttack()
    {
       

        switch (currentAttackType+1)
        {
            case 1:
                bossFsm.ChangeState<SwordDistortionSlash<Sword>>(); // 외곡 찍기
                break;
            case 2:
                bossFsm.ChangeState<SwordTakeDownAttack<Sword>>(); // 내려찍기
                break;
            case 3:
                bossFsm.ChangeState<SwordSpinningAttack<Sword>>(); //회오리 참격베기
                break;
            case 4:
                bossFsm.ChangeState<SwordComboAttack1<Sword>>();  //연속베기 (콤보1)
                break;
            case 5:
                bossFsm.ChangeState<SwordComboAttack2<Sword>>();  //연속베기 (콤보2)
                break;
            case 6:
                bossFsm.ChangeState<SwordCircleRangeAttack<Sword>>();  //원 범위 공격 ( 텔, 대쉬해서 공격)
                break;
            case 7:
                bossFsm.ChangeState<SwordBaldoAttack<Sword>>();  //발도 기술
                break;
        }
        animator.SetTrigger("Attack");
    }
    
    public void AttakDamageCircle()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, radius[currentAttackType], playerLayer);
    }
    public void AttackDamageArc()
    {
        Vector3 dir = target.position - transform.position;
        dir.Normalize();
        dir.y = 0;
        RaycastHit[] hits = null;
        var col = Physics.SphereCastNonAlloc(transform.position, radius[currentAttackType], dir, hits, radius[currentAttackType], playerLayer);


        for (var i = 0; i < col; i++)
        {
            var attackTargetEntity = hits[i].collider.GetComponent<IDamageable>();

            if (attackTargetEntity != null && !lastAttackedTargets.Contains(attackTargetEntity))
            {
                // 공격이 들어간 지점
                //if (hits[i].distance <= 0f)
                //{
                //    message.hitPoint = attackRoot.position;
                //}
                //else
                //{
                //    message.hitPoint = hits[i].point;
                //}

                // 공격이 들어가는 방향
                //message.hitNormal = hits[i].normal;

                attackTargetEntity.ApplyDamage((int)data.damage);

                // 이미 공격을 가한 상대방이라는 뜻에서
                lastAttackedTargets.Add(attackTargetEntity);

                Debug.Log("일단 실행은 함");

                break;  // 공격 대상을 찾았으니 for문 종료
            }
        }
    }
    public void EnableEffect()
    {
        motionTrail.isMotionTrail = false;
        attackEffect[currentAttackType].gameObject.SetActive(true);
        attackEffect[currentAttackType].Play();
        isAttacking = true;
        lastAttackedTargets.Clear();
    }

    public void DisableEffect()
    {
        motionTrail.isMotionTrail = true;
        attackEffect[currentAttackType].gameObject.SetActive(false);
        isAttacking = false;
        
    }

}
