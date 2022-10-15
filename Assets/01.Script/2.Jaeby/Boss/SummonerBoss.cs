using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerBoss : BossBase<SummonerBoss>
{
    private void Start()
    {
        bossFsm = new BossStateMachine<SummonerBoss>(this, new SummonerIdle());
        bossFsm.AddStateList(new SummonerWalk());

        CurHp = Data.maxHp;
    }

    public void Instan(GameObject obj)
    {
        Instantiate(obj);
    }

    public override void ApplyDamage(int dmg)
    {
        DamagePopup.PopupDamage(transform.position + Vector3.up * 1.3f, dmg);
        CurHp -= (float)dmg;
        BossUIManager.BossDamaged();
        if(CurHp <= 0)
        {
            Debug.Log("»ç¸Á !!");
            OnDeathEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
