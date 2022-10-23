using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartCutScene : MonoBehaviour
{
    public GameObject blackHoleTrm = null;
    public Material blackHoleMat = null;
    public Material playerMat = null;
    public Material playerResurrectionMat = null; //부활할때 쓰는 머티리얼
    public float progress = 0.1f;
    public float innerRadius = 1f;
    public float singularity = 0f;

    public Material bodyOutlineMat;

    public Vector3 blackHolePos = new Vector3(0, 2.24f, 3f);

    public Material[] allMaterials = null;

    private Renderer[] allChildRenderers;

    public GameObject arrow;
    private void Awake()
    {
        
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
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{

        //    StartCoroutine(StartCutScene());
        //}
        if (Input.GetKeyDown(KeyCode.K))
        {

            StartCoroutine(ResurrectionCutScene());
        }
    }
    public IEnumerator ResurrectionCutScene()
    {

        CameraManager.Instance.TargetingCameraAnimation(transform, 8.5f, 20f);

        Glitch.GlitchManager.Instance.OtherValue();
        yield return new WaitForSeconds(0.5f);
        //아웃라인 사라짐
        bodyOutlineMat.SetFloat("_Thickness", 0);

        //모든 머티리얼 부할 이펙트 머티리얼로 교체
        for (int i = 0; i < allMaterials.Length; i++)
        {
            if (allMaterials[i] != playerMat)
            {
                //allMaterials[i] = playerResurrectionMat;
                allChildRenderers[i].material = playerResurrectionMat;
            }
        }

        CameraManager.Instance.CameraShake(7, 7, 1f);
        //여기다가 민영이가 만든 터지는 쉐이더 넣으면 될거 같은디



        //사라졋다가
        singularity = 0;
        playerResurrectionMat?.SetFloat("_Singularity", 0);
        while (true)
        {
            if (singularity >= 1f)
            {
                Debug.Log("부활1");
                break;
            }
            singularity += 0.0025f;
            playerResurrectionMat?.SetFloat("_Singularity", singularity);
            yield return new WaitForSeconds(0.001f);
        }

        yield return new WaitForSeconds(0.1f);

        //다시 나옴
        while (true)
        {
            if (singularity <= 0f)
            {
                Debug.Log("부활2");
                break;
            }
            singularity -= 0.0025f;
            playerResurrectionMat?.SetFloat("_Singularity", singularity);
            yield return new WaitForSeconds(0.001f);
        }
        //아웃라인 주고
        bodyOutlineMat.SetFloat("_Thickness", 1.2f);

        //원래 머티리얼로 교체
        for (int i = 0; i < allMaterials.Length; i++)
        {
            allChildRenderers[i].material = allMaterials[i];
        }
        Glitch.GlitchManager.Instance.ZeroValue();
        yield return new WaitForSeconds(1f);
    }


    public IEnumerator StartCutScene()
    {
        bodyOutlineMat.SetFloat("_Thickness", 0);

        progress = 1;
        for (int i = 0; i < allMaterials.Length; i++)
        {
            allMaterials[i].SetFloat("_Progress", progress);
        }

        yield return new WaitForSeconds(1.25f);

        GameObject go = Instantiate(blackHoleTrm, blackHolePos, Quaternion.Euler(-180, 0, 0));
        go.SetActive(true);


        Glitch.GlitchManager.Instance.GrayValue();
        Glitch.GlitchManager.Instance._intensity = 0.01f;

        innerRadius = 0.5f;
        blackHoleMat.SetFloat("_InnerRadius", innerRadius);


        //블랙홀 생기는 거
        
        while (true)
        {
            if (innerRadius <= -0.25)
            {
                Debug.Log("머임2");
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
        //플레이어 나오는거
        while (true)
        {
            if(progress <= 0f)
            {
                Debug.Log("머임");
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
        //블랙홀 닫히는거
        while (true)
        {
            if (innerRadius >= 1)
            {
                Debug.Log("머임2");
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
