using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;
using Microsoft.Cci;

public class CardImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UpgradeUI upgradeUI;

    private UpgradeData upgradeData;

    private RectTransform rect;

    public TextMeshProUGUI _ablityNameText;

    public Image _profileImage;

    public TextMeshProUGUI _descTxt;

    public Button _upgradeBtn;

    private Sequence _seq = null;

    public Material defaultCardMat;
    public Material changeCardMat;
    public Image lightImage;

    public Animator animator;
    public Transform parentCntTrm;
    public GameObject cntImagePrefab;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void SetData(UpgradeData data)
    {
        upgradeData = data;
        _profileImage.sprite = data.upgradeProfile;
        _descTxt.text = data.upgradeDesc;
        _ablityNameText.text = data.upgradeName;
        CreateUpgradeCntImage(data);
        _upgradeBtn.onClick.RemoveAllListeners();
        //_upgradeBtn.onClick.AddListener(() => FindObjectOfType<UpgradeUI>().endUpgrade = ()=> UpgradeManager.Instance.Upgrade(data.upgradeType));
        _upgradeBtn.onClick.AddListener(() => UpgradeManager.Instance.Upgrade(data.upgradeType));
        _upgradeBtn.onClick.AddListener(() => upgradeUI.UpgradeCardEffect(gameObject));
    }
    public void CreateUpgradeCntImage(UpgradeData data)
    {
        int upCnt = UpgradeManager.Instance.GetUpgradeCount(data.upgradeType);
        for (int i = 0; i < (int)data.upgradeAbleCount; i++)
        {
            
            GameObject cntImage = Instantiate(cntImagePrefab, parentCntTrm.transform);
            RectTransform rectTrm = cntImage.GetComponent<RectTransform>();

            if (i < upCnt)
            {
                rectTrm.GetComponent<Image>().color = Color.yellow;
            }

            rectTrm.anchoredPosition = new Vector2(15, 35) * i;
            rectTrm.sizeDelta = new Vector2(15, 35);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        lightImage.material = changeCardMat;
        animator.Play("updateshiny");
        _profileImage.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetUpdate(true);
        rect.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        lightImage.material = defaultCardMat;
        animator.Play("Idle");
        rect.DOScale(Vector3.one, 0.5f).SetUpdate(true);
        _profileImage.transform.DOScale(Vector3.one, 0.3f).SetUpdate(true);
        Debug.Log("Exit");
    }
}
