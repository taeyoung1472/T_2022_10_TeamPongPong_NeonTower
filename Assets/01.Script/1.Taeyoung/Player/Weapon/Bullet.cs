using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Bullet : PoolAbleObject
{
    // [Get Set ÇÁ·ÎÆÛÆ¼]
    private float Speed { get { return data.bulletSpeed * UpgradeManager.Instance.GetUpgradeValue(UpgradeType.BulletSpeed); } }
    private float Damage { get { return UpgradeManager.Instance.GetUpgradeValue(UpgradeType.BulletDamage); } }
    private AudioClip FireClip { get { return data.fireClip; } }
    private AudioClip CollisionClip { get { return data.collisionClip; } }

    [Header("[µ¥ÀÌÅÍ]")]
    [SerializeField] private BulletDataSO data;
    Vector3 inDir;

    [Header("[º¯¼ö]")]
    private Rigidbody rb;
    private int bounceChance;
    private bool isCanExplosion;
    private bool isCanKnockBack;
    private float knockBackForce = 0f;
    private float explosionRadius = 2.5f;

    [Header("[·¹ÀÌ¾î]")]
    private LayerMask enemyLayer;

    public void Start()
    {
        enemyLayer |= LayerMask.GetMask("Enemy");
    }

    public void OnCollisionEnter(Collision collision)
    {
        AudioManager.PlayAudioRandPitch(CollisionClip, 1, 0.1f, 0.5f);

        GameObject bulletImpact = PoolManager.Instance.Pop(PoolType.BulletImpact).gameObject;
        bulletImpact.transform.SetPositionAndRotation(transform.position, transform.rotation);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isCanExplosion)
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);

                PoolManager.Instance.Pop(PoolType.BulletExplosionImpact).transform.
                    SetPositionAndRotation(transform.position, Quaternion.LookRotation(inDir));

                foreach (var col in cols)
                {
                    col.GetComponent<IDamageable>()?.ApplyDamage(Damage * UpgradeManager.Instance.GetUpgradeValue(UpgradeType.BulletExplosion));
                }
            }

            collision.gameObject.GetComponent<IDamageable>().ApplyDamage(Damage);
            //ÃÑ¾Ë ¼Ò¸ê
            PoolManager.Instance.Push(PoolType, gameObject);
        }
        else
        {
            //ÃÑ¾Ë ¹Ý»ç
            if (bounceChance > 0)
            {
                #region ÃÑ¾Ë ¹Ý»ç
                Vector3 dir = Vector3.Reflect(inDir, collision.contacts[0].normal).normalized;
                dir.y = 0;
                dir = dir.normalized;
                rb.velocity = dir * Speed;

                /*
                Utility.DrawRay(collision.contacts[0].point - inDir * 10, inDir, 10, 1, Color.blue);
                Utility.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, 10, 1, Color.red);
                Utility.DrawRay(collision.contacts[0].point, dir, 10, 1, Color.yellow);
                */

                inDir = dir;
                #endregion

                bounceChance--;
            }

            //ÃÑ¾Ë ¼Ò¸ê
            else if (bounceChance <= 0)
            {
                PoolManager.Instance.Push(PoolType, gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        AudioManager.PlayAudioRandPitch(CollisionClip, 1, 0.1f, 0.5f);

        GameObject bulletImpact = PoolManager.Instance.Pop(PoolType.BulletImpact).gameObject;
        bulletImpact.transform.SetPositionAndRotation(transform.position, transform.rotation);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isCanExplosion)
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);

                PoolManager.Instance.Pop(PoolType.BulletExplosionImpact).transform.
                    SetPositionAndRotation(transform.position, Quaternion.LookRotation(inDir));

                foreach (var col in cols)
                {
                    col.GetComponent<IDamageable>()?.ApplyDamage(Damage * UpgradeManager.Instance.GetUpgradeValue(UpgradeType.BulletExplosion));
                }
            }
            else
            {
                collision.gameObject.GetComponent<IDamageable>().ApplyDamage(Damage);
            }

            //ÃÑ¾Ë ¼Ò¸ê
            PoolManager.Instance.Push(PoolType, gameObject);
        }
        
    }

    public void Init(Vector3 pos, Quaternion rot, bool isFirst)
    {
        transform.SetPositionAndRotation(pos, rot);
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = transform.forward * Speed;
        inDir = transform.forward;

        if (isFirst)
        {
            AudioManager.PlayAudioRandPitch(FireClip);
        }

        ParticlePool muzzleImpact = PoolManager.Instance.Pop(PoolType.BulletMuzzleImpact) as ParticlePool;
        muzzleImpact.Set(transform.position, transform.rotation);
    }

    public override void Init_Pop()
    {
        bounceChance = (int)UpgradeManager.Instance.GetUpgradeValue(UpgradeType.BulletBounce);
        Debug.Log("Bounce Chance : " + (int)UpgradeManager.Instance.GetUpgradeValue(UpgradeType.BulletBounce));
        knockBackForce = UpgradeManager.Instance.GetUpgradeValue(UpgradeType.BulletKnockback);
        isCanExplosion = UpgradeManager.Instance.GetUpgradeValue(UpgradeType.BulletExplosion) != 0;
        isCanKnockBack = (int)UpgradeManager.Instance.GetUpgradeValue(UpgradeType.BulletKnockback) != 0;
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    public override void Init_Push()
    {
        rb.velocity = Vector3.zero;
    }

}
