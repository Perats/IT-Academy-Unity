using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    private bool _isScale = true;
    void Start()
    {
    }
    void Update()
    {
        if (_isScale)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            if (transform.localScale.x > 10) _isScale = false;
        }
        else
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            if (transform.localScale.x < 0.2) _isScale = true;
        }
        
    }
}
