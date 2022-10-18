using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowField : MonoBehaviour
{
    private float _lastSpeed = 0f;
    private float _slowIntensity = 0f;
    public float SlowIntensity
    {
        get => _slowIntensity;
        set => _slowIntensity = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _lastSpeed = other.GetComponent<PlayerController>().CurSpeed;
            //other.GetComponent<PlayerController>().CurSpeed = _lastSpeed * _slowIntensity;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //other.GetComponent<PlayerController>().CurSpeed = _lastSpeed;
    }

}
