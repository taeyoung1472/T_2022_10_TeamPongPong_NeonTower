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

    [Header("焊胶")]
    [SerializeField] private Boss[] bossList;
    private int bossIdx = 0;

    [Header("[TMP]")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI waveCntText;

    [Header("[Audio]")]
    [SerializeField] private AudioClip floorChangeClip;

    private WaveUIManager _waveUIManager = null;

    public void Start()
    {
        backgrounds = FindObjectsOfType<Background>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        StartCoroutine(WaveSystem());

        _waveUIManager = WaveUIManager.Instance;
    }

    public void Update()
    {
        if(_waveUIManager != null && IsBossClear)
        {
            _waveUIManager.NextWaveTextSet(wavePerTime, waveTimer);
            waveTimer += Time.deltaTime;
        }
        else
        {
            _waveUIManager.NextWaveTextSet(0, 0);
        }
    }

    public IEnumerator WaveSystem()
    {
        while (true)
        {
            yield return new WaitUntil(() => waveTimer > wavePerTime - 5f);
            WaveUIManager.Instance.WaveCount(CurFloor, CurWave);

            yield return new WaitUntil(() => waveTimer > wavePerTime);

            waveTimer = 0;
            curWave++;
            curFloor = (curWave / 4) + 1;

            if (curWave % 4 == 0)
            {
                #region 焊胶 贸府
                isBossClear = false;
                Define.Instance.playerController.IsIdle = true;
                WaveUIManager.Instance.SetImage(CurFloor - 1, CurWave - 1);
                bossList[bossIdx].gameObject.SetActive(true);
                CameraManager.Instance.TargetingBossCameraAnimation(bossList[bossIdx], 5);
                EXPManager.Instance.IsCanLevelUp = false;
                EnemySpawner.Instance.IsCanSpawn = false;
                EnemySubject.Instance.NotifyObserver();
                Define.Instance.playerController.transform.position = new Vector3(0, 0, -7);
                StadiumManager.Instance.StadiumMatches[bossIdx].Active();
                BGMChanger.Instance.ActiveAudio(BGMType.Boss);

                yield return new WaitUntil(() => isBossClear);
                yield return new WaitForSeconds(0.5f);

                WaveUIManager.Instance.SetImage(CurFloor, CurWave);
                EnemySpawner.Instance.IsCanSpawn = true;
                StadiumManager.Instance.StadiumMatches[bossIdx].DeActive();
                BGMChanger.Instance.ActiveAudio(BGMType.Default);
                EXPManager.Instance.IsCanLevelUp = true;

                bossIdx++;
                #endregion
                EnemySubject.Instance.NotifyObserver();

                if (curFloor == 6)
                {
                    GameManager.Instance.LoadEnding();
                }

                foreach (Background bg in backgrounds) { bg.FloorChange(); }
                //EnemySubject.instance.NotifyObserver();
                CameraManager.Instance.CameraShake(1, 1, 4);

                #region Dotween 贸府
                Sequence seq = DOTween.Sequence();

                // Define.Instance.playerController.公利窃荐;
                seq.AppendCallback(() => EXPManager.Instance.IsCanLevelUp = false);
                seq.AppendCallback(() => enemySpawner.IsCanSpawn = false);
                seq.AppendInterval(waveChangeTime);
                seq.AppendCallback(() => EXPManager.Instance.IsCanLevelUp = true);
                seq.AppendCallback(() => enemySpawner.IsCanSpawn = true);
                seq.InsertCallback(3, () => AudioManager.PlayAudio(floorChangeClip));
                #endregion
            }
            else
            {
                #region Dotween 贸府
                waveCntText.SetText($"{CurWave % 4 + 1}");
                waveText.SetText($"Wave");
                AudioManager.PlayAudio(UISoundManager.Instance.data.waveChangeClip);

                Sequence seqWave = DOTween.Sequence();
                seqWave.Append(waveText.rectTransform.DOAnchorPosY(300, 1f));
                seqWave.Join(waveCntText.rectTransform.DOAnchorPosY(300, 1f));
                seqWave.AppendInterval(2f);
                //seqWave.Join(waveText.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f));
                //seqWave.Join(waveCntText.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f));

                //seqWave.AppendInterval(1f);

                //seqWave.Join(waveText.rectTransform.DOScale(new Vector3(1, 1f, 1f), 1f));
                //seqWave.Join(waveCntText.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 1f));
                seqWave.Join(waveText.rectTransform.DOShakeAnchorPos(1.5f, 50, 100));
                seqWave.Join(waveCntText.rectTransform.DOShakeAnchorPos(1.5f, 50, 100));

                seqWave.Append(waveText.rectTransform.DOAnchorPosY(700, 1f));
                seqWave.Join(waveCntText.rectTransform.DOAnchorPosY(700, 1f));
                #endregion

                WaveUIManager.Instance.SetImage(CurFloor, CurWave);
            }
        }
    }
}
