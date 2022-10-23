using Cinemachine;
using DG.Tweening;
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
            time -= Time.unscaledDeltaTime;
        }
        CompletePrevFeedBack();
    }

    public void ZoomCamera(float maxValue, float time, Action Callback = null)
    {

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
        _cmVCam.m_Lens.FieldOfView = maxValue;
        Callback?.Invoke();
    }


    public void CameraReset()
    {
        if (_zoomCoroutine != null)
        {
            StopCoroutine(_zoomCoroutine);
        }
    }

    /// <summary>
    /// ���� UI ���� �ִϸ��̼��Դϴ�
    /// </summary>
    /// <param name="�i�ư� target"></param>
    /// <param name="�� �i�ư��� �󸶳� ��ٸ� ����"></param>
    /// <param name="Danger�� �󸶳� ���ӵ� ������"></param>
    /// <param name="�󸶳� ������ ������"></param>
    public void TargetingBossCameraAnimation(Boss boss, float idleTime, float zoomAmount = 12f)
    {
        //Transform lastTarget = _cmVCam.Follow;
        Transform lastTarget = Define.Instance.playerController.transform;
        _cmVCam.Follow = boss.transform;
        float last = _cmVCam.m_Lens.FieldOfView;
        float dangerIdleTime = idleTime - 2f;
        StartCoroutine(TargetingCameraCoroutine(true, last, lastTarget, idleTime, dangerIdleTime, zoomAmount, boss));
    }

    public void TargetingCameraAnimation(Transform target, float idleTime, float zoomAmount = 12f)
    {
        //Transform lastTarget = _cmVCam.Follow;
        Transform lastTarget = Define.Instance.playerController.transform;
        _cmVCam.Follow = target;
        float last = _cmVCam.m_Lens.FieldOfView;
        StartCoroutine(TargetingCameraCoroutine(false, last, lastTarget, idleTime, 0f, zoomAmount));
    }

    private IEnumerator TargetingCameraCoroutine(bool isBoss, float last, Transform lastTarget, float idleTime, float dangerIdleTime, float zoomAmount, Boss boss = null)
    {

        yield return new WaitForSeconds(0.5f);
        if (isBoss)
        {
            BossUIManager.Instance?.DangerAnimation(dangerIdleTime, boss);
        }
        ZoomCamera(_cmVCam.m_Lens.FieldOfView - zoomAmount, 0.5f);

        yield return new WaitForSeconds(idleTime);
        ZoomCamera(last, 0.2f, () =>
        {
            if (isBoss)
            {
                _cmVCam.Follow = lastTarget;
                Define.Instance.playerController.IsIdle = false;
            }
            else
                _cmVCam.Follow = lastTarget;

            if (!isBoss)
            {
                WaveManager.Instance.IsBossClear = true;
            }
        });
    }

    public void TargetingCameraAnimationUnscale(Transform target, float idleTime, float zoomAmount = 12f)
    {
        //Transform lastTarget = _cmVCam.Follow;
        Transform lastTarget = Define.Instance.playerController.transform;
        _cmVCam.Follow = target;
        float last = _cmVCam.m_Lens.FieldOfView;
        StartCoroutine(TargetingCameraCoroutineUnsacle(false, last, lastTarget, idleTime, 0f, zoomAmount));
    }

    private IEnumerator TargetingCameraCoroutineUnsacle(bool isBoss, float last, Transform lastTarget, float idleTime, float dangerIdleTime, float zoomAmount, Boss boss = null)
    {

        yield return new WaitForSecondsRealtime(0.5f);
        if (isBoss)
        {
            BossUIManager.Instance?.DangerAnimation(dangerIdleTime, boss);
        }
        ZoomCameraUnscale(_cmVCam.m_Lens.FieldOfView - zoomAmount, 0.5f);

        yield return new WaitForSecondsRealtime(idleTime);
        ZoomCameraUnscale(last, 0.2f, () =>
        {
            if (isBoss)
            {
                _cmVCam.Follow = lastTarget;
                Define.Instance.playerController.IsIdle = false;
            }
            else
                _cmVCam.Follow = lastTarget;

            if (!isBoss)
            {
                WaveManager.Instance.IsBossClear = true;
            }
        });
    }

    public void ZoomCameraUnscale(float maxValue, float time, Action Callback = null)
    {

        _zoomCoroutine = StartCoroutine(ZoomCoroutineUnscale(maxValue, time, Callback));
    }

    private IEnumerator ZoomCoroutineUnscale(float maxValue, float duration, Action Callback = null)
    {
        float time = 0f;
        float nextLens = 0f;
        float currentLens = _cmVCam.m_Lens.FieldOfView;

        while (time <= duration)
        {
            nextLens = Mathf.Lerp(currentLens, maxValue, time / duration);
            _cmVCam.m_Lens.FieldOfView = nextLens;
            yield return new WaitForSecondsRealtime(0.01f);
            time += Time.unscaledDeltaTime;
        }
        _cmVCam.m_Lens.FieldOfView = maxValue;
        Callback?.Invoke();
    }
}
