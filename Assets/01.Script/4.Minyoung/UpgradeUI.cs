using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class UpgradeUI : MonoBehaviour, IUserInterface
{
    public List<CardImage> cardsTrm;

    public List<GameObject> pointLights;

    private Sequence _seq = null;

    private Vector2 initPos = Vector2.zero;
    private CanvasGroup _canvasGroup = null;

    public UnityEvent OnOpenUI { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public UnityEvent OnCloseUI { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    [SerializeField] private GameObject upgradeUI;

    public Material dissolveMat;
    public Material originMat;

    public GameObject oneLight;
    public GameObject twoLight;
    public GameObject threeLight;

    //public Action endUpgrade;
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        initPos = new Vector2(0, Screen.height);
        transform.localPosition = initPos;
    }

    public void Make()
    {
        UpgradeData[] so = UpgradeManager.Instance.GetRandDataList();
        cardsTrm[0].SetData(so[0]);
        cardsTrm[1].SetData(so[1]);
        cardsTrm[2].SetData(so[2]);
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
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(Vector3.zero, 0.5f)).SetUpdate(true);
        seq.AppendCallback(() =>
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        });
        seq.Append(cardsTrm[0].transform.DOLocalMove(new Vector3(-600, 30, 0), 0.6f).SetEase(Ease.OutBounce)).SetUpdate(true);
        seq.Insert(0.6f, cardsTrm[0].GetComponent<CanvasGroup>().DOFade(1f, 0.3f)).SetUpdate(true);

        seq.Insert(0.8f, cardsTrm[1].transform.DOLocalMove(new Vector3(0, 100, 0), 0.6f).SetEase(Ease.OutBounce)).SetUpdate(true);
        seq.Insert(0.9f, cardsTrm[1].GetComponent<CanvasGroup>().DOFade(1f, 0.3f)).SetUpdate(true);

        seq.Insert(1.3f, cardsTrm[2].transform.DOLocalMove(new Vector3(600, 30, 0), 0.6f).SetEase(Ease.OutBounce)).SetUpdate(true);
        seq.Insert(1.4f, cardsTrm[2].GetComponent<CanvasGroup>().DOFade(1f, 0.3f)).SetUpdate(true);
    }

    public void CloseUI()
    {
        if (_seq != null)
            _seq.Kill();

        _seq = DOTween.Sequence();

        StartCoroutine(DissolveCard());
    }
    public void InitCardSet()
    {
        foreach (var card in cardsTrm)
        {
            card.GetComponent<Image>().material = originMat;
            card.transform.Find("DescArea").GetComponent<Image>().material = originMat;
        }
        cardsTrm[0].transform.localPosition = new Vector3(-600, 1000, 0);
        cardsTrm[1].transform.localPosition = new Vector3(-0, 1000, 0);
        cardsTrm[2].transform.localPosition = new Vector3(600, 1000, 0);

        cardsTrm[0].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 7));
        cardsTrm[1].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        cardsTrm[2].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -7));


        cardsTrm[0].transform.GetComponent<CanvasGroup>().alpha = 0.5f;
        cardsTrm[1].transform.GetComponent<CanvasGroup>().alpha = 0.5f;
        cardsTrm[2].transform.GetComponent<CanvasGroup>().alpha = 0.5f;

        dissolveMat.SetFloat("_Dissolve", 0);
    }
    public void UpgradeCardEffect(GameObject selectedObjcet)
    {
        Sequence seq = DOTween.Sequence();

        List<GameObject> noneSelectedCards = new List<GameObject>();
        foreach (var item in cardsTrm)
        {
            if (selectedObjcet != item.gameObject)
            {
                noneSelectedCards.Add(item.gameObject);
            }
            float t = 0;
            foreach (var none in noneSelectedCards)
            {
                StartCoroutine(DissolveAndOut(t, none, selectedObjcet));
                t += 0.3f;
            }
        }

        seq.AppendInterval(0.6f).SetUpdate(true);

        seq.Append(selectedObjcet.transform.DOLocalMove(new Vector3(0, 100, 0), 0.44f)).SetUpdate(true);
        seq.Join(selectedObjcet.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.44f)).SetUpdate(true);

        seq.AppendInterval(1f).SetUpdate(true);

        seq.AppendCallback(() =>
        {
            selectedObjcet.GetComponent<Image>().material = dissolveMat;
            selectedObjcet.transform.Find("DescArea").GetComponent<Image>().material = dissolveMat;
            DoFade(0.5f, 0.75f, 1f);
        });

        seq.Append(selectedObjcet.transform.DOLocalMove(new Vector3(0, -1000, 0), 0.76f)).SetUpdate(true);

        seq.AppendCallback(() =>
        {
            InitCardSet();
        });

        foreach (var card in cardsTrm)
        {
            RectTransform[] childList = card.parentCntTrm.GetComponentsInChildren<RectTransform>();
            foreach (RectTransform deleteChilds in childList)
            {
                if (deleteChilds == card.parentCntTrm)
                    continue;

                Debug.Log(deleteChilds.gameObject.name);
                Destroy(deleteChilds.gameObject);
            }

        }

        Debug.Log($"{selectedObjcet.name} 카드가 눌림");
    }


    private IEnumerator DissolveAndOut(float time, GameObject none, GameObject selectedObjcet)
    {
        yield return new WaitForSecondsRealtime(time);
        none.GetComponent<Image>().material = dissolveMat;
        none.transform.Find("DescArea").GetComponent<Image>().material = dissolveMat;


         DoFade(0.5f, 0.75f, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        none.transform.DOLocalMoveY(1000f, 0.34f).SetUpdate(true);
    }
    public IEnumerator DissolveCard()
    {
        yield return new WaitForSecondsRealtime(3f);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(initPos, 0.3f)).SetUpdate(true);
        seq.AppendCallback(() => Time.timeScale = 1);
    }

    void DoFade(float start, float dest, float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", start, "to", dest, "time", time, "onupdatetarget", gameObject, "onupdate", "TweenOnUpdate", "oncomplte",
            "TweenOnComplte", "easetype", iTween.EaseType.easeInOutCubic
            ));
    }
    void TweenOnUpdate(float value)
    {
        dissolveMat.SetFloat("_Dissolve", value);
    }
    void TweenOnComplte()
    {
        for (int i = 0; i < 3; i++)
        {

            cardsTrm[i].GetComponent<Image>().material = originMat;
        }

    }
}
