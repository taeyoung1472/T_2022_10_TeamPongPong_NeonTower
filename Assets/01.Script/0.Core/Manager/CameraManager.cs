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

        _noise.m_FrequencyGain = 0; // ���� �� ����
        _noise.m_AmplitudeGain = 0;
        _currentShakeAmount = 0f;
    }

    /// <summary>
    /// ī�޶� ������
    /// </summary>
    /// <param name="��鸮�� ����"></param>
    /// <param name="��鸮�� ��"></param>
    /// <param name="��鸱 �ð�"></param>
    public void CameraShake(float amplitude, float intensity, float duration)
    {
        Debug.Log(_currentShakeAmount);
        if (_currentShakeAmount > amplitude)
        {
            return;
        }
        CompletePrevFeedBack();

        _noise.m_AmplitudeGain = amplitude; // ��鸮�� ����
        _noise.m_FrequencyGain = intensity; // ���� �� ����

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

    public void TargetingCameraAnimation(Transform target)
    {
        Transform lastTarget = _cmVCam.Follow;
        _cmVCam.Follow = target;
        float last = _cmVCam.m_Lens.FieldOfView;
        ZoomCamera(_cmVCam.m_Lens.FieldOfView - 12f, 1f, () =>
        {
            StartCoroutine(TargetingCameraCoroutine(last, lastTarget));
        });
    }

    private IEnumerator TargetingCameraCoroutine(float last, Transform lastTarget)
    {
        yield return new WaitForSeconds(2f);
        ZoomCamera(last, 1f, () =>
        {
            _cmVCam.Follow = lastTarget;
        });
    }
}
