using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;

    public int maxHealth = 3;
    public GameObject prefab;

    [Header("[공격 관련]")]
    //공격 관련 데이터
    public int damage = 1;
    public float attackDelay = 1;
    public float attackRadius = 2f;
    public float attackDistance = 2f;


    [Header("[무빙 관련]")]
    //무빙 관련 데이터
    [Range(1, 10)]
    public float speed = 3;
    [Range(1f, 10f)]
    public float stoppingDistance = 5f;
    //public float knockbackPower = 5f;
    [Range(0.01f, 2f)] public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    [Header("[대시 관련]")]
    //대쉬 관련
    public float dashDistance = 3.5f;
    public float dashSpeed = 4f;
    public float dashDamage = 3f;
    public float dashTime = 0.5f;

    [Header("[폭발 관련]")]
    //폭발 관련
    public float explosionDistance = 2f;
    public float explosionDamage = 5f;
    public float explosionRange = 3f;


    [Header("[박격포 관련]")]
    //박격포 관련
    public int bulletSpeed;
    public CannonBall bulletPrefab;


    [Header("[오디오 관련]")]
    public AudioClip hitClip; // 피격시 재생할 소리
    public AudioClip deathClip; // 사망시 재생할 소리
}
