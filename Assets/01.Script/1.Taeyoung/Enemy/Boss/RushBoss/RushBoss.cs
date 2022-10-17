using UnityEngine;

public class RushBoss : BossBase<RushBoss>
{
    [Header("[RushBoss]")]
    public float defaultSpeed;
    public float rushSpeed;

    private float movement;
    private float movementGoal = 0;
    public float MovementGoal { get { return movementGoal; } set { movementGoal = value; } }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        bossFsm = new BossStateMachine<RushBoss>(this, new Idle_RushBoss<RushBoss>());
        bossFsm.AddStateList(new MeleeAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new WaveAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new RushAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new JumpAttack_RushBoss<RushBoss>());
        bossFsm.AddStateList(new Move_RushBoss<RushBoss>());

        StadiumManager.Instance.GetStadiumByType(BossType.Boss2).Active();
    }

    protected override void Update()
    {
        base.Update();
        movement = Mathf.Lerp(movement, movementGoal, Time.deltaTime * 2.5f);
        animator.SetFloat("Movement", movement);
    }
}
