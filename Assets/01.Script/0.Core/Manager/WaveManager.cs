using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class WaveManager : MonoSingleTon<WaveManager>
{
    [Header("[Data]")]
    [SerializeField] private int wavePerTime = 40;
    [SerializeField] private int waveChangeTime = 8;
    private int curWave = 0;
    private int curFloor = 1;
    private float waveTimer = 0;
    private bool isBossClear = false;

    public int CurWave { get { return curWave; } }
    public bool IsBossClear { get { return isBossClear; } set { isBossClear = value; } }

    [Header("[Ref]")]
    private Background[] backgrounds;
    private EnemySpawner enemySpawner;

    [Header("[TMP]")]
    [SerializeField] private TextMeshProUGUI nextWaveText;
    [SerializeField] private TextMeshProUGUI floorText;
    [SerializeField] private TextMeshProUGUI waveText;

    [Header("[Audio]")]
    [SerializeField] private AudioClip floorChangeClip;

    public void Start()
    {
        backgrounds = FindObjectsOfType<Background>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        StartCoroutine(WaveSystem());
    }

    public void Update()
    {
        nextWaveText.text = $"다음 웨이브 까지 : {wavePerTime - waveTimer:0.0} 초";
        waveTimer += Time.deltaTime;
    }

    public IEnumerator WaveSystem()
    {
        while (true)
        {
            floorText.text = $"{curFloor} 층";
            yield return new WaitUntil(() => waveTimer > wavePerTime);

            waveTimer = 0;
            curWave++;
            curFloor = (curWave / 3) + 1;

            if (curWave % 4 == 0)
            {
                //isBossClear = false;
                //yield return new WaitUntil(() => isBossClear);
                EnemySubject.Instance.NotifyObserver();

                if (curFloor == 6)
                {
                    GameManager.Instance.LoadEnding();
                }

                foreach (Background bg in backgrounds) { bg.FloorChange(); }
                //EnemySubject.instance.NotifyObserver();
                CameraManager.Instance.CameraShake(1, 1, 4);

                #region Dotween 처리
                Sequence seq = DOTween.Sequence();

                // Define.Instance.playerController.무적함수;
                seq.AppendCallback(() => EXPManager.Instance.isCanLevelup = false);
                seq.AppendCallback(() => enemySpawner.IsCanSpawn = false);
                seq.AppendInterval(waveChangeTime);
                seq.AppendCallback(() => EXPManager.Instance.isCanLevelup = true);
                seq.AppendCallback(() => enemySpawner.IsCanSpawn = true);
                seq.InsertCallback(3, () => AudioManager.PlayAudio(floorChangeClip));
                #endregion

                floorText.text = $"{curFloor} 층";
            }
            else
            {
                #region Dotween 처리
                Sequence seqWave = DOTween.Sequence();
                seqWave.Append(waveText.rectTransform.DOAnchorPosY(300, 1f));
                seqWave.AppendInterval(2f);
                seqWave.Join(waveText.rectTransform.DOShakeAnchorPos(1.5f, 50, 100));
                seqWave.Append(waveText.rectTransform.DOAnchorPosY(700, 1f));
                #endregion
            }
        }
    }
}
