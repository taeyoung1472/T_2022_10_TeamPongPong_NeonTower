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
    private void Awake()
    {
        
        blackHoleTrm.SetActive(false);
    }
    private void Start()
    {
        //StartCoroutine(StartCutScene());
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
            
        //    StartCoroutine(StartCutScene());
        //}
        //if (Input.GetKeyDown(KeyCode.K))
        //{

        //    StartCoroutine(ResurrectionCutScene());
        //}
    }
    public IEnumerator ResurrectionCutScene()
    {
        bodyOutlineMat.SetFloat("_Thickness", 0);
        Material[] changeMat = null;
        Transform[] childTrm = null;

        childTrm = transform.GetComponentsInChildren<Transform>();
        int i = 0;
        foreach (Transform child in childTrm)
        {
            if (child.gameObject.GetComponent<Material>() != null)
            {
                changeMat[i] = child.gameObject.GetComponent<Material>();
                Debug.Log(changeMat[i]);
                if (changeMat[i] == playerMat)
                {
                    changeMat[i] = playerResurrectionMat;
                    child.gameObject.GetComponent<Renderer>().material = changeMat[i];
                }
            }
            i++;
        }
        Debug.Log(i);
        singularity = 0;
        playerResurrectionMat?.SetFloat("_Singularity", 0);
        while (true)
        {
            if (singularity >= 1f)
            {
                Debug.Log("부활");
                break;
            }
            singularity += 0.01f;
            playerResurrectionMat?.SetFloat("_Singularity", singularity);
            yield return null;
        }
        bodyOutlineMat.SetFloat("_Thickness", 1f);

        //for (int i = 0; i < childTrm.Length; i++)
        //{
        //    if (childTrm[i].gameObject.GetComponent<Material>() != null)
        //    {
        //        changeMat[i] = childTrm[i].gameObject.GetComponent<Material>();

        //        if (changeMat[i] == playerMat)
        //        {
        //            changeMat[i] = playerResurrectionMat;
        //        }
        //    }
        //}

        yield return new WaitForSeconds(1f);
    }
    public IEnumerator StartCutScene()
    {
        bodyOutlineMat.SetFloat("_Thickness", 0);
        blackHoleTrm.SetActive(true);

        progress = 1;
        playerMat.SetFloat("_Progress", progress);

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
        playerMat.SetVector("_TargetPosition", blackHoleTrm.transform.position);

        //플레이어 나오는거
        while (true)
        {
            if(progress <= 0f)
            {
                Debug.Log("머임");
                break;
            }
            progress -= 0.01f;
            playerMat.SetFloat("_Progress", progress);
            yield return null;
        }
        bodyOutlineMat.SetFloat("_Thickness", 1.2f);
        yield return new WaitForSeconds(0.5f);

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
        
        blackHoleTrm.SetActive(false);

    }
}
