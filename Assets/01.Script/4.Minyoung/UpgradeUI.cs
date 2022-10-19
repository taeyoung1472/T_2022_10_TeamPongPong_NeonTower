using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeUI : MonoBehaviour, IUserInterface
{
    public CardImage cardOneTrm;
    public CardImage cardTwoTrm;
    public CardImage cardThreeTrm;

    private Sequence _seq = null;

    private Vector2 initPos = Vector2.zero;
    private CanvasGroup _canvasGroup = null;

    public UnityEvent OnOpenUI { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public UnityEvent OnCloseUI { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        initPos = new Vector2(0, Screen.height);
        transform.localPosition = initPos;
    }

    public void Make()
    {
        UpgradeData[] so = UpgradeManager.Instance.GetRandDataList();
        cardOneTrm.SetData(so[0]);
        cardTwoTrm.SetData(so[1]);
        cardThreeTrm.SetData(so[2]);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            OpenUI();
        }
    }
    public void OpenUI()
    {
        UIManager.Instance.IsDisplayContinue = false;
        Time.timeScale = 0f;

        Make();

        if (_seq != null)
            _seq.Kill();

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOLocalMove(Vector3.zero, 0.5f)).SetUpdate(true);
        _seq.AppendCallback(() =>
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        });
    }

    public void CloseUI()
    {
        if (_seq != null)
            _seq.Kill();

        CloseCard();
        _seq = DOTween.Sequence();
       // _seq.Append(transform.DOLocalMove(initPos, 0.3f)).SetUpdate(true);
        Time.timeScale = 1f;
    }
    public void CloseCard()
    {
       // cardOneTrm.transform.DOMove()
        cardOneTrm.transform.DORotate(new Vector3(0f, Random.Range(-8f, 8f), 0f), 3f);
        }
    }
