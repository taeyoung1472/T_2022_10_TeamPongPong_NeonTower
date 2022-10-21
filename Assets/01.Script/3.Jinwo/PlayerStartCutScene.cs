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
        blackHoleTrm.SetActive(true);

        progress = 1;
        playerMat.SetFloat("_Progress", progress);

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
            Debug.Log(progress);
            innerRadius -= 0.005f;
            blackHoleMat.SetFloat("_InnerRadius", innerRadius);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.1f);
        float startTime = Time.time;
        playerMat.SetVector("_TargetPosition", blackHoleTrm.transform.position);

        //�÷��̾� �����°�
        while (true)
        {
            if(progress <= 0f)
            {
                Debug.Log("����");
                break;
            }
            Debug.Log(progress);
            progress -= 0.01f;
            playerMat.SetFloat("_Progress", progress);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        //��Ȧ �����°�
        while (true)
        {
            if (innerRadius >= 1)
            {
                Debug.Log("����2");
                break;
            }
            Debug.Log(progress);
            innerRadius += 0.01f;
            blackHoleMat.SetFloat("_InnerRadius", innerRadius);
            yield return null;
        }

        blackHoleTrm.SetActive(false);

    }
}
