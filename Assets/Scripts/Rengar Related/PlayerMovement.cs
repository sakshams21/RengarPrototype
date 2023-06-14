using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 2f;
    public float HeightMultiplier = 2f;
    public AnimationCurve HeightCurve;

    private Vector3 _start;
    private Vector3 _destination;
    private ParticleSystem _particles;
    private float _time = 0;
    private float _duration= 0;

    private bool _initiate=false;

    public void AssignDestination(Vector3 destinationValue)
    {
        _destination = destinationValue;
        _start = transform.position;
        _duration = Vector3.Distance(_start, destinationValue) / Speed;
        _initiate = true;
        _time = 0;
    }


    private void Update()
    {
        if (!_initiate)
            return;
        
        _time += Time.deltaTime;
        float a = _time / _duration;
        if (a < 1f)
        {
            transform.position = Vector3.Lerp(_start, _destination, a) +
                                 Vector3.up * (HeightCurve.Evaluate(a) * HeightMultiplier);
        }
        else
        {
            _initiate = false;
        }
    }
}
