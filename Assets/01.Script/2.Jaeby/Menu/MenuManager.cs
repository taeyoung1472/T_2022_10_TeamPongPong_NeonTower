using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoSingleTon<MenuManager>
{
    [SerializeField]
    private Transform _startInitPosition = null;
    [SerializeField]
    private CinemachineVirtualCamera _vCam = null;

    [SerializeField]
    private GameObject[] _texts = null;

    [SerializeField]
    private RectTransform _startButton = null;
    [SerializeField]
    private RectTransform _tutorialButton = null;
    [SerializeField]
    private RectTransform _exitButton = null;

    [SerializeField]
    private GameObject _textParent = null;

    private bool _isClicked = false;
    public bool IsClicked { get => _isClicked; }

    [SerializeField]
    private AudioClip _middleClickClip = null;
    [SerializeField]
    private AudioClip _lightClickClip = null;
    [SerializeField]
    private Image _fadeUI = null;

    private void Start()
    {
        Time.timeScale = 1f;

        Glitch.GlitchManager.Instance.StartSceneValue();

        for (int i = 0; i < _texts.Length; i++)
        {
            Transform targetText = _texts[i].transform;

            Sequence seq = DOTween.Sequence();
            seq.Append(targetText.DOMoveZ(14f, 0.5f)).SetUpdate(true);
            seq.AppendInterval(0.25f).SetUpdate(true);
            seq.Append(targetText.DOMoveZ(15f, 0.5f)).SetUpdate(true);
            seq.AppendInterval(0.25f).SetUpdate(true);
            seq.SetLoops(-1);
        }
    }

    public void FadeButton()
    {
        Sequence seq = DOTween.Sequence();

        _startButton.DOKill();
        _tutorialButton.DOKill();
        _exitButton.DOKill();

        seq.Append(_startButton.DOAnchorPosX(-800f, 0.2f));
        seq.Append(_tutorialButton.DOAnchorPosX(-800f, 0.2f));
        seq.Append(_exitButton.DOAnchorPosX(-800f, 0.2f));
        seq.AppendCallback(() => StartInit());
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    public void StartInit()
    {
        //StartCoroutine(LightDown());

        Sequence seq = DOTween.Sequence();
        //seq.AppendInterval(0.2f * _lights.Length);
        seq.Append(_vCam.transform.DOMove(_startInitPosition.position, 1.5f));
        seq.AppendCallback(() =>
        {
            Glitch.GlitchManager.Instance.LoadGameCutScene();

            CameraManager.Instance.ZoomCamera(45f, 0.5f);
            _fadeUI.gameObject.SetActive(true);
            _fadeUI.DOFade(1f, 1f);
        });
        seq.AppendInterval(0.8f);
        seq.AppendCallback(() =>
        {
            Glitch.GlitchManager.Instance.StartSceneValue();
            SceneManager.LoadScene(1);
        });
    }

    public void GoTutorial()
    {
        Sequence seq = DOTween.Sequence();
        _fadeUI.gameObject.SetActive(true);
        seq.Append(_fadeUI.DOFade(1f, 1f));
        seq.AppendCallback(() =>
        {
            SceneManager.LoadScene(3);
        });
    }

    public void ExitInit()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_startButton.DOAnchorPosX(-800f, 0.2f));
        seq.Append(_tutorialButton.DOAnchorPosX(-800f, 0.2f));
        seq.Append(_exitButton.DOAnchorPosX(-800f, 0.2f));
        seq.AppendInterval(0.4f);
        seq.Append(_textParent.transform.DOMoveY(-14f, 0.5f));
        seq.AppendCallback(() =>
        {
            Application.Quit();
        });
    }

    public void IsClick()
    {
        _isClicked = true;
    }


    public void MiddleButtonClick()
    {
        AudioManager.PlayAudio(_middleClickClip);
    }
    public void LightButtonClick()
    {
        AudioManager.PlayAudio(_lightClickClip);
    }
}
