using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerStartCutScene : MonoBehaviour
{
    [SerializeField] private GameObject blackHoleTrm = null;
    [SerializeField] private Material blackHoleMat = null;
    [SerializeField] private Material playerMat = null;
    [SerializeField] private Material playerResurrectionMat = null; //��Ȱ�Ҷ� ���� ��Ƽ����
    [SerializeField] private float progress = 0.1f;
    [SerializeField] private float innerRadius = 1f;
    [SerializeField] private float singularity = 0f;

    [SerializeField] private Material bodyOutlineMat;

    [SerializeField] private Vector3 blackHolePos = new Vector3(0, 2.24f, 3f);

    
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject bombEffect = null;
    [SerializeField] private Animator bombAnimator = null;

    [SerializeField] private VisualEffect purplesparkleEffect = null;

    [SerializeField] private Material[] allMaterials = null;
    private Renderer[] allChildRenderers;
    private void Awake()
    {
        purplesparkleEffect = transform.Find("StyliesdSparkle").gameObject.GetComponent<VisualEffect>();
        purplesparkleEffect.gameObject.SetActive(false);

        bombEffect = transform.Find("PlayerBoom").gameObject;


        bombAnimator = bombEffect.GetComponent<Animator>();
        bombEffect.SetActive(false);

    }
    private void Start()
    {
        arrow.SetActive(false);

        allChildRenderers = GetComponentsInChildren<Renderer>();
        allMaterials = new Material[allChildRenderers.Length];

        for (int j = 0; j < allChildRenderers.Length; j++)
        {
            if(allChildRenderers[j].material != null)
            {
                allMaterials[j] = allChildRenderers[j].material;
            }
        }

        StartCoroutine(StartCutScene());
    }
    public void Resurrection(Action callback)
    {
        StartCoroutine(ResurrectionCutScene(callback));
    }
    public IEnumerator ResurrectionCutScene(Action callback)
    {
        CameraManager.Instance.TargetingCameraAnimationUnscale(transform, 4f, 22.5f);

        Glitch.GlitchManager.Instance.OtherValue();
        yield return new WaitForSecondsRealtime(0.5f);
        //�ƿ����� �����
        bodyOutlineMat.SetFloat("_Thickness", 0);

        //��� ��Ƽ���� ���� ����Ʈ ��Ƽ����� ��ü
        for (int i = 0; i < allMaterials.Length; i++)
        {
            if (allMaterials[i] != playerMat && allMaterials[i].name != "SmokeBCG2 (Instance)")
            {
                //allMaterials[i] = playerResurrectionMat;
                allChildRenderers[i].material = playerResurrectionMat;
            }
        }
        

        CameraManager.Instance.CameraShake(7, 7, 1f);
        //����ٰ� �ο��̰� ���� ������ ���̴� ������ �ɰ� ������
        bombEffect.SetActive(true);
        bombAnimator.SetTrigger("Bomb");
        yield return new WaitForSecondsRealtime(0.25f);
        //��󠺴ٰ�
        singularity = 0;
        playerResurrectionMat?.SetFloat("_Singularity", 0);
        while (true)
        {
            if (singularity >= 1f)
            {
                Debug.Log("��Ȱ1");
                break;
            }
            singularity += 0.0035f;
            playerResurrectionMat?.SetFloat("_Singularity", singularity);
            yield return new WaitForSecondsRealtime(0.001f);
        }

        yield return new WaitForSecondsRealtime(0.7f);
        bombEffect.SetActive(false);
        //�ٽ� ����
        while (true)
        {
            if (singularity <= 0f)
            {
                Debug.Log("��Ȱ2");
                break;
            }
            if(singularity <= 0.4f && !purplesparkleEffect.gameObject.activeSelf)
            {
                callback?.Invoke();
                bodyOutlineMat.SetFloat("_Thickness", 1.2f);
                Glitch.GlitchManager.Instance.ZeroValue();
                purplesparkleEffect.gameObject.SetActive(true);
            }
            singularity -= 0.0025f;
            playerResurrectionMat?.SetFloat("_Singularity", singularity);
            yield return new WaitForSecondsRealtime(0.001f);
        }
        purplesparkleEffect.gameObject.SetActive(false);
        //���� ��Ƽ����� ��ü
        for (int i = 0; i < allMaterials.Length; i++)
        {
            allChildRenderers[i].material = allMaterials[i];
        }
        
        yield return new WaitForSeconds(1f);

        Define.Instance.playerController.IsResurrection = false;
    }


    public IEnumerator StartCutScene()
    {
        bodyOutlineMat.SetFloat("_Thickness", 0);

        progress = 1;
        for (int i = 0; i < allMaterials.Length; i++)
        {
            allMaterials[i].SetFloat("_Progress", progress);
        }

        yield return new WaitForSeconds(2f);

        GameObject go = Instantiate(blackHoleTrm, blackHolePos, Quaternion.Euler(-180, 0, 0));
        AudioManager.PlayAudio(UISoundManager.Instance.data.portalOpenClip);
        go.SetActive(true);


        Glitch.GlitchManager.Instance.GrayValue();
        Glitch.GlitchManager.Instance._intensity = 0.01f;

        innerRadius = 0.5f;
        blackHoleMat.SetFloat("_InnerRadius", innerRadius);


        //��Ȧ ����� ��
        
        while (true)
        {
            if (innerRadius <= -0.25)
            {
                Debug.Log("����2");
                break;
            }
            innerRadius -= 0.005f;
            blackHoleMat.SetFloat("_InnerRadius", innerRadius);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.1f);
        float startTime = Time.time;
        playerMat.SetVector("_TargetPosition", blackHolePos);

        
        CameraManager.Instance.CameraShake(13, 10, 1f);
        AudioManager.PlayAudio(UISoundManager.Instance.data.portalCloseClip);

        //�÷��̾� �����°�
        while (true)
        {
            if(progress <= 0f)
            {
                Debug.Log("����");
                break;
            }
            progress -= 0.01f;
            for (int i = 0; i < allMaterials.Length; i++)
            {
                allMaterials[i].SetFloat("_Progress", progress);
            }
            yield return null;
        }
        bodyOutlineMat.SetFloat("_Thickness", 1.2f);
        yield return new WaitForSeconds(0.5f);

        Glitch.GlitchManager.Instance.ZeroValue();
        //��Ȧ �����°�
        while (true)
        {
            if (innerRadius >= 1)
            {
                Debug.Log("����2");
                break;
            }
            innerRadius += 0.01f;
            blackHoleMat.SetFloat("_InnerRadius", innerRadius);
            yield return null;
        }
        arrow.SetActive(true);
        Destroy(go);
    }
}
