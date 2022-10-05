using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class WaveManager : MonoSingleTon<WaveManager>
{
    #region Legarcy
    /*[SerializeField] private float waveTime = 40;
    private float curWaveTime;
    public int curWave;
    public int realcurWave = 1;
    public int curFloor = 1;

    [SerializeField] private Background background;

    [Header("[TMP]")]
    [SerializeField] private TextMeshProUGUI nextWaveText;
    [SerializeField] private TextMeshProUGUI floorText;

    //[SerializeField] private TextMeshProUGUI wave;
    //[SerializeField] private TextMeshProUGUI floor;
    */
    /*private void Start()
    {
        wave.text = $"Wave 1-{curFloor}";
        wave.gameObject.SetActive(true);
        StartCoroutine(DisableText());
    }
    public void Update()
    {
        curWaveTime += Time.deltaTime;

        if (curWaveTime > waveTime)
        {
            curWaveTime = 0;
            curWave++;
            curFloor++;
            if (curWave % 3 == 0)
            {
                realcurWave++;
                curFloor = 1;
                background.FloorChange();
                EnemySubject.instance.NotifyObserver();
                CameraManager.Instance.CameraShake(1, 1, 4);
                //����ٰ� ���̺� �ö󰡴� �ؽ�Ʈ
                //wave.text = $"Wave {curWave}-{curFloor}";
                //wave.gameObject.SetActive(true);

                Sequence seq = DOTween.Sequence();
                //Define.Instance.controller.GodMode(8);
                seq.AppendCallback(() => EXPManager.Instance.isCanLevelup = false);
                seq.AppendCallback(() => EnemyGenerator.Instance.isCanGenerated = false);
                seq.AppendInterval(8);
                seq.AppendCallback(() => EXPManager.Instance.isCanLevelup = true);
                seq.AppendCallback(() => EnemyGenerator.Instance.isCanGenerated = true);
            }

            wave.text = $"Wave {realcurWave}-{curFloor}";
            wave.gameObject.SetActive(true);
            StartCoroutine(DisableText());

            DisplayFloor();
        }

        nextWaveText.text = $"���� ���̺� ���� {waveTime - curWaveTime}��";
    }

    IEnumerator DisableText()
    {
        yield return new WaitForSeconds(3f);
        wave.gameObject.SetActive(false);
    }

    private void DisplayFloor()
    {
        string str = "";
        str += $"{curWave / 3 + 1} Floor";
        if (curWave / 3 + 1 == 6)
        {
            UIManager.Instance.GameEnding();
        }
        floorText.text = str;
    }

    public int GetFloor()
    {
        return curWave / 3 + 1;
    }*/
    #endregion
    [Header("[Data]")]
    [SerializeField] private int wavePerTime = 30;
    [SerializeField] private int waveChangeTime = 8;
    private int curWave = 1;
    private int curFloor = 1;
    private float waveTimer = 0;

    public int CurWave { get { return curWave; } }

    [Header("[Ref]")]
    Background[] backgrounds;

    [Header("[TMP]")]
    [SerializeField] private TextMeshProUGUI nextWaveText;
    [SerializeField] private TextMeshProUGUI floorText;
    [SerializeField] private TextMeshProUGUI waveText;

    [Header("[Audio]")]
    [SerializeField] private AudioClip floorChangeClip;

    public void Start()
    {
        backgrounds = FindObjectsOfType<Background>();
        StartCoroutine(WaveSystem());
    }

    public void Update()
    {
        nextWaveText.text = $"���� ���̺� ���� : {wavePerTime - waveTimer:0.0} ��";
        waveTimer += Time.deltaTime;
    }

    public IEnumerator WaveSystem()
    {
        while (true)
        {
            floorText.text = $"{curFloor} ��";
            yield return new WaitUntil(() => waveTimer > wavePerTime);

            waveTimer = 0;
            curWave++;
            curFloor = (curWave / 3) + 1;

            #region Dotween ó��
            Sequence seqWave = DOTween.Sequence();
            seqWave.Append(waveText.rectTransform.DOAnchorPosY(300, 1f));
            seqWave.AppendInterval(1f);
            seqWave.Join(waveText.rectTransform.DOShakeAnchorPos(1, 50, 100));
            seqWave.Append(waveText.rectTransform.DOAnchorPosY(700, 1f));
            #endregion

            if (curWave % 3 == 0)
            {
                if (curFloor == 6)
                {
                    //Ending �ε�
                }

                foreach (Background bg in backgrounds) { bg.FloorChange(); }
                //EnemySubject.instance.NotifyObserver();
                CameraManager.Instance.CameraShake(1, 1, 4);

                #region Dotween ó��
                Sequence seq = DOTween.Sequence();

                // Define.Instance.playerController.�����Լ�;
                seq.AppendCallback(() => EXPManager.Instance.isCanLevelup = false);
                seq.AppendCallback(() => EnemyGenerator.Instance.isCanGenerated = false);
                seq.AppendInterval(waveChangeTime);
                seq.AppendCallback(() => EXPManager.Instance.isCanLevelup = true);
                seq.AppendCallback(() => EnemyGenerator.Instance.isCanGenerated = true);

                AudioManager.PlayAudio(floorChangeClip);
                #endregion

                floorText.text = $"{curFloor} ��";
            }
        }
    }
}
