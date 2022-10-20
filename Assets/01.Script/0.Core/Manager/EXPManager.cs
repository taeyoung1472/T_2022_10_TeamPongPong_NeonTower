using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EXPManager : MonoSingleTon<EXPManager>
{
    //public SlotMachineMg slotMachine;
    //public UIManagerHan han;
    //public SlotMachineManager slotMachineManager;
    public bool isCanLevelup = true;
    [SerializeField] private GameObject upgradeUI;

    [SerializeField] private GameObject levelUpEffect;

    public int[] expTable;
    int curExp = 0;
    int curLevel = 0;

    public TextMeshProUGUI expPercentText;
    public TextMeshProUGUI levelText;


    public Image expSlider;

    [ContextMenu("Init")]
    public void Init()
    {
        expTable = new int[40];
        int dif = 3;
        for (int i = 0; i < expTable.Length; i++)
        {
            if (i == 39)
            {
                expTable[39] = 999999;
                return;
            }
            expTable[i] = dif;
            dif += 3;
        }
    }

    public void Awake()
    {
        LevelUdateText();
    }

    public void Update()
    {
        ExpPercent();
    }

    public void AddExp(int amount = 1)
    {
        curExp += amount;
        if (curExp >= expTable[curLevel] && isCanLevelup)
        {
            curExp = 0;
            curLevel++;
            LevelUdateText();
            StartCoroutine(RaycastCotroll());

            UIManager.Instance.ActiveUI(upgradeUI);

            Sequence seq = DOTween.Sequence();

            GameObject e = null;

            seq.AppendInterval(0.25f);

            seq.AppendCallback(() => e = Instantiate(levelUpEffect, Define.Instance.playerController.transform.position, Quaternion.identity));

            seq.AppendCallback(() => Destroy(e, 6));

            //Define.Instance.controller.GodMode(2f);
        }
    }
    public void LevelUdateText()
    {
        levelText.text = ($"LV.{curLevel + 1}");
        expSlider.fillAmount = 0f;
    }
    IEnumerator RaycastCotroll()
    {
        yield return new WaitForSecondsRealtime(4);
        //slotMachine.gardImage.raycastTarget = false;
    }

    public void ExpPercent()
    {

        float expPer = ((float)curExp / (float)expTable[curLevel]) * 100;
        Sequence sequence = DOTween.Sequence();
        float a = expPer / 100f;

        DOTween.To(() => expSlider.fillAmount, x => expSlider.fillAmount = x, expPer / 100f, 0.3f).SetUpdate(true);
        expPercentText.text = ($"{Mathf.Ceil(expPer)}");
    }
}
