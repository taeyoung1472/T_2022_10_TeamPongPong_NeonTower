using UnityEngine;

[CreateAssetMenu(menuName = "SO/Sound")]
public class UISoundData : ScriptableObject
{
    public AudioClip clickClip;
    [Header("[ī��]")]
    public AudioClip cardThrowDownSound;
    public AudioClip cardThrowUpSound;
    public AudioClip cardOnMouseSound;
    public AudioClip cardSelectSound;

    [Header("[��Ż]")]
    public AudioClip portalOpenClip;
    public AudioClip portalCloseClip;

    [Header("[�÷��̾� ����]")]
    public AudioClip playerHitClip;
}
