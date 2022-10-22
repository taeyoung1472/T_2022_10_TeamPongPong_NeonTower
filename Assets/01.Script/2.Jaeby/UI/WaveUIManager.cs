using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaveUIManager : MonoSingleTon<WaveUIManager>
{
    [SerializeField]
    private WaveCountdown _popupText = null;
    [SerializeField]
    private List<Image> _floorsImages = new List<Image>();
    [SerializeField]
    private List<Image> _waveImages = new List<Image>();
    [SerializeField]
    private List<Transform> _waveImagesParents = new List<Transform>();
    [SerializeField]
    private TextMeshProUGUI _floorText = null;
    [SerializeField]
    private TextMeshProUGUI _waveText = null;
    [SerializeField]
    private TextMeshProUGUI _nextWaveText = null;

    [SerializeField]
    private UIColorSet _floorIamgeColor;
    [SerializeField]
    private UIColorSet _waveImageColor;

    private int floorIdx = 0;
    private int lastFloor = 0;

    [System.Serializable]
    private struct UIColorSet
    {
        public Color disableColor;
        public Color enableColor;
    }

    private void Start()
    {
        for (int i = 0; i < _waveImages.Count; i++)
        {
            _waveImagesParents.Add(_waveImages[i].transform.parent);
        }
        SetImage(1, 0);
    }

    public void NextWaveTextSet(float wavePerTime, float waveTimer)
    {
        _nextWaveText.text = $":<#F37D00>{wavePerTime - waveTimer:0}</color> ÃÊ";
    }

    public void WaveCount(int floorCnt, int waveCnt)
    {
        _popupText.DoCount(() =>
        {
            SetImage(floorCnt, waveCnt);
        });
    }

    private void SetImage(int floorCnt, int waveCnt)
    {
        SetFloorImage(floorCnt);
        SetWaveImage(waveCnt);
    }

    private void SetFloorImage(int floorCnt)
    {
        if (floorCnt > _floorsImages.Count) return;
        for (int i = 1; i <= _floorsImages.Count; i++)
        {
            Color targetColor = Color.white;
            if (i <= floorCnt)
            {
                targetColor = _waveImageColor.enableColor;
            }
            else
            {
                targetColor = _floorIamgeColor.disableColor;

            }
            _floorsImages[i - 1].color = targetColor;
        }

        if (lastFloor != floorCnt)
        {
            lastFloor = floorCnt;
            Sequence seq = DOTween.Sequence();
            _floorsImages[floorIdx].transform.localScale = Vector3.zero;
            seq.Append(_floorsImages[floorIdx].transform.DOScale(1f, 1f));
            seq.Join(_floorsImages[floorIdx].DOColor(_waveImageColor.enableColor, 1f));
            seq.AppendCallback(() =>
            {
                floorIdx++;
            });
        }
         

        _floorText.SetText($"<#5B8AFF>{floorCnt}</color>Ãþ");
    }

    private void SetWaveImage(int waveCnt)
    {
        if (waveCnt > _waveImages.Count) return;
        Sequence seq = DOTween.Sequence();
        List<Transform> targets = new List<Transform>();

        for (int i = 0; i < _waveImages.Count; i++)
        {
            Color targetColor = Color.white;
            if (i <= waveCnt)
            {
                targetColor = _floorIamgeColor.enableColor;

                targets.Add(_waveImagesParents[i]);
            }
            else
            {
                targetColor = _waveImageColor.disableColor;
            }

            _waveImages[i].color = targetColor;
        }

        if (targets.Count > 0)
        {
            targets[0].localScale = Vector3.one * 1.25f;
            int count = 1;
            for (int i = 0; i < targets.Count; i++)
            {
                seq.Append(targets[i].DOScale(1f, 0.3f).SetEase(Ease.OutExpo));
                seq.Insert(0.3f * i, targets[i].GetChild(0).DOScale(1f, 0.3f).SetEase(Ease.OutExpo));
                seq.AppendCallback(() =>
                {
                    if (count < targets.Count)
                    {
                        targets[count].localScale = Vector3.one * 1.4f;
                        targets[count].GetChild(0).localScale = Vector3.one * 1.4f;
                    }
                    Debug.Log(count);
                    count++;
                });
            }
        }

        _waveText.SetText($"<#F37D00>{waveCnt % 4 + 1}</color> ¿þÀÌºê");
    }
}
