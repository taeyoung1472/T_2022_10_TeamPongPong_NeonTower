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

    [SerializeField] private GameObject upgradeUI;

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
            UIManager.Instance.ActiveUI(upgradeUI);
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
        _seq.Append(cardOneTrm.transform.DOLocalMove(new Vector3(-600, 30, 0), 0.6f).SetEase(Ease.OutBounce));
        _seq.Insert(0.6f, cardOneTrm.GetComponent<CanvasGroup>().DOFade(1f, 0.3f));

        _seq.Insert(0.8f, cardTwoTrm.transform.DOLocalMove(new Vector3(0, 100, 0), 0.6f).SetEase(Ease.OutBounce));
        _seq.Insert(0.9f, cardTwoTrm.GetComponent<CanvasGroup>().DOFade(1f, 0.3f));

        _seq.Insert(1.3f, cardThreeTrm.transform.DOLocalMove(new Vector3(600, 30, 0), 0.6f).SetEase(Ease.OutBounce));
        _seq.Insert(1.4f, cardThreeTrm.GetComponent<CanvasGroup>().DOFade(1f, 0.3f));
    }

    public void CloseUI()
    {
        if (_seq != null)
            _seq.Kill();
        Debug.Log("´ÙÈû");
        ///CloseCard();
        ///
        _seq = DOTween.Sequence();
        
        _seq.Append(cardOneTrm.transform.DORotate(new Vector3(0f, Random.Range(-8f, 8f), 0f), 1f));
        _seq.Join(cardTwoTrm.transform.DORotate(new Vector3(0f, Random.Range(-8f, 8f), 0f), 1f));
        _seq.Join(cardThreeTrm.transform.DORotate(new Vector3(0f, Random.Range(-8f, 8f), 0f), 1f));

        _seq.Insert(2f, cardOneTrm.transform.DOMove(new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), Random.Range(-4f, 4f)), 2f));
        _seq.Insert(2f, cardTwoTrm.transform.DOMove(new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), Random.Range(-4f, 4f)), 2f));
        _seq.Insert(2f, cardThreeTrm.transform.DOMove(new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), Random.Range(-4f, 4f)), 2f));

        //_seq.Append(cardOneTrm.transform.DOMove(new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), Random.Range(-4f, 4f)), 2f));
        //_seq.Append(cardTwoTrm.transform.DOMove(new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), Random.Range(-4f, 4f)), 2f));
        //_seq.Append(cardThreeTrm.transform.DOMove(new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), Random.Range(-4f, 4f)), 2f));

        //_seq.Append(transform.DOLocalMove(initPos, 0.3f)).SetUpdate(true);
        CloseCard();
        //InitCardSet();
        Time.timeScale = 1f;
    }
    public void InitCardSet()
    {
        cardOneTrm.transform.localPosition = new Vector3(-600, 1000, 0);
        cardTwoTrm.transform.localPosition = new Vector3(-0, 1000, 0);
        cardThreeTrm.transform.localPosition = new Vector3(600, 1000, 0);

        cardOneTrm.transform.GetComponent<CanvasGroup>().alpha = 0.5f;
        cardTwoTrm.transform.GetComponent<CanvasGroup>().alpha = 0.5f;
        cardThreeTrm.transform.GetComponent<CanvasGroup>().alpha = 0.5f;
    }
    public void CloseCard()
    {
        cardOneTrm.transform.DORotate(new Vector3(0f, Random.Range(-8f, 8f), 0f), 3f);
        cardTwoTrm.transform.DORotate(new Vector3(0f, Random.Range(-8f, 8f), 0f), 3f);
        cardThreeTrm.transform.DORotate(new Vector3(0f, Random.Range(-8f, 8f), 0f), 3f);

        cardOneTrm.transform.DORotate(new Vector3(0f, Random.Range(-8f, 8f), 0f), 3f);

    }
}
