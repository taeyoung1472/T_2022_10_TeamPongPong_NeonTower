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


    [Header("ī�޶� ����")]
    public float amplitude = 0;
    public float intensity = 0;
    public float duration = 0;

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        //Gizmos.DrawSphere(transform.position, data.attackRange);

        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, arcangle / 2, arcwidth);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, - arcangle / 2, arcwidth);
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
        animeEvent.damageEvent = StartApplyDamage;
    }
    protected override void Update()
    {
        base.Update();
        
        Debug.Log(bossFsm.GetNowState);

    }
    protected void FixedUpdate()
    {
        if(isApplyDamage && isAttacking)
        {
            if (currentAttackType == 1 || currentAttackType == 2 || currentAttackType == 6)
                AttackDamageArc();
            else
                AttakDamageCircle();
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
                bossFsm.ChangeState<SwordDistortionSlash<Sword>>(); // �ܰ� ���
                amplitude = 25f;
                intensity = 50f;
                duration = 0.5f;
                break;
            case 2:
                bossFsm.ChangeState<SwordTakeDownAttack<Sword>>(); // �������
                amplitude = 30f;
                intensity = 40f;
                duration = 0.25f;
                break;
            case 3:
                bossFsm.ChangeState<SwordSpinningAttack<Sword>>(); //ȸ���� ���ݺ���
                amplitude = 30f;
                intensity = 30f;
                duration = 0.35f;
                break;
            case 4:
                bossFsm.ChangeState<SwordComboAttack1<Sword>>();  //���Ӻ��� (�޺�1)
                amplitude = 0;
                intensity = 0;
                duration = 0;
                break;
            case 5:
                bossFsm.ChangeState<SwordComboAttack2<Sword>>();  //���Ӻ��� (�޺�2)
                amplitude = 0;
                intensity = 0;
                duration = 0;
                break;
            case 6:
                bossFsm.ChangeState<SwordCircleRangeAttack<Sword>>();  //�� ���� ���� ( ��, �뽬�ؼ� ����)
                amplitude = 25f;
                intensity = 30f;
                duration = 0.2f;
                break;
            case 7:
                bossFsm.ChangeState<SwordBaldoAttack<Sword>>();  //�ߵ� ���
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
            Debug.Log("��Ŭ ���� ó����");
            var attackTargetEntity = col[0].GetComponent<IDamageable>();

            if(!lastAttackedTargets.Contains(attackTargetEntity))
            {
                lastAttackedTargets.Add(attackTargetEntity);

                attackTargetEntity.ApplyDamage((int)data.damage);
            }
            
        }
    }
    public void AttackDamageArc()
    {

        // target�� �� ������ �Ÿ��� radius ���� �۴ٸ�
        if (attackDir.magnitude <= radius[currentAttackType])
        {
            // 'Ÿ��-�� ����'�� '�� ���� ����'�� ����
            float dot = Vector3.Dot(attackDir.normalized, transform.forward);
            // �� ���� ��� ���� �����̹Ƿ� ���� ����� cos�� ���� ���ؼ� theta�� ����
            float theta = Mathf.Acos(dot);
            // angleRange�� ���ϱ� ���� degree�� ��ȯ
            float degree = Mathf.Rad2Deg * theta;

            // �þ߰� �Ǻ�
            if (degree <= arcangle / 2f)
            {
                Debug.Log("�þ߰��� �ְ� ó��������");
                Collider[] col = Physics.OverlapSphere(transform.position, radius[currentAttackType], playerLayer);

                if (col.Length > 0)
                {
                    var attackTargetEntity = col[0].GetComponent<IDamageable>();
                    if (!lastAttackedTargets.Contains(attackTargetEntity))
                    {
                        lastAttackedTargets.Add(attackTargetEntity);
                        attackTargetEntity.ApplyDamage((int)data.damage);
                    }
                }
            }
            else
            {
                Debug.Log("�� ó����");
            }
        }
        else
        {
            Debug.Log("���ʿ� �Ÿ��� �ȵ���");
        }
    }
    public void EnableEffect()
    {
        motionTrail.isMotionTrail = false;
        attackEffect[currentAttackType].gameObject.SetActive(true);
        attackEffect[currentAttackType].Play();
        isAttacking = true;
        //lastAttackedTargets.Clear();
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

}
