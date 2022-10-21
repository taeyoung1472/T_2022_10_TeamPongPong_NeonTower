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
    private bool isBossClear = true;

    public int CurWave { get { return curWave; } }
    public int CurFloor { get { return curFloor; } }
    public bool IsBossClear { get { return isBossClear; } set { isBossClear = value; } }

    [Header("[Ref]")]
    private Background[] backgrounds;
    private EnemySpawner enemySpawner;

    [Header("보스")]
    [SerializeField] private Boss[] bossList;
    private int bossIdx = 0;

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
        BGMChanger.Instance.ActiveAudio(BGMType.Default);
    }

    public void Update()
    {
        if (!isBossClear) return;
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
            curFloor = (curWave / 4) + 1;

            if (curWave % 4 == 0)
            {
                #region 보스 처리
                isBossClear = false;
                bossList[bossIdx].gameObject.SetActive(true);
                CameraManager.Instance.TargetingBossCameraAnimation(bossList[bossIdx], 5);
                EXPManager.Instance.isCanLevelup = false;
                EnemySubject.Instance.NotifyObserver();
                Define.Instance.playerController.transform.position = new Vector3(0, 0, -15);
                StadiumManager.Instance.StadiumMatches[bossIdx].Active();
                bossIdx++;
                BGMChanger.Instance.ActiveAudio(BGMType.Boss);

                yield return new WaitUntil(() => isBossClear);
                BGMChanger.Instance.ActiveAudio(BGMType.Default);
                #endregion
                EXPManager.Instance.isCanLevelup = true;
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
