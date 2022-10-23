using UnityEngine;

public class UISoundManager : MonoSingleTon<UISoundManager>
{
    [SerializeField] private AudioClip clickClip;

    public void PlayClickClip()
    {
        AudioManager.PlayAudio(clickClip);
    }
}