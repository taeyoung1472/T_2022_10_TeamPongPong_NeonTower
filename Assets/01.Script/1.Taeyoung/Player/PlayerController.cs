using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    #region Animator Hash
    readonly private int moveHash = Animator.StringToHash("Move");
    #endregion

    #region Audio
    private AudioSource rollingAudioSource;
    private float rollingSoundGoal = 0;
    #endregion

    [Header("[성능]")]
    [SerializeField] private int maxHp = 5;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int maxDash = 3;
    private float dashRechargeDelay = 2f;
    private int curDash;

    private float speedFixValue = 1;

    [Header("[성능 <상수> ]")]
    [SerializeField] private float dashSpeed = 5f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashDelay = 0.25f;
    [SerializeField] private float idleTime = 5f;

    // [Header("[GetSet 프로퍼티]")]
    public float CurSpeed { get 
        { 
            return speed // Default
                * (isDashing ? dashSpeed : 1) // Dash
                * speedFixValue; // Debuf
        } 
    }
    public float SpeedFixValue
    {
        get
        {
            return speedFixValue;
        }
        set
        {
            speedFixValue = value;
        }
    }
    public bool IsDead { get { return isDead; } }
    public bool IsIdle { get { return isIdle; } }
    public int CurDash { get { return curDash; } set { curDash = value; hud.SetDashValue(curDash, maxDash); } }

    [Header("[타이머]")]
    [SerializeField] private float damageIgnoreTime = 0.2f;

    [Header("[상태]")]
    private int curHp;
    private Vector3 moveDir;
    private bool isDead = false;
    private bool isDashing = false;
    private bool isIdle = true;

    public Vector3 MoveDir => moveDir;

    [Header("[사운드]")]
    [SerializeField] private AudioClip dashClip;

    [Header("[참조]")]
    [SerializeField] private ParticleSystem dustParticle;
    [SerializeField] private PlayerHUD hud;

    private Animator playerAnim;
    private CharacterController controller;
    private LayerMask mouseCheckLayer;
    private Camera cam;

    private bool isDamaged = false;

    private void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();

        rollingAudioSource = GetComponent<AudioSource>();

        cam = Camera.main;
        hud.HPMaxValue = maxHp;
        hud.HPValue = maxHp;
        CurDash = maxDash;
    }

    void Start()
    {
        this.Invoke(() => { isIdle = false; }, idleTime);
        SetLayer();
        InitData();

        StartCoroutine(DamageSystem());
        StartCoroutine(DashSystem());
        StartCoroutine(DashCharge());
    }

    private void InitData()
    {
        curHp = maxHp;
    }

    private void SetLayer()
    {
        mouseCheckLayer = 1 << LayerMask.NameToLayer("MouseCheck");
    }

    void Update()
    {
        if (!isIdle)
        {
            Audio();
            if (isDead) return;
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(h, 0, v).normalized;
        h = inputDir.x;
        v = inputDir.z;

        moveDir = new Vector3(h * CurSpeed, moveDir.y, v * CurSpeed);

        if (moveDir.x == 0 && moveDir.z == 0)
        {
            playerAnim.SetBool(moveHash, false);
            rollingSoundGoal = 0;

            playerAnim.transform.localRotation = Quaternion.identity;

            var e = dustParticle.emission;
            e.enabled = false;
        }
        else
        {
            playerAnim.SetBool(moveHash, true);
            rollingSoundGoal = 1;

            Quaternion rot = Quaternion.LookRotation(new Vector3(h, 0, v));
            playerAnim.transform.rotation = rot;

            var e = dustParticle.emission;
            e.enabled = true;
        }

        if (!controller.isGrounded)
            moveDir.y -= Time.deltaTime * 9.8f;
        else
            moveDir.y = 0;

        controller.Move(moveDir * Time.deltaTime);
    }

    private void Rotate()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, mouseCheckLayer))
        {
            Vector3 localHitPos = hit.point - transform.position;

            transform.rotation = Quaternion.LookRotation(new Vector3(localHitPos.x, 0, localHitPos.z));
        }
    }

    public void Dead()
    {
        isDead = true;
        FindObjectOfType<DieEffect>().PlayerDieEffect();
    }

    private void Audio()
    {
        float volume = rollingAudioSource.volume;

        rollingSoundGoal = Time.timeScale != 0 ? Mathf.Lerp(volume, rollingSoundGoal, Time.deltaTime * 5) : 0;
        rollingAudioSource.volume = rollingSoundGoal;
    }

    IEnumerator DamageSystem()
    {
        while (true)
        {
            yield return new WaitUntil(() => isDamaged && !isDead);

            curHp--;
            hud.HPValue = curHp;

            ColorCanvasEffect.Instance.Active(Color.red);

            if (curHp <= 0)
            {
                Dead();
            }

            yield return new WaitForSeconds(damageIgnoreTime);
            isDamaged = false;
        }
    }

    IEnumerator DashSystem()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift) && curDash > 0 && (moveDir.x != 0 || moveDir.z != 0));

            isDashing = true;
            AudioManager.PlayAudio(dashClip);

            yield return new WaitForSeconds(dashDuration);

            isDashing = false;

            CurDash--;

            yield return new WaitForSeconds(dashDelay);
        }
    }

    IEnumerator DashCharge()
    {
        while (true)
        {
            yield return new WaitUntil(() => curDash < maxDash);
            yield return new WaitForSeconds(dashRechargeDelay);
            CurDash++;
        }
    }

    public void ApplyDamage(float dmg)
    {
        if (true) return;
        if (isDead) return;

        isDamaged = true;
    }
}
