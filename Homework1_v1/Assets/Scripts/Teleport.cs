using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private float _timer = 0.0f;
    private float _randomTime = 0.0f;

    void Start()
    {
    }

    void Update()
    {
        if (_timer > _randomTime)
        {
            transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
            _randomTime = Random.Range(1.0f, 5.0f);
            _timer = 0.0f;
        }
        _timer += Time.deltaTime;
    }
}
