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

        //fsmManager.ReturnDic();
    }

    void Update()
    {
        fsmManager.Execute();
        Debug.Log(fsmManager.getNowState.ToString());

    }
    public override void FixedUpdate()
    {
        if (Dead) return;

        var lookRotation =
                Quaternion.LookRotation((Target.transform.position - transform.position).normalized, Vector3.up);
        var targetAngleY = lookRotation.eulerAngles.y;

        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleY,
                                    ref turnSmoothVelocity, turnSmoothTime);
    }
    private void OnEnable()
    {
        isAttack = false;
        muzzle.SetActive(false);
    }
    public override void ChangeAttack()
    {
        //Debug.Log("자식 실행");

        fsmManager.ChangeState<StateCannonAttack<CannonEnemy>>();
    }

    public override void EnableAttack()
    {
        isAttack = true;
        muzzle.SetActive(true);
        targetPos = Target.transform;
        CannonBall missile = Instantiate(cannonBall, attackPos.position, Quaternion.identity);

        missile.GetComponent<Rigidbody>().velocity = Vector3.up * 15f;

        missile.SetTargetPos(targetPos, EnemyData.bulletSpeed);

        StartCoroutine(SpawnCircle());

    }
    public override void DisableAttack()
    {
        isAttack = false;
        muzzle.SetActive(false);
    }

    public IEnumerator SpawnCircle()
    {

        yield return new WaitForSeconds(0.7f);
        CircleController circle = Instantiate(cir, targetPos.position, Quaternion.Euler(new Vector3(-90, 0 , 0)) );
        
    }

}
