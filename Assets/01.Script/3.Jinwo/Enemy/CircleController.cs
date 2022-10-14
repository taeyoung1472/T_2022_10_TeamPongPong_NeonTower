using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    public Material myMat;
    public float alphaTime = 0;
    public Vector3 offset = new Vector3(0.1f, 0.1f, 0);

    public float circleSize = 5f;
    private void Awake()
    {
        myMat = GetComponent<Renderer>().material;
        myMat.color = Color.red;
    }
    private void Update()
    {
        if(gameObject.transform.localScale.x >= circleSize)
        {
            return;
            Destroy(gameObject);
        }
        else
        {
            gameObject.transform.localScale += offset;
        }
    }
    

}
