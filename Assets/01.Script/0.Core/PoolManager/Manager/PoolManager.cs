using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolManager : MonoSingleTon<PoolManager>
{
    [SerializeField] private PoolDataSO poolData;//Ǯ�� ������
    Dictionary<PoolType, LocalPoolManager> localPoolDic = new Dictionary<PoolType, LocalPoolManager>();//PoolType���� �˻��ϱ� ���� ��ųʸ�
    private void Awake()
    {
        for (int i = 0; i < poolData.poolDatas.Length; i++)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);
            LocalPoolManager localPool = obj.AddComponent<LocalPoolManager>();
            PoolData data = poolData.poolDatas[i];
            localPool.Init(data.InitCount, data.PoolAbleObject, data.PoolType);
            localPoolDic.Add(data.PoolType, localPool);
            localPool.name = $"LocalPool : {data.PoolType}";
        }
    }
    /// <summary>
    /// Type�� �´� ������Ʈ ��������
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public PoolAbleObject Pop(PoolType type)
    {
        return localPoolDic[type].Pop();
    }
    /// <summary>
    /// Type�� �°� ������Ʈ �ֱ�
    /// </summary>
    /// <param name="type"></param>
    /// <param name="obj"></param>
    public void Push(PoolType type, GameObject obj)
    {
        localPoolDic[type].Push(obj.GetComponent<PoolAbleObject>());
        Debug.Log($"{obj.name} �̰� Push��");
    }

}
[Serializable]
public class PoolData/*Ǯ�� ������*/
{
    [SerializeField] private PoolType poolType;
    [SerializeField] private int initCount;
    [SerializeField] private PoolAbleObject poolAbleObject;
    public PoolType PoolType { get { return poolType; } }
    public int InitCount { get { return initCount; } }
    public PoolAbleObject PoolAbleObject { get { return poolAbleObject; } }
}