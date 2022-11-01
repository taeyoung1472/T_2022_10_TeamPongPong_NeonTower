using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonEnemy : EnemyBase<CannonEnemy>
{
    public GameObject muzzle;

    public CannonBall cannonBall;

    public Transform attackPos = null;

    public CircleController cir;

    public Transform targetPos;
    
    protected override void Awake()
    {
        base.Awake();

        cannonBall = EnemyData.bulletPrefab;

        muzzle.SetActive(false);

        fsmManager = new StateMachine<CannonEnemy>(this, new StateMove<CannonEnemy>());

        fsmManager.AddStateList(new StateCannonAttack<CannonEnemy>());

        fsmManager.AddStateList(new StateKnockback<CannonEnemy>());

        //fsmManager.ReturnDic();
    }

    public override void FixedUpdate()
    {
        if (Dead) return;

        var lookRotation =
                Quaternion.LookRotation((Target.transform.position - transform.position).normalized, Vector3.up);
        var targetAngleY = lookRotation.eulerAngles.y;

        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleY,
                                    ref EnemyData.turnSmoothVelocity, EnemyData.turnSmoothTime);
    }
    private void OnEnable()
    {
        isAttack = false;
        muzzle.SetActive(false);
    }
    public override void ChangeAttack()
    {
        fsmManager.ChangeState<StateCannonAttack<CannonEnemy>>();
    }

    public override void EnableAttack()
    {
        isAttack = true;
        muzzle.SetActive(true);
        targetPos = Target.transform;
        CannonBall missile = Instantiate(cannonBall, attackPos.position, Quaternion.identity);

        missile.GetComponent<Rigidbody>().velocity = Vector3.up * 15f;

        missile.SetTargetPos(targetPos.position, EnemyData.bulletSpeed);

        
        StartCoroutine(SpawnCircle(targetPos.position));

    }
    public override void DisableAttack()
    {
        isAttack = false;
        muzzle.SetActive(false);
    }

    public IEnumerator SpawnCircle(Vector3 spawnPos)
    {

        yield return new WaitForSeconds(0.5f);
        DangerZone.DrawCircle(spawnPos, 5f, 2.6f);
        //CircleController circle = Instantiate(cir, spawnPos, Quaternion.Euler(new Vector3(-90, 0 , 0)) );

        //circle.GetComponent<MeshRenderer>().material.color = Color.red;
        //Destroy(circle, 5f);
    }

    public override void ApplyDamage(float dmg)
    {
        base.ApplyDamage(dmg);
        if((int)UpgradeManager.Instance.GetUpgradeValue(UpgradeType.BulletKnockback) != 0)
        {
            fsmManager.ChangeState<StateKnockback<CannonEnemy>>();
        }
    }
}
