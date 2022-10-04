using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class NeonColorChange : MonoBehaviour
{
    private Light myLight;
    private float changeDelay = 10f;
    private float changeTime = 1;

    [Header("[Color]")]
    [SerializeField] private Color[] colorTable;

    private void Awake()
    {
        myLight = GetComponent<Light>();
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        int randIdx = -1;
        while (true)
        {
            yield return new WaitForSeconds(changeDelay + Random.Range(-5f, 5f));

            while (true)
            {
                int rand = Random.Range(0, colorTable.Length);
                if (randIdx != rand)
                {
                    randIdx = rand;
                    break;
                }
            }

            Color prevColor = myLight.color;
            Color randColor = colorTable[Random.Range(0, colorTable.Length)];

            float time = 0;
            while (time < changeTime)
            {
                time += Time.deltaTime / changeTime;
                myLight.color = Color.Lerp(prevColor, randColor, time);
                yield return null;
            }

            myLight.color = randColor;
        }
    }

    /*private void Update()
    {
        if (Time.time >= changetime + currentTime)
        {
            currentTime = Time.time;
            StartCoroutine(LerpColor());
        }
    }

    public void ShuffleColor()
    {
        int random1;
        int random2;

        Color tmp;
        for (int i = 0; i < changeColor.Length; i++)
        {
            random1 = Random.Range(0, changeColor.Length);
            random2 = Random.Range(0, changeColor.Length);

            tmp = changeColor[random1];
            changeColor[random1] = changeColor[random2];
            changeColor[random2] = tmp;

        }
    }
    IEnumerator LerpColor()
    {
        ShuffleColor();
        float progress = 0;
        float increment = smoothness / duration;
        int idx = Random.Range(0, changeColor.Length);
        if (currentColor == changeColor[idx])
        {
            idx = Random.Range(0, changeColor.Length);
        }
        while (progress < 1)
        {
            currentColor = Color.Lerp(currentColor, changeColor[idx], progress);
            myLight.color = currentColor;
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }

    }*/
}
