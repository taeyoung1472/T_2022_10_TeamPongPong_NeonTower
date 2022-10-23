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
    private Image _fadeUI = null;

    [SerializeField]
    private GameObject[] _menuObjects = null;
    [SerializeField]
    private GameObject[] _startObjects = null;

    private void Start()
    {
        Time.timeScale = 1f;

        Glitch.GlitchManager.Instance.StartSceneValue();

        GoFirst();

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

    public void GoFirst()
    {
        _isClicked = false;
        for (int i = 0; i < _startObjects.Length; i++)
        {
            _startObjects[i].SetActive(false);
        }
        for (int i = 0; i < _menuObjects.Length; i++)
        {
            _menuObjects[i].SetActive(true);
        }
    }

    public void GoSecond()
    {
        _isClicked = false;
        for (int i = 0; i < _menuObjects.Length; i++)
        {
            _menuObjects[i].SetActive(false);
        }
        for (int i = 0; i < _startObjects.Length; i++)
        {
            _startObjects[i].SetActive(true);
        }
    }

    public void FadeButton(int value)
    {
        Sequence seq = DOTween.Sequence();

        _startButton.DOKill();
        _tutorialButton.DOKill();
        _exitButton.DOKill();

        for(int i = 0; i <_startObjects.Length; i ++)
        {
            seq.Append(_startObjects[i].GetComponent<RectTransform>().DOAnchorPosX(-800f, 0.2f));
        }
        //seq.Append(_startButton.DOAnchorPosX(-800f, 0.2f));
        //seq.Append(_tutorialButton.DOAnchorPosX(-800f, 0.2f));
        //seq.Append(_exitButton.DOAnchorPosX(-800f, 0.2f));
        seq.AppendCallback(() => StartInit(value));
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    public void StartInit(int value)
    {
        PlayerPrefs.SetInt("resurrectionCount", value);
        //StartCoroutine(LightDown());

        Sequence seq = DOTween.Sequence();
        //seq.AppendInterval(0.2f * _lights.Length);
        seq.Append(_vCam.transform.DOMove(_startInitPosition.position, 1.5f));
        seq.AppendCallback(() =>
        {
            Glitch.GlitchManager.Instance.LoadGameCutScene();

            CameraManager.Instance.ZoomCamera(45f, 0.5f);
            _fadeUI.gameObject.SetActive(true);
            _fadeUI.DOFade(1f, 2f);
        });
        seq.AppendInterval(1.5f);

        seq.AppendCallback(() =>
        {
            //Glitch.GlitchManager.Instance.StartSceneValue();
            SceneManager.LoadScene(1);
        });
    }

    public void GoTutorial()
    {
        Sequence seq = DOTween.Sequence();
        _fadeUI.gameObject.SetActive(true);
        seq.Append(_fadeUI.DOFade(1f, 3f));
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
}
