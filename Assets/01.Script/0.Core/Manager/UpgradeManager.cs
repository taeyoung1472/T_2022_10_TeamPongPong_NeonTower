using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoSingleTon<UpgradeManager>
{
    [SerializeField] private UpgradeDataSO UpgradeDataSO;
    private Dictionary<UpgradeType, int> upgradeCountDic = new();
    [SerializeField] private UpgradeData maxData;

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
        if(upgradeType == UpgradeType.MAX)
        {
            return;
        }
        if(upgradeCountDic[upgradeType] == 0)
        {
            Debug.LogError($"{upgradeType} 의 업그레이드 한도를  넘겼습니다.");

            return;
        }
        upgradeCountDic[upgradeType]--;
        if (upgradeCountDic[upgradeType] < 0)
        {
            upgradeCountDic.Remove(upgradeType);
        }
        UIManager.Instance.DeActiveUI();
        UIManager.Instance.isActiveContinue = false;
    }

  

    public UpgradeData[] GetRandDataList()
    {
        List<UpgradeData> returnData = new();
        List<UpgradeType> generateAbleList = new();
        List<UpgradeTier> generatedTier = new();

        foreach (var type in upgradeCountDic.Keys)
        {
            if (type == UpgradeType.MAX) continue;
            generateAbleList.Add(type);
        }

        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, generateAbleList.Count);
            UpgradeType generatedType = generateAbleList[rand];
            if(generateAbleList.Count != 0)
            {
                if (upgradeCountDic.ContainsKey(generatedType))
                {
                    if (upgradeCountDic[generatedType] == 0)
                    {
                        upgradeCountDic.Remove(generatedType);
                        generateAbleList.Remove(generatedType);
                        i--;
                    }
                    else
                    {
                        if(i == 2 && !generatedTier.Contains(UpgradeTier.Tier1) && UpgradeDataSO.upgradeDataDic[generatedType].upgradeTier != UpgradeTier.Tier1)
                        {
                            i--;
                            continue;
                        }
                        else
                        {
                            generateAbleList.Remove(generatedType);
                            returnData.Add(UpgradeDataSO.upgradeDataDic[generatedType]);
                        }
                    }
                }
                else
                {
                    generateAbleList.Remove(generatedType);
                    i--;
                }
            }
            else
            {
                returnData.Add(maxData);
            }
        }

        return returnData.ToArray();
    }

    public int GetUpgradeCount(UpgradeType type)
    {
        return (int)UpgradeDataSO.upgradeDataDic[type].upgradeAbleCount - (upgradeCountDic.ContainsKey(type)
            ? upgradeCountDic[type] : 0);
    }

    public float GetUpgradeValue(UpgradeType type)
    {
        return UpgradeDataSO.upgradeDataTableDic[type].datas[(int)UpgradeDataSO.upgradeDataDic[type].upgradeAbleCount - (upgradeCountDic.ContainsKey(type) ? upgradeCountDic[type] : 0)];
    }
}
