using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class DoorController : MonoBehaviour
{
    private NavMeshObstacle meshObstacle;
    public bool isOpened = false;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationAmount = 90f;
    [SerializeField]
    private float _forwardDirection = 0f;
    private Vector3 _startRotation;
    private Vector3 _forward;
    private Coroutine AnimationCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        meshObstacle = GetComponent<NavMeshObstacle>();
        meshObstacle.carveOnlyStationary = false;
        meshObstacle.carving = isOpened;
        meshObstacle.enabled = isOpened;

        _startRotation = transform.rotation.eulerAngles;
        _forward = transform.right;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(Vector3 UserPosition)
    {
        if (!isOpened)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
            float dot = Vector3.Dot(_forward, (UserPosition - transform.position).normalized);
            AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;
        if (ForwardAmount >= _forwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(_startRotation.x,_startRotation.y - _rotationAmount, _startRotation.z));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(_startRotation.x, _startRotation.y + _rotationAmount, _startRotation.z));
        }
        isOpened = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }
        meshObstacle.enabled = true;
        meshObstacle.carving = true;
    }

    public void Close()
    {
        if (isOpened)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
            AnimationCoroutine = StartCoroutine(DoRotationClose());
        }
    }

    private IEnumerator DoRotationClose()
    {
        meshObstacle.enabled = false;
        meshObstacle.carving = false;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(_startRotation);
        isOpened = false;
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }
    }
}
