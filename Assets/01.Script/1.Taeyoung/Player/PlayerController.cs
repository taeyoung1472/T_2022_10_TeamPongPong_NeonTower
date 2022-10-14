using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    [Header("[성능 <상수> ]")]
    [SerializeField] private float dashSpeed = 5f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashDelay = 0.25f;

    // [Header("[GetSet 프로퍼티]")]
    public float CurSpeed { get 
        { 
            return speed * (isDashing ? dashSpeed : 1); 
        } 
    }

    [Header("[타이머]")]
    [SerializeField] private float damageIgnoreTime = 0.2f;

    [Header("[상태]")]
    private int curHp;
    private Vector3 moveDir;
    private bool isDead = false;
    private bool isDashing = false;

    [Header("[사운드]")]
    [SerializeField] private AudioClip dashClip;

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
    }

    void Start()
    {
        SetLayer();
        InitData();

        StartCoroutine(DamageSystem());
        StartCoroutine(DashSystem());
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
        Move();
        Rotate();
        Audio();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDamaged = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            DangerZone.DrawArc(transform.position, transform.forward, 2, new Vector3(2, 1, 5), 3);
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
        }
        else
        {
            playerAnim.SetBool(moveHash, true);
            rollingSoundGoal = 1;

            Quaternion rot = Quaternion.LookRotation(new Vector3(h, 0, v));
            playerAnim.transform.rotation = rot;
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

    private void Dead()
    {
        isDead = true;
    }

    private void Audio()
    {
        float volume = rollingAudioSource.volume;

        rollingAudioSource.volume = rollingSoundGoal;
        rollingSoundGoal = Mathf.Lerp(volume, rollingSoundGoal, Time.deltaTime * 5);
    }

    IEnumerator DamageSystem()
    {
        while (true)
        {
            yield return new WaitUntil(() => isDamaged && !isDead);

            curHp--;

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
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));

            isDashing = true;
            AudioManager.PlayAudio(dashClip);

            yield return new WaitForSeconds(dashDuration);

            isDashing = false;

            yield return new WaitForSeconds(dashDelay);
        }
    }
}
