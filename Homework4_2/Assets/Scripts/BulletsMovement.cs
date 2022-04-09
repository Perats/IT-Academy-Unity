using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsMovement : MonoBehaviour
{
    [SerializeField]
    float _speed = 5f;

    public event Action<BulletsMovement> OnTriggeredEvent;
    void Update()
    {
       // transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider)
        {
            OnTriggeredEvent(this);
        }
    }

    internal void Release()
    {
        gameObject.SetActive(false);
    }
}
