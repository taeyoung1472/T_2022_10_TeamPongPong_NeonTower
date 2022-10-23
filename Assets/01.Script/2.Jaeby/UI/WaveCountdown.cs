using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCountdown : MonoBehaviour
{
    private Sequence _seq = null;
    private TextMeshProUGUI _text = null;
    [SerializeField]
    private AudioClip _countDownClip = null;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.SetText("");
    }

    public void DoCount(Action Callback)
    {
        if (_seq != null)
            _seq.Kill();
        _text.transform.localScale = Vector3.one;
        _seq = DOTween.Sequence();

        _text.SetText("5");
        int count = 4;

        for (int i = 5; i > 0; i--)
        {
            _seq.Append(_text.transform.DOShakePosition(1f, 10, 15, 90, false, true));
            _seq.Join(_text.transform.DOScale(1f, 1f));
            _seq.AppendCallback(() =>
            {
                AudioManager.PlayAudio(_countDownClip);
                _text.SetText(count.ToString());
                count--;
                _text.transform.localScale = Vector3.one * 1.5f;
            });
        }
        _seq.AppendCallback(() =>
        {
            Callback?.Invoke();
            _text.SetText("");
        });
    }
}
