using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private Transform _firstTarget = null;
    [SerializeField]
    private Transform _secondTarget = null;
    [SerializeField]
    private Transform _thirdTarget = null;

    [SerializeField]
    private GameObject _player = null;
    [SerializeField]
    private TextMeshProUGUI _tutorialText = null;
    [SerializeField]
    private Transform _tutorialTextPos = null;
    [SerializeField]
    private Vector3 _initPos = Vector3.zero;
    #region 튜토1
    private int _amount = 0;
    #endregion

    private bool _tutorialStart = false;
    [SerializeField]
    private GameObject _colEffect = null;
    [SerializeField]
    private GameObject[] _test1Boundarys = null;
    [SerializeField]
    private GameObject[] _test2Boundarys = null;
    [SerializeField]
    private GameObject[] _test3Boundarys = null;
    [SerializeField]
    private GameObject _spawnEffectPrefab = null;
    [SerializeField]
    private Transform[] _enemyPos = null;

    private Sequence _seq = null;

    [SerializeField]
    private Color _impactColor = Color.white;

    [SerializeField]
    private GameObject[] _overlayCanvas = null;
    [SerializeField]
    private UIManager _uiManager = null;

    private float _speed = 0f;

    private void Start()
    {
        Time.timeScale = 1f;

        _overlayCanvas[2].SetActive(false);
        StartCoroutine(StartTutorial());
    }


    public void amountUp(Collider col)
    {
        if (_tutorialStart == false) return;

        Instantiate(_colEffect, col.transform.position, Quaternion.identity);
        col.gameObject.SetActive(false);

        _amount++;
        if (_amount >= 4)
        {
            TextPop("잘 하셨습니다!!");
            StartCoroutine(GoTutorialTwo());
            // 클리어
        }
    }

    public void TestTwoStart(Collider col)
    {
        StartCoroutine(TutorialTwoStart());
        col.gameObject.SetActive(false);
    }

    private void TextPop(string text)
    {
        if (_seq != null)
            _seq.Kill();

        _tutorialText.SetText(text);
        _tutorialText.rectTransform.anchoredPosition = _initPos;
        _seq = DOTween.Sequence();
        _seq.Append(_tutorialText.transform.DOLocalMoveY(400f, 0.5f));
        _seq.Append(_tutorialText.transform.DOShakePosition(150f));
    }

    private IEnumerator StartTutorial()
    {
        TextPop("네온 타워에 오신 것을 환영합니다.");
        yield return new WaitForSeconds(3f);
        TextPop($"캐릭터의 기본 조작은 <#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>WASD</color>로 합니다.");
        yield return new WaitForSeconds(3f);
        TextPop("이동을 하여 노란 공에 닿아보세요.");
        yield return new WaitForSeconds(1f);
        TextPop("3");
        yield return new WaitForSeconds(0.5f);
        TextPop("2");
        yield return new WaitForSeconds(0.5f);
        TextPop("1");
        yield return new WaitForSeconds(0.5f);
        TextPop($"<#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>시작!</color>");

        //_speed = Define.Instance.playerController.SpeedFixValue;
        _speed = 1f;
        _tutorialStart = true;
    }

    private IEnumerator GoTutorialTwo()
    {
        yield return new WaitForSeconds(1f);

        CameraManager.Instance.TargetingCameraAnimation(_firstTarget, 2f, 2f);
        Define.Instance.playerController.SpeedFixValue = 0f;

        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < _test1Boundarys.Length; i++)
        {
            _test1Boundarys[i].transform.DOMoveY(-2.05f, 1f);
        }
        yield return new WaitForSeconds(2.5f);
        Define.Instance.playerController.SpeedFixValue = _speed;

        TextPop("오른쪽으로 이동해주세요");
    }

    private IEnumerator TutorialTwoStart()
    {
        for (int i = 0; i < _test1Boundarys.Length; i++)
        {
            _test1Boundarys[i].transform.DOMoveY(0f, 0.5f);
        }
        TextPop("잘 하셨습니다!");
        yield return new WaitForSeconds(1f);
        TextPop($"<#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>Shift</color> 키를 눌러서 <#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>대시를</color> 사용할 수 있습니다.");
        yield return new WaitForSeconds(3f);
        TextPop($"대시는 여러 번 사용할 수 있는 <#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>충전형 스킬</color>입니다.");
        yield return new WaitForSeconds(3f);
        TextPop("남은 충전 횟수는 왼쪽 아래의 UI에서 확인하실 수 있습니다.");
        yield return new WaitForSeconds(3f);
        TextPop("준비 되셨나요?");
        yield return new WaitForSeconds(1f);
        TextPop("3");
        yield return new WaitForSeconds(0.5f);
        TextPop("2");
        yield return new WaitForSeconds(0.5f);
        TextPop("1");
        yield return new WaitForSeconds(0.5f);
        TextPop("시작!!");
        _dashObj[0].SetActive(true);
    }

    [SerializeField]
    private GameObject[] _dashObj = null;
    private int i = 0;


    public void NextDashObjectEnable()
    {
        i++;

        if (i == _dashObj.Length)
        {
            TextPop($"<#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>클리어 !!!</color>");
            StartCoroutine(Test2Clear());
            return;
        }

        _dashObj[i].SetActive(true);
    }
    public void DashObjectCollision(Collider col)
    {
        Instantiate(_colEffect, col.transform.position, Quaternion.identity);
        col.gameObject.SetActive(false);
    }

    private IEnumerator Test2Clear()
    {
        yield return new WaitForSeconds(1.5f);
        TextPop($"엄청 대단하시네요 !!");
        yield return new WaitForSeconds(1.5f);

        CameraManager.Instance.TargetingCameraAnimation(_secondTarget, 2f, 2f);
        Define.Instance.playerController.SpeedFixValue = 0f;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < _test2Boundarys.Length; i++)
        {
            _test2Boundarys[i].transform.DOMoveY(-2.05f, 1f);
        }
        yield return new WaitForSeconds(2.5f);
        Define.Instance.playerController.SpeedFixValue = _speed;
        TextPop($"밑으로 내려가주세요");
    }


    public void TestThreeStart(Collider col)
    {
        StartCoroutine(TutorialThreeStart());
        col.gameObject.SetActive(false);
    }
    private IEnumerator TutorialThreeStart()
    {
        for (int i = 0; i < _test2Boundarys.Length; i++)
        {
            _test2Boundarys[i].transform.DOMoveY(0f, 0.5f);
        }
        TextPop("잘 하셨습니다!");
        yield return new WaitForSeconds(1f);
        TextPop($"<#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>마우스 왼쪽</color> 키를 눌러서 <#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>기본공격을</color> 할 수 있습니다.");
        yield return new WaitForSeconds(3f);
        TextPop("앞에 있는 표적을 때려보세요.");
        yield return new WaitForSeconds(1.5f);
        TextPop("준비 되셨나요?");
        yield return new WaitForSeconds(1f);
        TextPop("3");
        yield return new WaitForSeconds(0.5f);
        TextPop("2");
        yield return new WaitForSeconds(0.5f);
        TextPop("1");
        yield return new WaitForSeconds(0.5f);
        TextPop("시작!!");
        for (int i = 0; i < _enemyPos.Length - 2; i++)
        {
            GameObject effect = Instantiate(_spawnEffectPrefab, _enemyPos[i].transform.position, Quaternion.identity);
            effect.transform.localScale = Vector3.one * 0.05f;
            yield return new WaitForSeconds(1f);
            Enemy enemy = PoolManager.Instance.Pop(PoolType.ComonEnemy) as Enemy;
            enemy.Init(_enemyPos[i].position, _player);
            enemy.OnDeath.AddListener(EnemyDie);
            yield return new WaitForSeconds(1.5f);
        }
    }

    int count = 0;
    public void EnemyDie()
    {
        count++;
        if (count == _enemyPos.Length - 2)
        {
            count = 0;
            Test3Clear();
        }
    }
    public void EnemyDie2()
    {
        count++;
        if (count == _enemyPos.Length - 2)
        {
            count = 0;
            RealTest3Clear();
        }
    }

    public void Test3Clear()
    {
        StartCoroutine(SecondTutorialCoroutine());
    }

    public void RealTest3Clear()
    {
        StartCoroutine(Test3ClearCoroutine());
    }

    private IEnumerator SecondTutorialCoroutine()
    {
        TextPop("잘 하셨습니다!");
        yield return new WaitForSeconds(1f);
        TextPop("이 게임에는 웨이브 시스템이 있습니다.");
        yield return new WaitForSeconds(3f);
        TextPop("오른쪽 위의 UI가 보이나요?");
        yield return new WaitForSeconds(2f);
        TextPop("일정 주기마다 웨이브가 올라가게 됩니다.");
        yield return new WaitForSeconds(3f);
        TextPop("4웨이브마다 층이 올라가고 보스가 등장하게 됩니다.");
        yield return new WaitForSeconds(3f);
        TextPop("그리고 층마다 새로운 적들이 등장합니다.");
        yield return new WaitForSeconds(3f);
        TextPop("테스트로 층을 올려보겠습니다.");
        yield return new WaitForSeconds(2f);
        TextPop("");
        WaveUIManager.Instance.WaveCount(2, 1);
        yield return new WaitForSeconds(5f);
        WaveUIManager.Instance.SetImage(2, 4);
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < _enemyPos.Length - 2; i++)
        {
            GameObject effect = Instantiate(_spawnEffectPrefab, _enemyPos[i].transform.position, Quaternion.identity);
            effect.transform.localScale = Vector3.one * 0.05f;
            yield return new WaitForSeconds(1f);
            Enemy enemy = PoolManager.Instance.Pop(PoolType.DashEnemy) as Enemy;
            enemy.Init(_enemyPos[i].position, _player);
            enemy.OnDeath.AddListener(EnemyDie2);
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator Test3ClearCoroutine()
    {
        TextPop($"잘 하셨습니다 !!!");
        yield return new WaitForSeconds(3f);
        TextPop($"마지막 훈련만 남았습니다.");
        yield return new WaitForSeconds(2f);

        CameraManager.Instance.TargetingCameraAnimation(_thirdTarget, 2f, 2f);
        Define.Instance.playerController.SpeedFixValue = 0f;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < _test3Boundarys.Length; i++)
        {
            _test3Boundarys[i].transform.DOMoveY(-2.05f, 1f);
        }
        yield return new WaitForSeconds(2.5f);
        Define.Instance.playerController.SpeedFixValue = _speed;
        TextPop($"옆으로 가주세요");
    }


    public void Test4Start(Collider col)
    {
        StartCoroutine(Test4Coroutine());
        col.gameObject.SetActive(false);
    }

    private IEnumerator Test4Coroutine()
    {
        for (int i = 0; i < _test3Boundarys.Length; i++)
        {
            _test3Boundarys[i].transform.DOMoveY(0f, 0.5f);
        }
        TextPop($"정말 훌륭합니다 !!!!");
        yield return new WaitForSeconds(3f);
        TextPop($"이 게임에는 레벨업으로 추가기능을 부여할 수 있습니다.");
        yield return new WaitForSeconds(3f);
        TextPop($"테스트용으로 경험치를 드릴테니 업그레이드를 해보세요.");
        yield return new WaitForSeconds(3f);
        for(int i = 0; i < _overlayCanvas.Length; i++)
        {
            _overlayCanvas[i].SetActive(false);
        }
        EXPManager.Instance.AddExp(40);
    }

    [SerializeField]
    private Image _fadeImage = null;

    public void Ending()
    {
        StartCoroutine(EndingCoroutine());

    }
    private IEnumerator EndingCoroutine()
    {
        TextPop($"잘하셨습니다. 이제 게임을 시작할 준비가 되신 것 같네요.");
        _uiManager.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        DOTween.KillAll();
        _overlayCanvas[2].SetActive(true);
        _fadeImage.gameObject.SetActive(true);
        _fadeImage.DOFade(1f, 3f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }

}
