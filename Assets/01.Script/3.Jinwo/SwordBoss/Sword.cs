using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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


    public bool isApplyDamage = false;
    public Vector3 attackDir = Vector3.zero;

    public float arcDistance = 0;

    public float arcwidth = 0;

    public float arcangle = 0;


    private Collider _col = null;
    public Collider Col => _col;

    public Material myMat;

    public GameObject dangerZone = null;


    [Header("카메라 관련")]
    public float amplitude = 0;
    public float intensity = 0;
    public float duration = 0;

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, arcangle / 2, arcwidth);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, - arcangle / 2, arcwidth);
    }
#endif
    protected override void Awake()
    {
        base.Awake();

        myMat = transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Renderer>().material;

        myMat.SetFloat("_Singularity", 1);
        _col = GetComponent<Collider>();

        bossFsm = new BossStateMachine<Sword>(this, new SwordEnterState<Sword>());

        bossFsm.AddStateList(new SwordIdle<Sword>());
        bossFsm.AddStateList(new SwordMove<Sword>());

        bossFsm.AddStateList(new SwordSpinningAttack<Sword>());
        bossFsm.AddStateList(new SwordTakeDownAttack<Sword>());
        bossFsm.AddStateList(new SwordComboAttack1<Sword>());
        bossFsm.AddStateList(new SwordDistortionSlash<Sword>());
        bossFsm.AddStateList(new SwordComboAttack2<Sword>());
        bossFsm.AddStateList(new SwordCircleRangeAttack<Sword>());
        bossFsm.AddStateList(new SwordBaldoAttack<Sword>());

        bossFsm.AddStateList(new SwordDie<Sword>());
        currentAttackType = 0;

       // motionTrail._data.
    }

    private void Start()
    {
        isAttacking = false;

        CurHp = Data.maxHp;

        IsDead = false;
        //motionTrail.isMotionTrail = true;
        animeEvent.startAnime = EnableEffect;
        animeEvent.endAnime = DisableEffect;
        animeEvent.damageEvent = StartApplyDamage;
        animeEvent.dieEvent = DieEvent;

    }
    protected override void Update()
    {
        base.Update();
        
        Debug.Log(bossFsm.GetNowState);

        if (isApplyDamage && isAttacking)
        {
            if (currentAttackType == 1 || currentAttackType == 2 || currentAttackType == 6)
                AttackDamageArc();
            else
                AttakDamageCircle();
        }

    }
    protected void FixedUpdate()
    {
        
    }
    public override void ApplyDamage(float dmg)
    {

        DamagePopup.PopupDamage(transform.position + Vector3.up, dmg);
        CurHp -= dmg;
        BossUIManager.BossDamaged();
        if (CurHp <= 0)
        {
            Debug.Log("사망 !!");
            if (dangerZone != null)
            {
                MonoHelper._Destroy(dangerZone, 0.1f);
            }

            Glitch.GlitchManager.Instance.GrayValue();
            motionTrail.enabled = false;
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            StopAllCoroutines();
            OnDeathEvent?.Invoke();
            bossFsm.ChangeState<SwordDie<Sword>>();
        }
    }
    
    public override void Die()
    {
        //OnDeathEvent?.Invoke();
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
                amplitude = 25f;
                intensity = 50f;
                duration = 0.5f;
                break;
            case 2:
                bossFsm.ChangeState<SwordTakeDownAttack<Sword>>(); // 내려찍기
                amplitude = 30f;
                intensity = 40f;
                duration = 0.25f;
                break;
            case 3:
                bossFsm.ChangeState<SwordSpinningAttack<Sword>>(); //회오리 참격베기
                amplitude = 30f;
                intensity = 30f;
                duration = 0.35f;
                break;
            case 4:
                bossFsm.ChangeState<SwordComboAttack1<Sword>>();  //연속베기 (콤보1)
                amplitude = 0;
                intensity = 0;
                duration = 0;
                break;
            case 5:
                bossFsm.ChangeState<SwordComboAttack2<Sword>>();  //연속베기 (콤보2)
                amplitude = 0;
                intensity = 0;
                duration = 0;
                break;
            case 6:
                bossFsm.ChangeState<SwordCircleRangeAttack<Sword>>();  //원 범위 공격 ( 텔, 대쉬해서 공격)
                amplitude = 25f;
                intensity = 30f;
                duration = 0.2f;
                break;
            case 7:
                bossFsm.ChangeState<SwordBaldoAttack<Sword>>();  //발도 기술
                amplitude = 25f;
                intensity = 30f;
                duration = 0.45f;
                break;
        }
        animator.SetTrigger("Attack");
    }
    
    public void AttakDamageCircle()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, radius[currentAttackType], playerLayer);

        if (col.Length > 0 )
        {
            Debug.Log("서클 범위 처맞음");
            var attackTargetEntity = col[0].GetComponent<IDamageable>();
            Glitch.GlitchManager.Instance.HitValue();
            if(!lastAttackedTargets.Contains(attackTargetEntity))
            {
                lastAttackedTargets.Add(attackTargetEntity);

                attackTargetEntity.ApplyDamage((int)data.damage);
            }
            
        }
    }
    public void AttackDamageArc()
    {
        attackDir = target.position - transform.position;
        attackDir.y = 0;
        // target과 나 사이의 거리가 radius 보다 작다면
        if (attackDir.magnitude <= radius[currentAttackType])
        {
            // '타겟-나 벡터'와 '내 정면 벡터'를 내적
            float dot = Vector3.Dot(transform.forward, attackDir.normalized);
            // 두 벡터 모두 단위 벡터이므로 내적 결과에 cos의 역을 취해서 theta를 구함
            float theta = Mathf.Acos(dot);
            // angleRange와 비교하기 위해 degree로 변환
            float degree = Mathf.Rad2Deg * theta;

            Debug.Log("거리안에 들어왔고 degree: "+ degree);

            // 시야각 판별
            if (degree <= arcangle / 2f)
            {
                Debug.Log("시야각에 있고 처맞은거임");
                Collider[] cols = Physics.OverlapSphere(transform.position, 14f, playerLayer);

                foreach (var col in cols)
                {
                    IDamageable attackTargetEntity = col.GetComponent<IDamageable>();
                    Glitch.GlitchManager.Instance.HitValue();
                    if (!lastAttackedTargets.Contains(attackTargetEntity))
                    {
                        lastAttackedTargets.Add(attackTargetEntity);
                        attackTargetEntity.ApplyDamage((int)data.damage);
                    }
                }
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
        EndApplyDamage();
        motionTrail.isMotionTrail = true;
        attackEffect[currentAttackType].gameObject.SetActive(false);
        isAttacking = false;
    }

    public void StartApplyDamage()
    {
        CameraManager.Instance.CameraShake(amplitude, intensity, duration);
        isApplyDamage = true;
    }
    public void EndApplyDamage()
    {
        lastAttackedTargets.Clear();
        isApplyDamage = false;
    }
    public void DieEvent()
    {
        Glitch.GlitchManager.Instance.ZeroValue();
    }
}
