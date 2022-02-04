using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerScript : MonoBehaviour
{
    private bool isScale = true;
    void Start()
    {
    }
    void Update()
    {
        if (isScale)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            if (transform.localScale.x > 10) isScale = false;
        }
        else
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            if (transform.localScale.x < 0.2) isScale = true;
        }
        
    }
}
