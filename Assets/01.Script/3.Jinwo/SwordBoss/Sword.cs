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

        bossFsm = new BossStateMachine<Sword>(this, new SwordMove<Sword>());

        //bossFsm.AddStateList(new SwordMove<Sword>());

        bossFsm.AddStateList(new SwordSpinningAttack<Sword>());
        bossFsm.AddStateList(new SwordTakeDownAttack<Sword>());
        bossFsm.AddStateList(new SwordComboAttack1<Sword>());
        bossFsm.AddStateList(new SwordDistortionSlash<Sword>());
        bossFsm.AddStateList(new SwordComboAttack2<Sword>());
        bossFsm.AddStateList(new SwordCircleRangeAttack<Sword>());
        bossFsm.AddStateList(new SwordBaldoAttack<Sword>());

        attackCoolTime = data.patternCoolTime;
        currentAttackType = 0;
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
    public IEnumerator SwordDash()
    {
        float startTime = Time.time;
        Vector3 dir = target.position - transform.position;

        while (Time.time < startTime + dashTime)
        {
            transform.Translate(dir.normalized * dashspeed * Time.deltaTime);

            yield return null;
        }
    }
    public void ChangeAttack()
    {
        int attackType = Random.Range(1, 8);
        currentAttackType = attackType - 1;

        switch (attackType)
        {
            case 1:
                
                //StartCoroutine(SwordDash());
                bossFsm.ChangeState<SwordDistortionSlash<Sword>>(); // �ܰ� ���
                
                break;
            case 2:
               // StartCoroutine(SwordDash());
                bossFsm.ChangeState<SwordTakeDownAttack<Sword>>(); // �������
                break;
            case 3:
                bossFsm.ChangeState<SwordSpinningAttack<Sword>>(); //ȸ���� ���ݺ���
                break;
            case 4:
                bossFsm.ChangeState<SwordComboAttack1<Sword>>();  //���Ӻ��� (�޺�1)
                break;
            case 5:
                bossFsm.ChangeState<SwordComboAttack2<Sword>>();  //���Ӻ��� (�޺�2)
                break;
            case 6:
                //StartCoroutine(SwordDash());
                bossFsm.ChangeState<SwordCircleRangeAttack<Sword>>();  //�� ���� ���� ( ��, �뽬�ؼ� ����)
                break;
            case 7:
               // StartCoroutine(SwordDash());
                bossFsm.ChangeState<SwordBaldoAttack<Sword>>();  //�ߵ� ���
                break;
        }
        animator.SetTrigger("Attack");
    }

    public void EnableEffect()
    {
        motionTrail.isMotionTrail = false;
        attackEffect[currentAttackType].gameObject.SetActive(true);
        attackEffect[currentAttackType].Play();
        isAttacking = true;
    }

    public void DisableEffect()
    {
        motionTrail.isMotionTrail = true;
        attackEffect[currentAttackType].gameObject.SetActive(false);
        isAttacking = false;
    }

}
