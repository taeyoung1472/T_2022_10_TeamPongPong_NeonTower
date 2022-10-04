using System;
using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("[세팅]")]
    [SerializeField] private FirePos[] firePosList;
    // 테스트 코드
    [Range(0, 3)] public int firePosLevel = 0;
    public float fireDelay = 0.2f;

    public void Start()
    {
        StartCoroutine(WeaponCycle());
    }

    IEnumerator WeaponCycle()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKey(KeyCode.Mouse0));

            bool isFirstInstance = true;

            foreach (Transform firePos in firePosList[firePosLevel].firePos)
            {
                Bullet bullet = PoolManager.Instance.Pop(PoolType.Bullet) as Bullet;
                bullet.Init(firePos.position, firePos.rotation, isFirstInstance);
                isFirstInstance = false;
            }

            yield return new WaitForSeconds(fireDelay);
        }
    }

    [Serializable]
    public class FirePos
    {
        public Transform[] firePos;
    }
}
