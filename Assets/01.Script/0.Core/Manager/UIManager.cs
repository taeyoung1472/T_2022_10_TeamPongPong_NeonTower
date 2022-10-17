using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UIManager : MonoSingleTon<UIManager>
{
    [Header("[UI Canvas]")]
    [SerializeField] private GameObject _escUI = null;
    [SerializeField] private GameObject _continueUI = null;
    [SerializeField] private GameObject _upgradeUI = null;

    [SerializeField]
    private AudioClip ClickClip = null;
    [HideInInspector] public bool isActiveContinue;

    [Header("HPUI °ü·Ã")]
    [SerializeField]
    private Color _damagedColor = Color.white;
    [SerializeField]
    private Color _normalColor = Color.white;
    [SerializeField]
    private TextMeshProUGUI _hpText = null;

    Stack<IUserInterface> _popupStack = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_popupStack.Count == 0 && !isActiveContinue)
            {
                ActiveUI(_escUI);
            }
        }
    }

    public void ActiveUI(GameObject targetUI)
    {
        if(_popupStack.Count > 0)
        {
            _popupStack.Peek().CloseUI();
        }

        _popupStack.Push(targetUI.GetComponent<IUserInterface>());
        _popupStack.Peek().OpenUI();
    }

    public void DeActiveUI()
    {
        if(_popupStack.Count > 1)
        {
            _popupStack.Pop().CloseUI();
            _popupStack.Peek().OpenUI();
        }
        else if(_popupStack.Count == 1)
        {
            _popupStack.Pop().CloseUI();
            _continueUI.GetComponent<IUserInterface>().OpenUI();
            isActiveContinue = true;
        }
        else if(_popupStack.Count == 0)
        {
            _continueUI.GetComponent<IUserInterface>().CloseUI();
        }
    }

    public void SetHpUI(int curHp, int maxHp)
    {
        string str = "";

        for (int i = maxHp; i > 0; i--)
        {
            string colorCode = curHp >= i ? ColorUtility.ToHtmlStringRGBA(_normalColor) : ColorUtility.ToHtmlStringRGBA(_damagedColor);
            print($"I : {i}, color : {colorCode}");
            str += $"<#{colorCode}>¡Ü</color>";
        }

        _hpText.text = str;
    }

    #region ClickSound
    public void ClickSundPlay()
    {
        PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(ClickClip);
    }
    #endregion
}
