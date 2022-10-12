using UnityEngine;

[CreateAssetMenu(menuName = "SO/UpgradeDataTable")]
public class UpgradeDataTableSO : ScriptableObject
{
    [Header("[Type]")]
    public UpgradeType upgradeType;

    [Header("[Data]")]
    public float[] datas;
}