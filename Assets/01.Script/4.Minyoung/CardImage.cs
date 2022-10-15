using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;
public class CardImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private AbilitySO abilitySO;

    private RectTransform rect;

    public TextMeshProUGUI _ablityNameText;

    public Image _profileImage;

    public TextMeshProUGUI _descTxt;

    public Button _upgradeBtn;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void SetData(AbilitySO so)
    {
        abilitySO = so;
        _profileImage.sprite = so.sprite;
        _descTxt.text = so.decString;
        _ablityNameText.text = "���׷��̵� ����";
        _upgradeBtn.onClick.RemoveAllListeners();
        _upgradeBtn.onClick.AddListener(() => Debug.Log("����"));
        // Debug.Log("�µ���Ÿ �߽����");
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //eventData..DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5);
        rect.DOScale(new Vector2(1.2f, 1.2f), 0.5f);
        rect.DOScaleX(1.2f, 0.3f);
        rect.DOScaleY(1.2f, 0.3f);
        //����ȿ��

        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOScale(Vector2.one, 0.5f);
        Debug.Log("Exit");
    }
}
