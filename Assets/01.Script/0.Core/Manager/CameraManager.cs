using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoSingleTon<CameraManager>
{
    [SerializeField]
    private static CinemachineVirtualCamera _cmVCam = null;

    private CinemachineBasicMultiChannelPerlin _noise = null;

    private Coroutine _zoomCoroutine = null;
    private Coroutine _shakeCoroutine = null;

    private float _currentShakeAmount = 0f;

    [SerializeField]
    private BossUI _bossUI = null;

    private void OnEnable()
    {
        if (_cmVCam == null)
        {
            _cmVCam = FindObjectOfType<CinemachineVirtualCamera>();
        }

        _noise = _cmVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void CompletePrevFeedBack()
    {
        if (_shakeCoroutine != null)
        {
            StopCoroutine(_shakeCoroutine);
        }

        _noise.m_FrequencyGain = 0; // Èçµå´Â ºóµµ Á¤µµ
        _noise.m_AmplitudeGain = 0;
        _currentShakeAmount = 0f;
    }

    /// <summary>
    /// Ä«¸Þ¶ó ÈçµéÈçµé
    /// </summary>
    /// <param name="Èçµé¸®´Â Á¤µµ"></param>
    /// <param name="Èçµé¸®´Â ºóµµ"></param>
    /// <param name="Èçµé¸± ½Ã°£"></param>
    public void CameraShake(float amplitude, float intensity, float duration)
    {
        Debug.Log(_currentShakeAmount);
        if (_currentShakeAmount > amplitude)
        {
            return;
        }
        CompletePrevFeedBack();

        _noise.m_AmplitudeGain = amplitude; // Èçµé¸®´Â Á¤µµ
        _noise.m_FrequencyGain = intensity; // Èçµå´Â ºóµµ Á¤µµ

        _currentShakeAmount = _noise.m_AmplitudeGain;

        _shakeCoroutine = StartCoroutine(ShakeCorutine(amplitude, duration));
    }

    private IEnumerator ShakeCorutine(float amplitude, float duration)
    {
        float time = duration;
        while (time >= 0)
        {
            _noise.m_AmplitudeGain = Mathf.Lerp(0, amplitude, time / duration);
            yield return null;
            time -= Time.deltaTime;
        }
        CompletePrevFeedBack();
    }

    public void ZoomCamera(float maxValue, float time, Action Callback = null)
    {
        CameraReset();

        _zoomCoroutine = StartCoroutine(ZoomCoroutine(maxValue, time, Callback));
    }

    private IEnumerator ZoomCoroutine(float maxValue, float duration, Action Callback = null)
    {
        float time = 0f;
        float nextLens = 0f;
        float currentLens = _cmVCam.m_Lens.FieldOfView;

        while (time <= duration)
        {
            nextLens = Mathf.Lerp(currentLens, maxValue, time / duration);
            _cmVCam.m_Lens.FieldOfView = nextLens;
            yield return null;
            time += Time.deltaTime;
        }
        Callback?.Invoke();
    }


    public void CameraReset()
    {
        if (_zoomCoroutine != null)
        {
            StopCoroutine(_zoomCoroutine);
        }
    }

    public void TargetingBossCameraAnimation(Transform bossTrm, float idleTime, float dangerIdleTime, float zoomAmount = 12f)
    {
        Transform lastTarget = _cmVCam.Follow;
        _cmVCam.Follow = bossTrm;
        float last = _cmVCam.m_Lens.FieldOfView;

        if (dangerIdleTime == 0f)
            dangerIdleTime = 3f;
        StartCoroutine(TargetingCameraCoroutine(true, last, lastTarget, idleTime, dangerIdleTime, zoomAmount));
    }

    public void TargetingCameraAnimation(Transform target, float idleTime, float zoomAmount = 12f)
    {
        Transform lastTarget = _cmVCam.Follow;
        _cmVCam.Follow = target;
        float last = _cmVCam.m_Lens.FieldOfView;
        StartCoroutine(TargetingCameraCoroutine(false, last, lastTarget, idleTime,0f, zoomAmount));
    }

    private IEnumerator TargetingCameraCoroutine(bool isBoss, float last, Transform lastTarget, float idleTime, float dangerIdleTime, float zoomAmount)
    {
        yield return new WaitForSeconds(0.5f);
        if (isBoss)
        {
            _bossUI?.DangerAnimation(dangerIdleTime);
        }
        ZoomCamera(_cmVCam.m_Lens.FieldOfView - zoomAmount, 0.5f);

        yield return new WaitForSeconds(idleTime);
        ZoomCamera(last, 0.2f, () =>
        {
            _cmVCam.Follow = lastTarget;
        });
    }
}
