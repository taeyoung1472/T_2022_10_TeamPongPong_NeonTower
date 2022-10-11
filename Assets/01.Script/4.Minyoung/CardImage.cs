using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class CardImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private AbilitySO abilitySO;

    public TextMeshProUGUI ablityNameText;

    public Image profileImage;

    public TextMeshProUGUI _descTxt;

    public Button _btn;

    public void SetData(AbilitySO so)
    {
        abilitySO = so;
        profileImage.sprite = so.sprite;
        _descTxt.text = so.decString;
        ablityNameText.text = "���׷��̵� ����";
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(() => Debug.Log("����"));
       // Debug.Log("�µ���Ÿ �߽����");
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //����ȿ��
        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
    }
}
