using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossUI : MonoBehaviour
{
    private RectTransform _dangerUI = null;
    private RectTransform _bossNameUI = null;

    private Vector3 _dangerOriginPosition = Vector3.zero;
    private Vector3 _bossNameOriginPosition = Vector3.zero;

    private Sequence _seq = null;

    private Coroutine _dangerCoroutine = null;

    private void Start()
    {
        _dangerUI = GameObject.Find("BossDangerText")?.GetComponent<RectTransform>();
        _bossNameUI = GameObject.Find("BossNameText")?.GetComponent<RectTransform>();

        if(_dangerUI != null && _bossNameUI != null)
        {
            _dangerOriginPosition = _dangerUI.anchoredPosition;
            _bossNameOriginPosition = _bossNameUI.anchoredPosition;
        }
        else
        {
            Debug.Log("¤¸µÊ");
        }
    }

    public void DangerAnimation()
    {
        if (_dangerUI == null || _bossNameUI == null) return;
        if (_seq != null)
        {
            _seq.Kill();
            _bossNameUI.DOKill();
        }

        _dangerUI.anchoredPosition = _dangerOriginPosition;
        _bossNameUI.anchoredPosition = _bossNameOriginPosition;
        float x = Screen.currentResolution.width;

        _seq = DOTween.Sequence();
        _seq.Append(_dangerUI.DOAnchorPosX(x * 0.42f, 2f));
        _seq.Join(_bossNameUI.DOAnchorPosX(x * -0.42f, 2f));
        _seq.AppendInterval(2f);
        _seq.Append(_dangerUI.DOAnchorPosX(_dangerOriginPosition.x, 0.5f));
        _seq.Join(_bossNameUI.DOAnchorPosX(_bossNameOriginPosition.x, 0.5f));
        //_seq.Join();
    }

    private void OnDestroy()
    {
        if (_dangerCoroutine != null)
        {
            StopCoroutine(_dangerCoroutine);
        }
    }
}
