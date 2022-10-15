using UnityEngine;
using UnityEngine.Events;

public abstract class BossBase<T> : MonoBehaviour
{
    #region 변수
    [Header("상태변수")]
    private float curHp;
    private bool isDead;
    public float CurHp { get { return curHp; } set { curHp = value; } }
    public bool IsDead { get { return isDead; } set { isDead = value; } }

    [Header("[FSM]")]
    protected BossStateMachine<T> bossFsm;
    public BossStateMachine<T> BossFsm => bossFsm;

    [Header("[Ref]")]
    [SerializeField] private BossDataSO data;
    private Transform target;
    public Transform Target { get { if (target == null) { target = FindObjectOfType<PlayerController>().transform; } return target; } }
    public BossDataSO Data { get { return data; } }

    [Header("[Event]")]
    public UnityEvent OnDeathEvent;
    #endregion

    public void LookTarget()
    {
        transform.rotation = Quaternion.LookRotation(Target.position - transform.position);
    }

    protected virtual void Update()
    {
        bossFsm.Execute();
    }
}