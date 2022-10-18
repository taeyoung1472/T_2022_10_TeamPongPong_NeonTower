using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : MonoSingleTon<BossUIManager>
{
    private RectTransform _dangerUI = null;
    private RectTransform _bossNameUI = null;
    private RectTransform _upImageUI = null;
    private RectTransform _downImageUI = null;

    private Slider _bossHpSlider = null;
    private Image _bossImage = null;
    private TextMeshProUGUI _bossNameText = null;
    private TextMeshProUGUI _bossHPText = null;

    private Boss _currentBoss = null;

    private Vector3 _dangerOriginPosition = Vector3.zero;
    private Vector3 _bossNameOriginPosition = Vector3.zero;

    private Sequence _seq = null;

    private Sequence _popupSeq = null;
    private TextMeshProUGUI _popupText = null;
    private Vector3 _initPos = Vector3.zero;

    int _popupWeight = 0;

    private void Start()
    {
        Transform perent = GameObject.Find("BossCanvas").transform;
        _dangerUI = perent?.GetChild(0).GetComponent<RectTransform>();
        _bossNameUI = perent?.GetChild(1).GetComponent<RectTransform>();
        _upImageUI = perent?.GetChild(2).GetComponent<RectTransform>();
        _downImageUI = perent?.GetChild(3).GetComponent<RectTransform>();

        _bossHpSlider = perent?.GetChild(4).GetComponent<Slider>();
        _bossImage = perent?.GetChild(5).Find("Image").GetComponent<Image>();
        _bossNameText = perent?.GetChild(6).GetComponent<TextMeshProUGUI>();
        _bossHPText = _bossHpSlider.transform.Find("BossHPText").GetComponent<TextMeshProUGUI>();
        _popupText = perent?.GetChild(7).GetComponent<TextMeshProUGUI>();

        if (_dangerUI != null && _bossNameUI != null)
        {
            _dangerOriginPosition = _dangerUI.anchoredPosition;
            _bossNameOriginPosition = _bossNameUI.anchoredPosition;
            _initPos = _popupText.rectTransform.anchoredPosition;
        }
        else
        {
            Debug.Log("¤¸µÊ");
        }
    }

    public void SetBoss(Boss boss)
    {
        _currentBoss = boss;
        if (_currentBoss == null || _bossHpSlider == null || _bossImage == null) return;

        _currentBoss.OnDeathEvent.AddListener(ExitBoss);
        _bossHpSlider.gameObject.SetActive(true);
        _bossImage.transform.parent.gameObject.SetActive(true);
        _bossNameText.gameObject.SetActive(true);

        _bossHpSlider.value = _currentBoss.CurHp / (float)_currentBoss.Data.maxHp;
        _bossHPText?.SetText($"{Instance._currentBoss.CurHp} / {Instance._currentBoss.Data.maxHp}");
        _bossImage.sprite = _currentBoss.Data.bossProfile;
        _bossNameText?.SetText(_currentBoss.Data.bossName);
    }

    /// <summary>
    /// º¸½º Àü¿ë ÆË¾÷ ÅØ½ºÆ® !!
    /// </summary>
    /// <param name="text"></param>
    /// <param name="time"></param>
    /// <param name="weight"></param>
    public void BossPopupText(string text, float time, int weight)
    {
        if (_popupText == null) return;
        if (weight < _popupWeight) return;
        if (_popupSeq != null)
        {
            _popupSeq.Kill();
            weight = 0;
        }

        _popupWeight = weight;
        _popupText.SetText(text);
        _popupText.rectTransform.anchoredPosition = _initPos;
        _seq = DOTween.Sequence();
        _seq.Append(_popupText.rectTransform.DOAnchorPosY(-65f, 0.5f));
        _seq.Append(_popupText.transform.DOShakePosition(time));
        _seq.Append(_popupText.rectTransform.DOAnchorPosY(_initPos.y, 0.5f));
        _seq.AppendCallback(() =>
        {
            weight = 0;
        });
    }

    public static void BossDamaged()
    {
        if (Instance._currentBoss == null) return;
        Instance._bossHpSlider.value = Instance._currentBoss.CurHp / (float)Instance._currentBoss.Data.maxHp;
        Instance._bossHPText?.SetText($"{Instance._currentBoss.CurHp} / {Instance._currentBoss.Data.maxHp}");
    }

    public void ExitBoss()
    {
        _currentBoss = null;
        if (_bossHpSlider == null || _bossImage == null) return;
        _bossHpSlider.value = 0f;
        _bossImage.sprite = null;
        _bossNameText?.SetText("");

        _bossHpSlider.gameObject.SetActive(false);
        _bossImage.transform.parent.gameObject.SetActive(false);
        _bossNameText.gameObject.SetActive(false);
    }

    public void DangerAnimation(float dangerIdleTime, Boss boss)
    {
        if (_dangerUI == null || _bossNameUI == null) return;
        if (_seq != null)
        {
            _seq.Kill();
        }

        _dangerUI.anchoredPosition = _dangerOriginPosition;
        _bossNameUI.anchoredPosition = _bossNameOriginPosition;
        _bossNameUI.GetComponent<TextMeshProUGUI>()?.SetText(boss.Data.bossName);
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
        _seq.AppendInterval(0.2f);
        _seq.AppendCallback(() =>
        {
            SetBoss(boss);
        });
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
