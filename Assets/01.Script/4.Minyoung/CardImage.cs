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

    private Color baseColor;

    public Button _upgradeBtn;
    private List<Image> edgeList = new();
    private List<Image> cornerList = new();
    public Image line;

    private bool isFocusing = false;
    private bool rotationFlag = false;
    private float startAngleZ;
    private float angleZGoal = 0;
    private float time;

    public Material changematerial;
    public Material originmaterial;
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

            //img.color = data.color * new Vector4(0.5f, 0.5f, 0.5f, 1);

        }
        foreach (Image img in cornerList)
        {
            img.color = data.color * new Vector4(0.5f, 0.5f, 0.5f, 1);
        }
    }


    private void Update()
    {
        {
            //Time.timeScale = 1;
            //if (isFocusing)
            //{
            ////    angleZGoal = Mathf.Sin(time * 3) * 5 * Mathf.Rad2Deg;
            //  time += Time.deltaTime * (rotationFlag ? 1 : -1);
            //}
            //else
            //{
            //    angleZGoal = Mathf.Lerp(angleZGoal, 0, Time.deltaTime * 2);
            //}

            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleZGoal + startAngleZ));
        }


    }

    public void OnPointerEnter(PointerEventData eventData)

    {
        Debug.Log("Enter");
        foreach (Image img in cornerList)
        {
            img.color = upgradeData.color * new Vector4(1f, 1f, 1f, 1);
        }
        foreach (Image img in edgeList)
        {
            Color color = upgradeData.color;
            changematerial.SetColor("_EmmisionColor", color * 10f);
            Debug.Log(color);
            img.material = changematerial;

            //img.color = data.color * new Vector4(0.5f, 0.5f, 0.5f, 1);
        }
        line.material = changematerial;
        rect.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (Image img in cornerList)
        {
            img.color = upgradeData.color * new Vector4(0.5f, 0.5f, 0.5f, 1);
        }
        foreach (Image img in edgeList)
        {
            img.material = originmaterial;
            //img.color = data.color * new Vector4(0.5f, 0.5f, 0.5f, 1);
        }
        line.material = originmaterial;

        rect.DOScale(Vector3.one, 0.5f).SetUpdate(true);
        Debug.Log("Exit");
    }
}
