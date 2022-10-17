using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BossBase<Sword>
{
    public float attackCoolTime;

    public ParticleSystem[] attackEffect;

    public int currentAttackType = 0;

    public MeshAfterImage motionTrail;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, data.attackRange);
    }
#endif
    private void Awake()
    {

        bossFsm = new BossStateMachine<Sword>(this, new SwordIdle<Sword>());

        bossFsm.AddStateList(new SwordMove<Sword>());

        bossFsm.AddStateList(new SwordSpinningAttack<Sword>());
        bossFsm.AddStateList(new SwordTakeDownAttack<Sword>());
        bossFsm.AddStateList(new SwordContinuousSlash<Sword>());
        bossFsm.AddStateList(new SwordDistortionSlash<Sword>());
        attackCoolTime = data.patternCoolTime;
        currentAttackType = 0;
    }
    private void Start()
    {
        //motionTrail.isMotionTrail = true;
    }
    protected override void Update()
    {
        base.Update();

    }
    public void ChangeAttack()
    {
        int attackType = Random.Range(1, 5);
        currentAttackType = attackType - 1;
        switch (attackType)
        {
            case 1:
                bossFsm.ChangeState<SwordDistortionSlash<Sword>>(); // 외곡 찍기
                
                break;
            case 2:
                bossFsm.ChangeState<SwordTakeDownAttack<Sword>>(); // 내려찍기
                break;
            case 3:
                bossFsm.ChangeState<SwordContinuousSlash<Sword>>();  //연속베기
                break;
            case 4:
                bossFsm.ChangeState<SwordSpinningAttack<Sword>>(); //회오리 참격베기
                break;
        }
    }

    public void EnableEffect()
    {
        attackEffect[currentAttackType].gameObject.SetActive(true);
        attackEffect[currentAttackType].Play();
    }

    public void DisableEffect()
    {
        attackEffect[currentAttackType].gameObject.SetActive(false);
    }

}
