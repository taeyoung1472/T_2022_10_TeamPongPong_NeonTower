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

    private PlayerController _target = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _target = other.GetComponent<PlayerController>();
            _lastSpeed = _target.SpeedFixValue;
            _target.SpeedFixValue = _lastSpeed * _slowIntensity;
            Debug.Log(_lastSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _target.SpeedFixValue = _lastSpeed;
        }
    }

    private void OnDisable()
    {
        if(_lastSpeed > 0f)
        {
            _target.SpeedFixValue = _lastSpeed;
        }
    }
}
