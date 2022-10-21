using MoreMountains.Tools;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Bullet")]
public class BulletDataSO : ScriptableObject
{
    [Header("[��ġ]")]
    public float bulletSpeed = 10;
    [Header("[����]")]
    public AudioClip fireClip;
    public AudioClip collisionClip;
}
