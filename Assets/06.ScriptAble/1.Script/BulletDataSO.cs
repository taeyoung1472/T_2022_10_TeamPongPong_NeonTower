using MoreMountains.Tools;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Bullet")]
public class BulletDataSO : ScriptableObject
{
    [Header("[수치]")]
    public float bulletSpeed = 10;
    [Header("[사운드]")]
    public AudioClip fireClip;
    public AudioClip collisionClip;
}
