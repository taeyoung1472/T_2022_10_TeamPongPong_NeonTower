using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public Slider hpBar;
    public float maxHP;
    public float currentHP;
    public GameObject HpLineFolder;
    public float unitHp = 1f;
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = currentHP / maxHP;
    }
    public void GetHpBoost()
    {
        maxHP += 1f;
        float scaleX = (5f / unitHp) / (maxHP / unitHp);
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);
        foreach (Transform child in HpLineFolder.transform)
        {
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        }
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
    }
    public void MinusHpBoost()

        {
        maxHP -= 1f;
        float scaleX = (5f / unitHp) / (maxHP / unitHp);
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);
        foreach (Transform child in HpLineFolder.transform)
        {
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        }
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);

    }
}
