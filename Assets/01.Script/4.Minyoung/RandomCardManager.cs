using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class RandomCardManager : MonoBehaviour
{
    public List<AbilitySO> abilitySOs = new List<AbilitySO>();

    public CardImage cardOneTrm;
    public CardImage cardTwoTrm;
    public CardImage cardThreeTrm;

    private void Awake()
    {
     
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Make();
        }
    }

    //public void Make()
    //{
    //    //for (int i = 0; i < cardOneTrm.childCount; i++)
    //    //{
    //    //    Destroy(cardOneTrm.GetChild(i).gameObject);
    //    //}  
    //    //for (int i = 0; i < cardTwoTrm.childCount; i++)
    //    //{
    //    //    Destroy(cardTwoTrm.GetChild(i).gameObject);
    //    //} 
    //    //for (int i = 0; i < cardThreeTrm.childCount; i++)
    //    //{
    //    //    Destroy(cardThreeTrm.GetChild(i).gameObject);
    //    //}


    //    AbilitySO[] so = RandomPickSO();
    //    for (int i = 0; i < so.Length; i++)
    //    {
    //        //CardImage slot = Instantiate(cardImagePrefab, cardOneTrm) as CardImage;

    //        //slot.SetData(so[i]);
    //        //RectTransform rectTrm = slot.GetComponent<RectTransform>();
    //        //rectTrm.anchoredPosition = new Vector2(0, 100) * i;

    //        //rectTrm.sizeDelta = new Vector2(100, 100);
    //      // Debug.Log("잘만들어짐?" );
    //    }
    //    so = RandomPickSO();
    //    /*for (int i = 0; i < so.Length; i++)
    //    {
    //        //CardImage slot = Instantiate(cardImagePrefab, cardTwoTrm) as CardImage;

    //        //slot.SetData(so[i]);
    //        //RectTransform rectTrm = slot.GetComponent<RectTransform>();
    //        //rectTrm.anchoredPosition = new Vector2(0, 100) * i;

    //        //rectTrm.sizeDelta = new Vector2(100, 100);
    //        //Debug.Log("잘만들어짐두번째?");
    //    }*/
    //    so = RandomPickSO();
    //    for (int i = 0; i < so.Length; i++)
    //    {
    //        //CardImage slot = Instantiate(cardImagePrefab, cardThreeTrm) as CardImage;

    //        //slot.SetData(so[i]);
    //        //RectTransform rectTrm = slot.GetComponent<RectTransform>();
    //        //rectTrm.anchoredPosition = new Vector2(0, 100) * i;

    //        //rectTrm.sizeDelta = new Vector2(100, 100);
    //       // Debug.Log("잘만들어짐세번째?");
    //    }
    //}
    //public AbilitySO[] RandomPickSO()
    //{
    //    /*while (soList.Contains(soList[rand]) == false)
    //    {
    //        rand = Random.Range(0, abilitySOs.Count);
    //    }*/
    //    List<AbilitySO> soList = new List<AbilitySO>();
    //    int rand = 0;
    //    //rand = Random.Range(0, abilitySOs.Count);
    //    //soList.Add(abilitySOs[rand]);

    //    for (int i = 0; i < 3; i++)
    //    {
    //        rand = Random.Range(0, abilitySOs.Count);
    //        //soList.RemoveAt(rand);
    //        if (soList.Contains(abilitySOs[rand]))//중복
    //        {
    //            i--;
    //            continue;
    //        }
    //        else
    //        {
    //            //리스트에 넣어
    //            soList.Add(abilitySOs[rand]);
    //        }
    //    }

    //    //soList.Add(abilitySOs[rand]);
    //    // Debug.Log("랜덤출력" + rand + soList.ToArray());
    //    return soList.ToArray();

    //}
    
    public void Make()
    {
        AbilitySO[] so = RandomPickSO();
        cardOneTrm.SetData(so[0]);
        cardTwoTrm.SetData(so[1]);
        cardThreeTrm.SetData(so[2]);
    }
    public AbilitySO[] RandomPickSO()
    {
        List<AbilitySO> soList = new List<AbilitySO>();
        int rand = 0;

        for (int i = 0; i < 3; i++)
        {
            rand = Random.Range(0, abilitySOs.Count);
            if (soList.Contains(abilitySOs[rand]))//중복
            {
                i--;
                continue;
            }
            else
            {
                //리스트에 넣어
                soList.Add(abilitySOs[rand]);
            }
        }
        return soList.ToArray();
    }
}
