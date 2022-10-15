using UnityEngine;

[CreateAssetMenu(menuName = "SO/BossData")]
public class BossDataSO : ScriptableObject
{
    public float speed = 5;
    public float damage = 1;
    public float maxHp = 100;
    public float attackRange = 10;
    public float patternCoolTime = 4;
}
