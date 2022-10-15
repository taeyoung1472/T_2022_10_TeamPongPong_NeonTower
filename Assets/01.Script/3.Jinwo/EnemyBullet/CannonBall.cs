using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetPos;

    [SerializeField]
    private LayerMask targetLayer;

    private Rigidbody rigid;

    public float maxSpeed;
    public float curSpeed;

    [SerializeField]
    private GameObject bombEffect;

    private Collider col;

    [SerializeField]
    private float CircleSize = 3.5f;
    private void Awake()
    {
        col = GetComponent<Collider>();
        maxSpeed = 5f;
        curSpeed = 1f;
        rigid = GetComponent<Rigidbody>();
        col.enabled = false;
    }
    private void Update()
    {
        if (rigid.velocity.y <= 0f)
        {
            if (!col.enabled)
            {
                col.enabled = true;
            }
            if (curSpeed <= maxSpeed)
            {
                curSpeed += maxSpeed * Time.deltaTime;
            }
            transform.position += transform.up * curSpeed * Time.deltaTime;


            Vector3 dir = (targetPos - transform.position).normalized + new Vector3(0,0.6f, 0);

            transform.up = Vector3.Slerp(transform.up, dir, 0.25f);
        }

    }
    public void SetTargetPos(Vector3 pos, float bulletSpeedData)
    {
        targetPos = pos;
        maxSpeed = bulletSpeedData;

    }
    public void AttackDamage()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, CircleSize, targetLayer);

        foreach (var col in cols)
        {
            IDamageable damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.ApplyDamage(1);
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        AttackDamage();

        //이쯤 어딘가에서 풀 푸쉬하고 플레이어 공격 받는 함수 모시깽 넣으면 될듯?
        GameObject go = Instantiate(bombEffect, targetPos + new Vector3(0, 0.5f, 0), Quaternion.identity);
        gameObject.SetActive(false);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, CircleSize);

    }
#endif
}
