using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomCardManager : MonoBehaviour
{
    public List<AbilitySO> abilitySOs = new List<AbilitySO>();

    public CardImage cardImagePrefab;

    public Transform cardOneTrm;
    public Transform cardTwoTrm;
    public Transform cardThreeTrm;

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
    public void Make()
    {
        for (int i = 0; i < cardOneTrm.childCount; i++)
        {
            Destroy(cardOneTrm.GetChild(i).gameObject);
        }

        AbilitySO[] so = RandomPickSO();
        for (int i = 0; i < so.Length; i++)
        {
            CardImage slot = Instantiate(cardImagePrefab, cardOneTrm) as CardImage;

            slot.SetData(so[i]);
            RectTransform rectTrm = slot.GetComponent<RectTransform>();
            rectTrm.anchoredPosition = new Vector2(0, 100) * i;

            rectTrm.sizeDelta = new Vector2(100, 100);
            Debug.Log("Àß¸¸µé¾îÁü?" );

        }
    }
    public AbilitySO[] RandomPickSO()
    {
        List<AbilitySO> soList = new List<AbilitySO>();
        int rand = 0;
        rand = Random.Range(0, abilitySOs.Count);
        soList.Add(abilitySOs[rand]);
        Debug.Log("·£´ýÃâ·Â" + soList.ToArray());
        return soList.ToArray();

    }
  
}
