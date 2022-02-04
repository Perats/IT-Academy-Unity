using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    private float timer = 0.0f;
    private float randomTime = 0.0f;

    void Start()
    {
    }

    void Update()
    {
        if (timer > randomTime)
        {
            transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
            randomTime = Random.Range(1.0f, 5.0f);
            timer = 0.0f;
        }
        timer += Time.deltaTime;
    }
}
