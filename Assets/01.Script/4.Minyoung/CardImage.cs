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
    private UpgradeData upgradeData;

    private RectTransform rect;

    public TextMeshProUGUI _ablityNameText;

    public Image _profileImage;

    public TextMeshProUGUI _descTxt;

    public Button _upgradeBtn;
    private List<Image> edgeList = new();
    private List<Image> cornerList = new();

    private bool isFocusing = false;
    private bool rotationFlag = false;
    private float startAngleZ;
    private float angleZGoal = 0;
    private float time;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        startAngleZ = transform.eulerAngles.z;
        edgeList.AddRange(transform.Find("Edge").GetComponentsInChildren<Image>());
        cornerList.AddRange(transform.Find("Corner").GetComponentsInChildren<Image>());
    }
    public void SetData(UpgradeData data)
    {
        upgradeData = data;
        _profileImage.sprite = data.upgradeProfile;
        _descTxt.text = data.upgradeDesc;
        _ablityNameText.text = data.upgradeName;
        _upgradeBtn.onClick.RemoveAllListeners();
        _upgradeBtn.onClick.AddListener(() => UpgradeManager.Instance.Upgrade(data.upgradeType));
        foreach (Image img in edgeList)
        {
            img.color = data.color;
        }
        foreach (Image img in cornerList)
        {
            img.color = data.color * new Vector4(0.5f, 0.5f, 0.5f, 1);
        }
    }

    private void Update()
    {
        if (isFocusing)
        {
            angleZGoal = Mathf.Lerp(angleZGoal, Mathf.Sin(time * 3) * 5, Time.deltaTime * 2);
            time += Time.deltaTime * (rotationFlag ? 1 : -1);
        }
        else
        {
            angleZGoal = Mathf.Lerp(angleZGoal, 0, Time.deltaTime * 2);
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleZGoal + startAngleZ));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOScale(new Vector2(1.2f, 1.2f), 0.5f);
        rect.DOScaleX(1.2f, 0.3f);
        rect.DOScaleY(1.2f, 0.3f);
        isFocusing = true;
        rotationFlag = Random.Range(0, 2) == 0 ? true : false;
        time = 0;
        //강조효과

        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOScale(Vector2.one, 0.5f);
        isFocusing = false;

        Debug.Log("Exit");
    }
}
