using UnityEngine;

[CreateAssetMenu(menuName = "SO/Sound")]
public class UISoundData : ScriptableObject
{
    public AudioClip clickClip;
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
}
