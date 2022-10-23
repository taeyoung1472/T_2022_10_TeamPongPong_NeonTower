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
    #region Ʃ��1
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
            TextPop("�� �ϼ̽��ϴ�!!");
            StartCoroutine(GoTutorialTwo());
            // Ŭ����
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
        TextPop("�׿� Ÿ���� ���� ���� ȯ���մϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop($"ĳ������ �⺻ ������ <#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>WASD</color>�� �մϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop("�̵��� �Ͽ� ��� ���� ��ƺ�����.");
        yield return new WaitForSeconds(1f);
        TextPop("3");
        yield return new WaitForSeconds(0.5f);
        TextPop("2");
        yield return new WaitForSeconds(0.5f);
        TextPop("1");
        yield return new WaitForSeconds(0.5f);
        TextPop($"<#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>����!</color>");

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

        TextPop("���������� �̵����ּ���");
    }

    private IEnumerator TutorialTwoStart()
    {
        for (int i = 0; i < _test1Boundarys.Length; i++)
        {
            _test1Boundarys[i].transform.DOMoveY(0f, 0.5f);
        }
        TextPop("�� �ϼ̽��ϴ�!");
        yield return new WaitForSeconds(1f);
        TextPop($"<#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>Shift</color> Ű�� ������ <#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>��ø�</color> ����� �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop($"��ô� ���� �� ����� �� �ִ� <#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>������ ��ų</color>�Դϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop("���� ���� Ƚ���� ���� �Ʒ��� UI���� Ȯ���Ͻ� �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop("�غ� �Ǽ̳���?");
        yield return new WaitForSeconds(1f);
        TextPop("3");
        yield return new WaitForSeconds(0.5f);
        TextPop("2");
        yield return new WaitForSeconds(0.5f);
        TextPop("1");
        yield return new WaitForSeconds(0.5f);
        TextPop("����!!");
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
            TextPop($"<#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>Ŭ���� !!!</color>");
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
        TextPop($"��û ����Ͻó׿� !!");
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
        TextPop($"������ �������ּ���");
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
        TextPop("�� �ϼ̽��ϴ�!");
        yield return new WaitForSeconds(1f);
        TextPop($"<#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>���콺 ����</color> Ű�� ������ <#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>�⺻������</color> �� �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop("�տ� �ִ� ǥ���� ����������.");
        yield return new WaitForSeconds(1.5f);
        TextPop("�غ� �Ǽ̳���?");
        yield return new WaitForSeconds(1f);
        TextPop("3");
        yield return new WaitForSeconds(0.5f);
        TextPop("2");
        yield return new WaitForSeconds(0.5f);
        TextPop("1");
        yield return new WaitForSeconds(0.5f);
        TextPop("����!!");
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
        TextPop("�� �ϼ̽��ϴ�!");
        yield return new WaitForSeconds(1f);
        TextPop("�� ���ӿ��� ���̺� �ý����� �ֽ��ϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop("������ ���� UI�� ���̳���?");
        yield return new WaitForSeconds(2f);
        TextPop("���� �ֱ⸶�� ���̺갡 �ö󰡰� �˴ϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop("4���̺긶�� ���� �ö󰡰� ������ �����ϰ� �˴ϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop("�׸��� ������ ���ο� ������ �����մϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop("�׽�Ʈ�� ���� �÷����ڽ��ϴ�.");
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
        TextPop($"�� �ϼ̽��ϴ� !!!");
        yield return new WaitForSeconds(3f);
        TextPop($"������ �Ʒø� ���ҽ��ϴ�.");
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
        TextPop($"������ ���ּ���");
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
        TextPop($"���� �Ǹ��մϴ� !!!!");
        yield return new WaitForSeconds(3f);
        TextPop($"�� ���ӿ��� ���������� �߰������ �ο��� �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop($"�׽�Ʈ������ ����ġ�� �帱�״� ���׷��̵带 �غ�����.");
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
        TextPop($"���ϼ̽��ϴ�. ���� ������ ������ �غ� �ǽ� �� ���׿�.");
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
