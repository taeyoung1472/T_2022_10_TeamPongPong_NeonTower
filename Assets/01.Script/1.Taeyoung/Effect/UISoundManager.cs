using UnityEngine;

public class UISoundManager : MonoSingleTon<UISoundManager>
{
    public UISoundData data;
    [SerializeField] private AudioClip clickClip;

    public void PlayClickClip()
    {
        AudioManager.PlayAudio(clickClip);
    }
}