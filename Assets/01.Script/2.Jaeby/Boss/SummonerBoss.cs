using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerBoss : BossBase<SummonerBoss>
{
    private Animator _animator = null;
    private readonly string _startAnim = "StartAnimation";
    private void Start()
    {
        _animator = GetComponent<Animator>();
        CurHp = Data.maxHp;

        StartCoroutine(StartAnimationCoroutine());
    }

    private IEnumerator StartAnimationCoroutine()
    {
        _animator.Play(_startAnim);
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName(_startAnim) == false);

        bossFsm = new BossStateMachine<SummonerBoss>(this, new SummonerIdle());
        bossFsm.AddStateList(new SummonerWalk());
    }

    public override void ApplyDamage(int dmg)
    {
        DamagePopup.PopupDamage(transform.position + Vector3.up * 1.3f, dmg);
        CurHp -= (float)dmg;
        BossUIManager.BossDamaged();
        if (CurHp <= 0)
        {
            Debug.Log("»ç¸Á !!");
            OnDeathEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
