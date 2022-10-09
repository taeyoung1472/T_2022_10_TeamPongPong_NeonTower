using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeManager : MonoSingleTon<UpgradeManager>
{
    [SerializeField] private UpgradeDataSO UpgradeDataSO;
    private Dictionary<UpgradeType, int> upgradeCountDic;

    public void Start()
    {
        UpgradeDataSO.Init();
        foreach (var data in UpgradeDataSO.upgradeDataDic.Values)
        {
            upgradeCountDic.Add(data.upgradeType, (int)data.upgradeAbleCount);
        }
    }

    public void Upgrade(UpgradeType upgradeType)
    {
        upgradeCountDic[upgradeType]--;
        if(upgradeCountDic[upgradeType] == 0)
        {
            upgradeCountDic.Remove(upgradeType);
        }
    }
}
