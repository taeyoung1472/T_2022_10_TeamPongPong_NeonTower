using MoreMountains.Tools;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Bullet")]
public class BulletDataSO : ScriptableObject
{
    [Header("[��ġ]")]
    public float bulletSpeed = 10;
    public int damage = 1;
    [Header("[����]")]
    public AudioClip fireClip;
    public AudioClip collisionClip;
}
