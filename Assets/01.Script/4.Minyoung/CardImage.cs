using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UpgradeUI upgradeUI;

    private UpgradeData upgradeData;

    private RectTransform rect;

    public TextMeshProUGUI _ablityNameText;

    public Image _profileImage;

    public TextMeshProUGUI _descTxt;

    public Button _upgradeBtn;

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
        _upgradeBtn.onClick.AddListener(() => { if (upgradeUI.isCanUpgrade) { UpgradeManager.Instance.Upgrade(data.upgradeType); } });
        _upgradeBtn.onClick.AddListener(() => { if (upgradeUI.isCanUpgrade) { upgradeUI.UpgradeCardEffect(gameObject); } });
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
        if (!upgradeUI.isCanUpgrade) return;
        Debug.Log("Enter");
        lightImage.material = changeCardMat;
        animator.Play("updateshiny");
        _profileImage.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetUpdate(true);
        rect.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).SetUpdate(true);
        AudioManager.PlayAudio(UISoundManager.Instance.data.cardOnMouseSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!upgradeUI.isCanUpgrade) return;
        lightImage.material = defaultCardMat;
        animator.Play("Idle");
        rect.DOScale(Vector3.one, 0.5f).SetUpdate(true);
        _profileImage.transform.DOScale(Vector3.one, 0.3f).SetUpdate(true);
        Debug.Log("Exit");
    }

    public void Init()
    {
        lightImage.material = defaultCardMat;
        animator.Play("Idle");
        rect.DOScale(Vector3.one, 0f).SetUpdate(true);
        _profileImage.transform.DOScale(Vector3.one, 0f).SetUpdate(true);
    }
}
