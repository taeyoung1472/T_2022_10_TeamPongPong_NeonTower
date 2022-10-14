using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossUI : MonoBehaviour
{
    private RectTransform _dangerUI = null;
    private RectTransform _bossNameUI = null;
    private RectTransform _upImageUI = null;
    private RectTransform _downImageUI = null;

    private Vector3 _dangerOriginPosition = Vector3.zero;
    private Vector3 _bossNameOriginPosition = Vector3.zero;

    private Sequence _seq = null;

    private void Start()
    {
        Transform perent = GameObject.Find("BossCanvas").transform;
        _dangerUI = perent?.GetChild(0).GetComponent<RectTransform>();
        _bossNameUI = perent?.GetChild(1).GetComponent<RectTransform>();
        _upImageUI = perent?.GetChild(2).GetComponent<RectTransform>();
        _downImageUI = perent?.GetChild(3).GetComponent<RectTransform>();

        if (_dangerUI != null && _bossNameUI != null)
        {
            _dangerOriginPosition = _dangerUI.anchoredPosition;
            _bossNameOriginPosition = _bossNameUI.anchoredPosition;
        }
        else
        {
            Debug.Log("¤¸µÊ");
        }
    }

    public void DangerAnimation(float dangerIdleTime)
    {
        if (_dangerUI == null || _bossNameUI == null) return;
        if (_seq != null)
        {
            _seq.Kill();
        }

        _dangerUI.anchoredPosition = _dangerOriginPosition;
        _bossNameUI.anchoredPosition = _bossNameOriginPosition;
        _upImageUI.anchoredPosition = Vector2.up * -130f;
        _downImageUI.anchoredPosition = Vector2.up * 130f;
        float x = Screen.currentResolution.width;

        _seq = DOTween.Sequence();
        _seq.Append(_dangerUI.DOAnchorPosX(x * 0.42f, 1.5f));
        _seq.Join(_bossNameUI.DOAnchorPosX(x * -0.42f, 1.5f));
        _seq.Join(_upImageUI.DOAnchorPosY(0f, 1f));
        _seq.Join(_downImageUI.DOAnchorPosY(0f, 1f));
        _seq.AppendInterval(dangerIdleTime);
        _seq.Append(_dangerUI.DOAnchorPosX(_dangerOriginPosition.x, 0.5f));
        _seq.Join(_bossNameUI.DOAnchorPosX(_bossNameOriginPosition.x, 0.5f));
        _seq.Join(_upImageUI.DOAnchorPosY(-130f, 0.5f));
        _seq.Join(_downImageUI.DOAnchorPosY(130f, 0.5f));
        //_seq.Join();
    }

    private void OnDestroy()
    {
        if (_seq != null)
        {
            _seq.Kill();
        }
    }
}
