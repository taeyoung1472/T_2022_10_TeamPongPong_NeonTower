using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private LayerMask targetLayer;

    private Rigidbody rigid;

    public float maxSpeed;
    public float curSpeed;

    [SerializeField]
    private GameObject bombEffect;

    [SerializeField]
    private Transform childTrm;

    private Vector3 prevPos;
    private Vector3 dir;
    private void Awake()
    {
        childTrm = GetComponentInChildren<Transform>();
        maxSpeed = 5f;
        curSpeed = 1f;
        rigid = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        dir = childTrm.position - prevPos;
        dir.Normalize();
        prevPos = childTrm.position;

    }
    private void Update()
    {
        if (rigid.velocity.y <= 0f)
        {
            if (curSpeed <= maxSpeed)
            {
                curSpeed += maxSpeed * Time.deltaTime;
            }
            Debug.Log("최고");
            transform.position += (target.position - transform.position).normalized * curSpeed * Time.deltaTime;

            childTrm.rotation = Quaternion.LookRotation(dir);

            //Vector3 dir = (target.position - transform.position).normalized;

            //transform.up = Vector3.Lerp(transform.up, dir, 0.5f);
        }
        else
        {
            childTrm.rotation = Quaternion.LookRotation(rigid.velocity);
        }

    }
    public void SetTargetPos(Transform pos, float bulletSpeedData)
    {
        target = pos;
        maxSpeed = bulletSpeedData;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("박격포 공격 성공");
        }
        else if(collision.gameObject.CompareTag("Wall"))
        {
            GameObject go = Instantiate(bombEffect, transform);
            gameObject.SetActive(false);
            //gameObject.SetActive(false);
        }

        

    }

}
