using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 2f;
    public float HeightMultiplier = 2f;
    public NavMeshAgent MeshAgent;
    public AnimationCurve HeightCurve;
    public GameObject RangeIndicator_GameObject;

    private Vector3 _start;
    private Vector3 _destination;
    private ParticleSystem _particles;
    private float _time = 0;
    private float _duration= 0;

    private bool _initiate=false;
    private bool _leapStarted;
    private bool _isPlayerInBush;

    private void Start()
    {
        _isPlayerInBush = false;
        
        //MeshAgent.autoRepath = true;
    }

    private void Update() => NormalMovement();

    private bool CheckForBush()
    {
        return _isPlayerInBush;
    }
    public void AssignDestination(Vector3 destinationValue)
    {
        _destination = destinationValue;
        _start = transform.position;
        _duration = Vector3.Distance(_start, destinationValue) / Speed;
        _initiate = true;
        _time = 0;
    }

    private void NormalMovement()
    {
        if (!_initiate)
            return;
        
        MeshAgent.SetDestination(_destination);
    }

    private void BushMovement()
    {
        if (!_initiate)
            return;
        _leapStarted = true;
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
            _leapStarted = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        RangeIndicator_GameObject.SetActive(other.gameObject.TryGetComponent(out Bush _));
        _isPlayerInBush = other.gameObject.TryGetComponent(out Bush _);
    }

    private void OnTriggerExit(Collider other)
    {
        if(RangeIndicator_GameObject.activeSelf)
            RangeIndicator_GameObject.SetActive(false);
        _isPlayerInBush = false;
    }
}

public enum MoveType
{
    Leap, Move, MovementIncludingLeap
}
