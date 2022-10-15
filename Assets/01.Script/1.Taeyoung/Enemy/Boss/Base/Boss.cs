using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    #region 변수
    [Header("상태변수")]
    protected float curHp;
    protected bool isDead;
    public float CurHp { get { return curHp; } set { curHp = value; } }
    public bool IsDead { get { return isDead; } set { isDead = value; } }

    [Header("[Ref]")]
    [SerializeField] protected BossDataSO data;
    protected Transform target;
    public Transform Target { get { if (target == null) { target = FindObjectOfType<PlayerController>().transform; } return target; } }
    public BossDataSO Data { get { return data; } }

    [Header("[Event]")]
    public UnityEvent OnDeathEvent;
    #endregion
}
