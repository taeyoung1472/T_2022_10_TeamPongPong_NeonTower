using UnityEngine;

[CreateAssetMenu(menuName = "SO/Sound")]
public class UISoundData : ScriptableObject
{
    [Header("[Ŭ��]")]
    public AudioClip clickClip;
    public AudioClip closeClip;

    [Header("[ȿ����]")]
    public AudioClip waveChangeClip;
    public AudioClip bossEnterClip;
    public AudioClip glitchClip;
    public AudioClip popClip;
    public AudioClip tickClip;
    public AudioClip blockUpClip;
    public AudioClip blockDownClip;

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

    [Header("[����ġ]")]
    public AudioClip expUpClip;
}
