using UnityEngine;

[CreateAssetMenu(menuName = "SO/BossData")]
public class BossDataSO : ScriptableObject
{
    [Header("기본 정보")]
    public Sprite bossProfile;
    public string bossName;

    [Header("성능 정보")]
    public float speed = 5;
    public float damage = 1;
    public float maxHp = 100;
    public float attackRange = 10;
    public float patternCoolTime = 4;
}
