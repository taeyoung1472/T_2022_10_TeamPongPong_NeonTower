using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class CardImage : MonoBehaviour, IPointerEnterHandler
{
    public AbilitySO abilitySO;
    public AbilitySO asdf;

    public Image _image;

    private TextMeshPro _desTxt;

    public Text s;

    public Button _btn;

    public void SetData(AbilitySO so)
    {
        abilitySO = so;
        _image.sprite = so.sprite;
        s.text = so.decString;
        Debug.Log("�µ���Ÿ �߽����");
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //����ȿ��
        Debug.Log("Sexymin");
    }
}
