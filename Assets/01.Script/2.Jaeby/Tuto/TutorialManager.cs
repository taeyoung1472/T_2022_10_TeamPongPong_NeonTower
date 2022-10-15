using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _testBoss = null;
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

    private void Start()
    {
        Time.timeScale = 1f;

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
        _tutorialStart = true;
    }

    private IEnumerator GoTutorialTwo()
    {
        yield return new WaitForSeconds(1f);
        TextPop("���������� �̵����ּ���");
        for (int i = 0; i < _test1Boundarys.Length; i++)
        {
            _test1Boundarys[i].SetActive(false);
        }
    }

    private IEnumerator TutorialTwoStart()
    {
        TextPop("�� �ϼ̽��ϴ�!");
        yield return new WaitForSeconds(1f);
        TextPop($"<#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>Shift</color> Ű�� ������ <#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>��ø�</color> ����� �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop($"��ô� ���� �� ����� �� �ִ� <#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>������ ��ų</color>�Դϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop("���� ���� Ƚ���� ������ �Ʒ��� UI���� Ȯ���Ͻ� �� �ֽ��ϴ�.");
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
        yield return new WaitForSeconds(3f);
        TextPop($"������ �������ּ���");
        for (int i = 0; i < _test2Boundarys.Length; i++)
        {
            _test2Boundarys[i].SetActive(false);
        }
    }


    public void TestThreeStart(Collider col)
    {
        StartCoroutine(TutorialThreeStart());
        col.gameObject.SetActive(false);
    }
    private IEnumerator TutorialThreeStart()
    {
        TextPop("�� �ϼ̽��ϴ�!");
        yield return new WaitForSeconds(1f);
        TextPop($"<#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>���콺 ����</color> Ű�� ������ <#{ColorUtility.ToHtmlStringRGBA(_impactColor)}>�⺻������</color> �� �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop("�տ� �ִ� ǥ���� ����������.");
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
        for (int i = 0; i < _enemyPos.Length; i++)
        {
            GameObject effect = Instantiate(_spawnEffectPrefab, _enemyPos[i].transform.position, Quaternion.identity);
            effect.transform.localScale = Vector3.one * 0.05f;
            yield return new WaitForSeconds(1f);
            Enemy enemy = PoolManager.Instance.Pop(PoolType.ComonEnemy) as Enemy;
            enemy.Init(_enemyPos[i].position, _player);
            if (i == _enemyPos.Length - 1)
                enemy.OnDeath.AddListener(Test3Clear);
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void Test3Clear()
    {
        StartCoroutine(Test3ClearCoroutine());
    }

    private IEnumerator Test3ClearCoroutine()
    {
        TextPop($"�� �ϼ̽��ϴ� !!!");
        yield return new WaitForSeconds(3f);
        TextPop($"������ �Ʒø� ���ҽ��ϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop($"������ ���ּ���");

        for (int i = 0; i < _test3Boundarys.Length; i++)
        {
            _test3Boundarys[i].SetActive(false);
        }
    }


    public void Test4Start(Collider col)
    {
        StartCoroutine(Test4Coroutine());
        col.gameObject.SetActive(false);
    }

    private IEnumerator Test4Coroutine()
    {
        TextPop($"���� �Ǹ��մϴ� !!!!");
        yield return new WaitForSeconds(3f);
        TextPop($"�� ���ӿ��� ���������� �߰������ �ο��� �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(3f);
        TextPop($"�׽�Ʈ������ ����ġ�� �帱�״� ���׷��̵带 �غ�����.");
        yield return new WaitForSeconds(3f);
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
        EXPManager.Instance.AddExp();
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
        yield return new WaitForSeconds(3f);
        _fadeImage.gameObject.SetActive(true);
        _fadeImage.DOFade(1f, 3f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            GameObject obj = Instantiate(_testBoss, new Vector3(-9.78f,0,9.25f), Quaternion.identity);
            CameraManager.Instance.TargetingBossCameraAnimation(obj.GetComponent<Boss>(), 5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            DamagePopup.PopupDamage(transform.position, 10);
        }
    }
}
