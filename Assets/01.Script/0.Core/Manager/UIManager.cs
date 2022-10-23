using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleTon<UIManager>
{
    [Header("[UI Canvas]")]
    [SerializeField] private GameObject _escUI = null;
    [SerializeField] private GameObject _continueUI = null;

    [SerializeField]
    private AudioClip ClickClip = null;
    public bool isActiveContinue;
    private bool isDisplayContinue = true;
    public bool IsDisplayContinue { get { return isDisplayContinue; } set { isDisplayContinue = value; } }

    Stack<IUserInterface> _popupStack = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_popupStack.Count == 0 && !isActiveContinue)
            {
                ActiveUI(_escUI);
            }
        }
    }

    public void ActiveUI(GameObject targetUI)
    {
        IsDisplayContinue = true;

        if (_popupStack.Count > 0)
        {
            _popupStack.Peek().CloseUI();
        }

        _popupStack.Push(targetUI.GetComponent<IUserInterface>());
        _popupStack.Peek().OpenUI();
    }

    public void DeActiveUI()
    {
        if (_popupStack.Count > 1)
        {
            _popupStack.Pop().CloseUI();
            _popupStack.Peek().OpenUI();
        }
        else if (_popupStack.Count == 1)
        {
            _popupStack.Pop().CloseUI();
            if (isDisplayContinue)
            {
                _continueUI.GetComponent<IUserInterface>().OpenUI();
            }
            isActiveContinue = true;
        }
        else if (_popupStack.Count == 0)
        {
            if (isDisplayContinue)
            {
                _continueUI.GetComponent<IUserInterface>().CloseUI();
            }
        }
    }

    #region ClickSound
    public void ClickSundPlay()
    {
        PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(ClickClip);
    }
    #endregion
}
