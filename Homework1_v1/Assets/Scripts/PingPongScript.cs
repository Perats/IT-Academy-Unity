using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongScript : MonoBehaviour
{
    public float speed = 5.0f;

    private Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);

    private bool moveForvard = true;
    void Start()
    {
    }

    void Update()
    {
        if (moveForvard)
        {
            transform.position = transform.position + direction * speed * Time.deltaTime;
            if (transform.position.x > 20) moveForvard = false;
        }
        else
        {
            transform.position = transform.position - direction * speed * Time.deltaTime;
            if (transform.position.x < 0) moveForvard = true;
        }
    }
}
