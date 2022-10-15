using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;

    public int maxHealth = 3;
    public GameObject prefab;

    [Header("[���� ����]")]
    //���� ���� ������
    public int damage = 1;
    public float attackDelay = 1;
    public float attackRadius = 2f;
    public float attackDistance = 2f;


    [Header("[���� ����]")]
    //���� ���� ������
    [Range(1, 10)]
    public float speed = 3;
    [Range(1f, 10f)]
    public float stoppingDistance = 5f;
    //public float knockbackPower = 5f;
    [Range(0.01f, 2f)] public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    [Header("[��� ����]")]
    //�뽬 ����
    public float dashDistance = 3.5f;
    public float dashSpeed = 4f;
    public float dashDamage = 3f;
    public float dashTime = 0.5f;

    [Header("[���� ����]")]
    //���� ����
    public float explosionDistance = 2f;
    public float explosionDamage = 5f;
    public float explosionRange = 3f;


    [Header("[�ڰ��� ����]")]
    //�ڰ��� ����
    public int bulletSpeed;
    public CannonBall bulletPrefab;


    [Header("[����� ����]")]
    public AudioClip hitClip; // �ǰݽ� ����� �Ҹ�
    public AudioClip deathClip; // ����� ����� �Ҹ�
}
