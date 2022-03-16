using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        _rb.transform.localScale += new Vector3(1f,1f,1f);
    }
}
