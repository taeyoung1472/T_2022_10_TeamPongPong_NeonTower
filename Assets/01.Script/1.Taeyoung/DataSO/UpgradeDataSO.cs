using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/Upgrade")]
public class UpgradeDataSO : ScriptableObject
{
    public UpgradeData[] upgradeDatas;
    public Dictionary<UpgradeType, UpgradeData> upgradeDataDic = new Dictionary<UpgradeType, UpgradeData>();

    public void Init()
    {
        upgradeDataDic.Clear();

        foreach (var data in upgradeDatas)
        {
            upgradeDataDic.Add(data.upgradeType, data);
        }
    }

    [Serializable]
    public class UpgradeData
    {
        [Header("[Enum]")]
        public UpgradeType upgradeType;
        public UpgradeAbleCount upgradeAbleCount;

        [Header("[����]")]
        public string upgradeName = "���׷��̵� �̸�";
        [Multiline(5)] public string upgradeDesc = "���׷��̵� ����";
    }
}

public enum UpgradeAbleCount
{
    One,
    Two,
    Three,
    Four,
    Infinity,
    End
}

public enum UpgradeType
{
    Hp,
    Speed,
    AutoHeal,
}