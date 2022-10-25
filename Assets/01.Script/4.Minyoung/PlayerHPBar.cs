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
            Debug.Log("민영이에요");
            Image hpObj = Instantiate(hpImage, transform.position, Quaternion.identity);
            hpObj.transform.SetParent(HpLineFolder.transform);
            hpImageList.Add(hpObj);
            Debug.Log(hpObj);
            Debug.Log(hpImageList); //이게 찍혀
        }
        Debug.Log("안녕하세요");
    }
    public void UpdateHpUI()
    {
        int a = HpLineFolder.transform.childCount;
        Debug.Log(a);

        //maxhp만큼 image를 생성하고

        for (int i = 0; i < a; i++)//현재 hp만큼 켜라 
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
    // 포이치로 자식의 갯수를 세어놔
    //이게 hp가 바뀔때 그 자신의 현재 hp만큼 foreach에서 셋엣틱브를 하는거지
}
