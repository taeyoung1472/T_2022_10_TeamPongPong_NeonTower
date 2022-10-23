using UnityEngine;

public class UISoundManager : MonoSingleTon<UISoundManager>
{
    public UISoundData data;

    public void PlayClickClip()
    {
        AudioManager.PlayAudio(data.clickClip);
    }
    public void PlayCloseClip()
    {
        AudioManager.PlayAudio(data.closeClip);
    }
}