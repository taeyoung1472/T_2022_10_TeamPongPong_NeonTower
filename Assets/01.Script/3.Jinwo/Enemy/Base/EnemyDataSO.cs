using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;

    public int maxHealth = 3;
    public GameObject prefab;

    [Header("���� ����")]
    //���� ���� ������
    public int damage = 1;
    public float attackDelay = 1;
    public float attackRadius = 2f;
    public float attackDistance = 2f;


    [Header("���� ����")]
    //���� ���� ������
    [Range(1, 10)]
    public float maxSpeed = 3;
    [Range(1f, 10f)]
    public float stoppingDistance = 5f;
    //public float knockbackPower = 5f;


    [Header("��� ����")]
    //�뽬 ����
    public float dashDistance = 3.5f;
    public float dashSpeed = 4f;
    public float dashDamage = 3f;


    [Header("���� ����")]
    //���� ����
    public float explosionDistance = 2f;
    public float explosionDamage = 5f;
    public float explosionRange = 3f;


    [Header("�ڰ��� ����")]
    //�ڰ��� ����
    public int bulletSpeed;



    [Header("����� ����")]
    public AudioClip hitClip; // �ǰݽ� ����� �Ҹ�
    public AudioClip deathClip; // ����� ����� �Ҹ�
}
