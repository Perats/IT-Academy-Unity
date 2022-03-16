using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed = 1f;



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _rb.AddForce(new Vector3(1, 0.5f, 0) , ForceMode.Force);

    }

    void OnCollisionEnter(Collision collision)
    {
        speed = -speed;
    }
}
