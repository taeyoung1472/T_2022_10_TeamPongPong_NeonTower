using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public class OpenCloseUI : MonoBehaviour, IUserInterface
{
    public UnityEvent OnOpenUI { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public UnityEvent OnCloseUI { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private Vector3 _originPos = Vector3.zero;

    private void Start()
    {
        _originPos = new Vector3(0f, -Screen.height, 0f);
        transform.localPosition = _originPos;
    }


    public void CloseUI()
    {

    }

    public void OpenUI()
    {
        Time.timeScale = 0f;
        transform.DOLocalMoveY(0f, 0.5f).SetUpdate(true);
    }
}
