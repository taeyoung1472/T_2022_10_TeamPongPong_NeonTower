using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour, IUserInterface
{
    public List<CardImage> cardsTrm;

    private Vector2 initPos = Vector2.zero;
    private CanvasGroup _canvasGroup = null;

    public bool isCanUpgrade = true;

    public UnityEvent OnOpenUI { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public UnityEvent OnCloseUI { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Material dissolveMat;

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
    public void OpenUI()
    {
        UIManager.Instance.IsDisplayContinue = false;
        Time.timeScale = 0f;

        Make();

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        #region 카드 내려오는 Sequence
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
        #endregion
    }

    public void CloseUI()
    {
        StartCoroutine(DissolveCard());
    }
    public void InitCardSet()
    {
        #region 카드 에니메이션 끝났을때 처리
        cardsTrm[0].transform.localPosition = new Vector3(-600, 1000, 0);
        cardsTrm[1].transform.localPosition = new Vector3(-0, 1000, 0);
        cardsTrm[2].transform.localPosition = new Vector3(600, 1000, 0);

        cardsTrm[0].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 7));
        cardsTrm[1].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        cardsTrm[2].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -7));

        foreach (var card in cardsTrm)
        {
            card.GetComponent<CanvasGroup>().alpha = 0.5f;
        }

        foreach (var card in cardsTrm)
        {
            card.GetComponent<Image>().material = null;
        }

        isCanUpgrade = true;
        #endregion
    }
    public void UpgradeCardEffect(GameObject selectedObjcet)
    {
        isCanUpgrade = false;
        //눌리면
        Sequence seq = DOTween.Sequence();

        #region 안선택된거 위로 올리기
        List<GameObject> noneSelectedCards = new List<GameObject>();
        foreach (var item in cardsTrm)
        {
            if (selectedObjcet != item.gameObject)
            {
                noneSelectedCards.Add(item.gameObject);
            }
            //item.GetComponent<Button>().interactable = true;
            float t = 0;
            foreach (var none in noneSelectedCards)
            {
                StartCoroutine(DissolveAndOut(t, none));
                t += 0.3f;
            }
        }
        #endregion

        seq.AppendInterval(0.6f).SetUpdate(true);

        seq.Append(selectedObjcet.transform.DOLocalMove(new Vector3(0, 100, 0), 0.44f)).SetUpdate(true); // 가운데로 모으고
        seq.Join(selectedObjcet.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.44f)).SetUpdate(true); // 회전 원위치

        seq.Append(selectedObjcet.transform.DOScale(1.25f, 0.5f));
        seq.Append(selectedObjcet.transform.DOScale(1f, 0.2f));

        seq.AppendInterval(0.2f).SetUpdate(true);

        seq.AppendCallback(() =>
        {
            selectedObjcet.GetComponent<Image>().material = dissolveMat;
            DoFade(0.485f, 0.57f, 0.5f, selectedObjcet.GetComponent<Image>().material);
        }); // 디졸브 시키고

        seq.Append(selectedObjcet.transform.DOLocalMove(new Vector3(0, -1000, 0), 0.76f)).SetUpdate(true); // 아레로 내리기

        seq.AppendCallback(() =>
        {
            InitCardSet();
        });

        #region 업그레이 겟수 지우기
        foreach (var card in cardsTrm)
        {
            RectTransform[] childList = card.parentCntTrm.GetComponentsInChildren<RectTransform>();
            foreach (RectTransform deleteChilds in childList)
            {
                if (deleteChilds == card.parentCntTrm)
                    continue;

                Destroy(deleteChilds.gameObject);
            }

        }
        #endregion

        Debug.Log($"{selectedObjcet.name} 카드가 눌림");
    }

    private IEnumerator DissolveAndOut(float time, GameObject none)
    {
        yield return new WaitForSecondsRealtime(time);

        none.GetComponent<Image>().material = dissolveMat;
        
        DoFade(0.485f, 0.57f, 0.5f, none.GetComponent<Image>().material);
        
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

    public void DoFade(float start, float dest, float time, Material dissolveMat)
    {
        DOTween.To(() => start, x => { start = x; dissolveMat.SetFloat("_Dissolve", start); }, dest, time).SetUpdate(true);
    }
}
