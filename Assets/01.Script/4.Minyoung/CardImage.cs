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

    private Color baseColor;

    public Button _upgradeBtn;
    private List<Image> edgeList = new();
    private List<Image> cornerList = new();
    public Image line;


    private Sequence _seq = null;


    public Material defaultCardMat;
    public Material changeCardMat;
    public Image lightImage;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        upgradeUI = GameObject.Find("CardUpgrade").GetComponent<UpgradeUI>();
        //edgeList.AddRange(transform.Find("Edge").GetComponentsInChildren<Image>());
        //cornerList.AddRange(transform.Find("Corner").GetComponentsInChildren<Image>());
    }
    public void SetData(UpgradeData data)
    {
        upgradeData = data;
        _profileImage.sprite = data.upgradeProfile;
        _descTxt.text = data.upgradeDesc;
        _ablityNameText.text = data.upgradeName;
        _upgradeBtn.onClick.RemoveAllListeners();
        _upgradeBtn.onClick.AddListener(() => UpgradeManager.Instance.Upgrade(data.upgradeType));
        _upgradeBtn.onClick.AddListener(() => upgradeUI.UpgradeCardEffect(gameObject));
    }

    private void Start()
    {
    }
    private void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        lightImage.material = changeCardMat;
        rect.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        lightImage.material = defaultCardMat;

        rect.DOScale(Vector3.one, 0.5f).SetUpdate(true);
        Debug.Log("Exit");
    }
}
