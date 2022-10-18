using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoSingleTon<UpgradeManager>
{
    [SerializeField] private UpgradeDataSO UpgradeDataSO;
    private Dictionary<UpgradeType, int> upgradeCountDic = new();

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

        foreach (var type in upgradeCountDic.Keys)
        {
            generateAbleList.Add(type);
        }

        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, generateAbleList.Count);
            UpgradeType generatedType = generateAbleList[rand];
            generateAbleList.Remove(generatedType);

            returnData.Add(UpgradeDataSO.upgradeDataDic[generatedType]);
        }

        return returnData.ToArray();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)){
            Upgrade(UpgradeType.BulletSpeed);
        }
    }

    public float GetUpgradeValue(UpgradeType type)
    {
        //print((int)UpgradeDataSO.upgradeDataDic[type].upgradeAbleCount - upgradeCountDic[type]);
        return UpgradeDataSO.upgradeDataTableDic[type].datas[(int)UpgradeDataSO.upgradeDataDic[type].upgradeAbleCount - upgradeCountDic[type]];
    }
}
