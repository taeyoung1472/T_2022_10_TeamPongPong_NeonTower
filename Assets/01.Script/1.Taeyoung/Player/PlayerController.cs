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

    [Header("[타이머]")]
    [SerializeField] private float damageIgnoreTime = 0.2f;

    [Header("[상태]")]
    private int curHp;
    private Vector3 moveDir;
    private bool isDead = false;

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
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(h * speed, moveDir.y, v * speed);

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
        Debug.Log("나 죽었어");
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
}
