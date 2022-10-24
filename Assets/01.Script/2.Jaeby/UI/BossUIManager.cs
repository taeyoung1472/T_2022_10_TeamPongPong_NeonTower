using DG.Tweening;
using System.Collections;
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
    private GameObject _fireImage = null;
    private TextMeshProUGUI _bossHPText = null;

    private Boss _currentBoss = null;

    private Vector3 _dangerOriginPosition = Vector3.zero;
    private Vector3 _bossNameOriginPosition = Vector3.zero;

    private Sequence _seq = null;

    private Sequence _popupSeq = null;
    private TextMeshProUGUI _popupText = null;
    private Vector3 _initPos = Vector3.zero;

    int _popupWeight = -1;

    private bool _isSliderAnimation = false;
    public bool IsSliderAnimation => _isSliderAnimation;

    private void Start()
    {
        Transform perent = GameObject.Find("BossCanvas").transform;
        _dangerUI = perent?.Find("BossDangerText").GetComponent<RectTransform>();
        _bossNameUI = perent?.Find("BossDangerNameText").GetComponent<RectTransform>();
        _upImageUI = perent?.Find("UpImage").GetComponent<RectTransform>();
        _downImageUI = perent?.Find("DownImage").GetComponent<RectTransform>();

        _bossHpSlider = perent?.Find("BossHPSlider").GetComponent<Slider>();
        _fireImage = perent?.Find("FireImage").gameObject;
        _bossImage = perent?.Find("BossImage").GetComponent<Image>();
        _bossNameText = perent?.Find("BossNameText").GetComponent<TextMeshProUGUI>();
        _bossHPText = _bossHpSlider.transform.Find("BossHPText").GetComponent<TextMeshProUGUI>();
        _popupText = perent?.Find("BossPopupText").GetComponent<TextMeshProUGUI>();

        _popupText.gameObject.SetActive(true);
        if (_dangerUI != null && _bossNameUI != null)
        {
            _dangerOriginPosition = _dangerUI.anchoredPosition;
            _bossNameOriginPosition = _bossNameUI.anchoredPosition;
            _initPos = _popupText.rectTransform.anchoredPosition;
            _initPos.x = 0f;
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

        _currentBoss.OnDeathEvent.AddListener(BossDieEvent);
        _bossHpSlider.gameObject.SetActive(true);
        _bossImage.gameObject.SetActive(true);
        _fireImage.SetActive(true);
        _bossNameText.gameObject.SetActive(true);

        _bossHPText?.SetText($"0 / {Instance._currentBoss.Data.maxHp}");
        _bossNameText?.SetText(_currentBoss.Data.bossName);

        _bossHpSlider.maxValue = _currentBoss.Data.maxHp;
        _bossHpSlider.minValue = 0f;
        _bossHpSlider.value = _bossHpSlider.minValue;
        _isSliderAnimation = true;
        DOTween.To(() => _bossHpSlider.value, x =>
        {
            _bossHpSlider.value = x;
            _bossHPText?.SetText($"{_bossHpSlider.value:0.0} / {Instance._currentBoss.Data.maxHp}");
        }, _bossHpSlider.maxValue, 1.5f)
            .OnComplete(() => _isSliderAnimation = false);
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
            _popupWeight = -1;
        }

        AudioManager.PlayAudioRandPitch(UISoundManager.Instance.data.tickClip);

        _popupWeight = weight;
        _popupText.SetText(text);
        _popupText.rectTransform.anchoredPosition = _initPos;
        _seq = DOTween.Sequence();
        _seq.Append(_popupText.rectTransform.DOAnchorPosY(-65f, 0.5f));
        _seq.Append(_popupText.transform.DOShakePosition(time));
        _seq.Append(_popupText.rectTransform.DOAnchorPosY(_initPos.y, 0.5f));
        _seq.AppendCallback(() =>
        {
            _popupWeight = -1;
        });
    }

    public static void BossDamaged()
    {
        if (Instance._currentBoss == null) return;
        if (Instance.IsSliderAnimation == false)
        {
            Instance._bossHpSlider.value = (float)Instance._currentBoss.CurHp;
            Instance._bossHPText?.SetText($"{Mathf.Clamp(Instance._currentBoss.CurHp, 0, 5000):0.0} / {Instance._currentBoss.Data.maxHp}");
        }
    }

    public void ExitBoss()
    {
        Destroy(_currentBoss.gameObject);
        _currentBoss = null;
        _popupWeight = -1;
        if (_bossHpSlider == null || _bossImage == null) return;
        _bossHpSlider.value = 0f;
        _bossNameText?.SetText("");

        _bossHpSlider.gameObject.SetActive(false);
        _bossHpSlider.value = 0f;
        _bossImage.gameObject.SetActive(false);
        _bossNameText.gameObject.SetActive(false);
        _fireImage.SetActive(false);
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

    public void BossDieEvent()
    {
        StartCoroutine(BossDieEventCoroutine());
    }

    private IEnumerator BossDieEventCoroutine()
    {
        Time.timeScale = 0.2f;
        CameraManager.Instance.TargetingCameraAnimation(_currentBoss.transform, 3f, 1f);
        _currentBoss.Animator.Play("Die");
        _currentBoss.Animator.Update(0);
        yield return new WaitUntil(() => _currentBoss.Animator.GetCurrentAnimatorStateInfo(0).IsName("Die") == false);

        Time.timeScale = 1f;
        ExitBoss();
        yield return new WaitForSeconds(1f);
        if (_popupSeq != null)
        {
            _popupSeq.Kill();
            _popupText.rectTransform.anchoredPosition = _initPos;
        }
    }

    private void OnDestroy()
    {
        if (_seq != null)
        {
            _seq.Kill();
        }
    }
}
