using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartCutScene : MonoBehaviour
{
    public GameObject blackHoleTrm = null;
    public Material blackHoleMat = null;
    public Material playerMat = null;
    public float progress = 0.1f;
    public float innerRadius = 1f;

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
        if (Input.GetKeyDown(KeyCode.L))
        {
            
            StartCoroutine(StartCutScene());
        }
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
