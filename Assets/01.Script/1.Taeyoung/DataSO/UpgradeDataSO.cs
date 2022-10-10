using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Upgrade")]
public class UpgradeDataSO : ScriptableObject
{
    [Header("[UpgardeDataTable List]")]
    public List<UpgradeDataTableSO> upgradeDataTableList = new();
    public Dictionary<UpgradeType, UpgradeDataTableSO> upgradeDataTableDic = new();

    [Header("[UpgradeData]")]
    public UpgradeData[] upgradeDataList;
    public Dictionary<UpgradeType, UpgradeData> upgradeDataDic = new();

    public void Init()
    {
        upgradeDataDic.Clear();
        foreach (var data in upgradeDataList)
        {
            upgradeDataDic.Add(data.upgradeType, data);
        }

        upgradeDataTableDic.Clear();
        foreach (var table in upgradeDataTableList)
        {
            upgradeDataTableDic.Add(table.upgradeType, table);
        }
    }

    public void OnValidate()
    {
        Init();

        for (int i = 0; i < (int)UpgradeType.End; i++)
        {
            float[] tempDatas = upgradeDataTableDic[(UpgradeType)i].datas;
            upgradeDataTableDic[(UpgradeType)i].datas = new float[(int)(upgradeDataDic[(UpgradeType)i].upgradeAbleCount) + 1];
            for (int j = 0; j < tempDatas.Length; j++)
            {
                try
                {
                    upgradeDataTableDic[(UpgradeType)i].datas[j] = tempDatas[j];
                }
                catch (IndexOutOfRangeException)
                {
                    Debug.LogError($"{(UpgradeType)i} 의 {j}번째 데이터 {tempDatas[j]}가 버려짐");
                }
            }
        }
    }

    [Serializable]
    public class UpgradeData
    {
        [Header("[Enum]")]
        public UpgradeType upgradeType;
        public UpgradeAbleCount upgradeAbleCount;

        [Header("[정보]")]
        public string upgradeName = "업그레이드 이름";
        [Multiline(5)] public string upgradeDesc = "업그레이드 설명";
    }
}

public enum UpgradeAbleCount
{
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    End = 5
}

public enum UpgradeType
{
    Hp,
    Speed,
    AutoHeal,
    BulletSpeed,

    End
}