using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public GameObject HpLineFolder;
    public List<Image> hpImageList;

    [SerializeField] private Image hpImage;
    void Start()
    {
        CreateHPImage();
        UpdateHpUI();
    }

    void Update()
    {
        //hpBar.value = currentHP / maxHP;
    }
    public void CreateHPImage()
    {
        for (int i = 0; i < maxHP; i++)
        {
            Debug.Log("�ο��̿���");
            Image hpObj = Instantiate(hpImage, transform.position, Quaternion.identity);
            hpObj.transform.SetParent(HpLineFolder.transform);
            hpImageList.Add(hpObj);
            Debug.Log(hpObj);
            Debug.Log(hpImageList); //�̰� ����
        }
        Debug.Log("�ȳ��ϼ���");
    }
    public void UpdateHpUI()
    {
        int a = HpLineFolder.transform.childCount;
        Debug.Log(a);

        //maxhp��ŭ image�� �����ϰ�

        for (int i = 0; i < a; i++)//���� hp��ŭ �Ѷ� 
        {
            if (i >= currentHP)
            {
                hpImageList[i].transform.gameObject.SetActive(false);
            }
            else
            {

                hpImageList[i].transform.gameObject.SetActive(true);
            }
        }
    }
    // 3 9
    // ����ġ�� �ڽ��� ������ �����
    //�̰� hp�� �ٲ� �� �ڽ��� ���� hp��ŭ foreach���� �¿�ƽ�긦 �ϴ°���
}
