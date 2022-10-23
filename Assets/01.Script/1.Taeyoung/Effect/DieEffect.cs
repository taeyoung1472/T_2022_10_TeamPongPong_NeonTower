using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Tools;

public class DieEffect : MonoBehaviour
{

    public GameObject dieParticlePrefab;
    public GameObject dieEffectPrefab;
    public GameObject pregmantPrefab;
    public GameObject diePanel;
    public ParticleSystem particle;
    public ParticleSystem particle1;
    public ParticleSystem particle2;

    public Transform playerTrm;
    public GameObject gameUI;

    public void Start()
    {
        particle.Stop();
        particle1.Stop();
        particle2.Stop();
    }

    public void PlayerDieEffect()
    {
        EXPManager.Instance.IsCanLevelUp = false;
        particle.Stop();
        particle1.Stop();
        particle2.Stop();
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => particle.Play());
        seq.AppendInterval(3f);
        seq.AppendCallback(() => particle1.Play());
        seq.AppendInterval(0.01f);
        seq.AppendCallback(() => CameraManager.Instance.CameraShake(6f, 2f, .5f));
        seq.AppendCallback(() => particle2.Play());
        seq.AppendCallback(() => particle.Stop());
        seq.AppendCallback(() => EnemySubject.Instance.NotifyObserver());
        seq.AppendCallback(() => Glitch.GlitchManager.Instance.LoadGameCutScene());
        //seq.AppendCallback(() => Glitch.GlitchManager.Instance.cantDoZero = true);
        seq.AppendInterval(0.25f);
       
        //seq.AppendCallback(() => Glitch.GlitchManager.Instance.StartSceneValue());
        //seq.Append(DOTween.To(() => Glitch.GlitchManager.Instance._intensity, x => Glitch.GlitchManager.Instance._intensity = x, 1f, 3f));
        seq.AppendInterval(1f);
        seq.AppendCallback(() => gameUI.SetActive(false));
        seq.AppendCallback(() => Time.timeScale = 0);
        seq.AppendCallback(() => UIManager.Instance.ActiveUI(diePanel));
    }
}
