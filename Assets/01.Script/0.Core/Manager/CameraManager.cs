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

        _noise.m_FrequencyGain = 0; // 흔드는 빈도 정도
        _noise.m_AmplitudeGain = 0;
        _currentShakeAmount = 0f;
    }

    /// <summary>
    /// 카메라 흔들흔들
    /// </summary>
    /// <param name="흔들리는 정도"></param>
    /// <param name="흔들리는 빈도"></param>
    /// <param name="흔들릴 시간"></param>
    public void CameraShake(float amplitude, float intensity, float duration)
    {
        if (_currentShakeAmount > amplitude)
        {
            return;
        }
        CompletePrevFeedBack();

        _noise.m_AmplitudeGain = amplitude; // 흔들리는 정도
        _noise.m_FrequencyGain = intensity; // 흔드는 빈도 정도

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
    /// 보스 UI 시작 애니메이션입니다
    /// </summary>
    /// <param name="쫒아갈 target"></param>
    /// <param name="다 쫒아가고 얼마나 기다릴 건지"></param>
    /// <param name="Danger가 얼마나 지속될 것인지"></param>
    /// <param name="얼마나 줌인할 것인지"></param>
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
        StartCoroutine(TargetingCameraCoroutine(false, last, lastTarget, idleTime,0f, zoomAmount));
    }

    private IEnumerator TargetingCameraCoroutine(bool isBoss, float last, Transform lastTarget, float idleTime, float dangerIdleTime, float zoomAmount, Boss boss = null)
    {
        var tr = _cmVCam.GetCinemachineComponent<CinemachineTransposer>();
        DOTween.To(() => tr.m_FollowOffset, x => tr.m_FollowOffset = x, new Vector3(0f, 9f, 3f), 0.3f);
        //tr.m_FollowOffset = new Vector3(0f, 9f, 3f);
        _cmVCam.transform.DORotate(new Vector3(70f, 0f, 0f), 0.3f);

        yield return new WaitForSeconds(0.5f);
        if (isBoss)
        {
            BossUIManager.Instance?.DangerAnimation(dangerIdleTime, boss);
        }
        ZoomCamera(_cmVCam.m_Lens.FieldOfView - zoomAmount, 0.5f);

        yield return new WaitForSeconds(idleTime);
        ZoomCamera(last, 0.2f, () =>
        {
            _cmVCam.Follow = lastTarget;
            //tr.m_FollowOffset = new Vector3(0f, 9f, 0f);
            DOTween.To(() => tr.m_FollowOffset, x => tr.m_FollowOffset = x, new Vector3(0f, 9f, 0f), 0.5f);
            _cmVCam.transform.DORotate(new Vector3(90f, 0f, 0f), 0.2f);
        });
    }
}
