using UnityEngine;

[CreateAssetMenu(menuName = "SO/Sound")]
public class UISoundData : ScriptableObject
{
    [Header("[클릭]")]
    public AudioClip clickClip;
    public AudioClip closeClip;

    [Header("[효과음]")]
    public AudioClip waveChangeClip;
    public AudioClip bossEnterClip;

    [Header("[카드]")]
    public AudioClip cardThrowDownSound;
    public AudioClip cardThrowUpSound;
    public AudioClip cardOnMouseSound;
    public AudioClip cardSelectSound;

    [Header("[포탈]")]
    public AudioClip portalOpenClip;
    public AudioClip portalCloseClip;

    [Header("[플레이어 관련]")]
    public AudioClip playerHitClip;

    [Header("[경험치]")]
    public AudioClip expUpClip;
}
