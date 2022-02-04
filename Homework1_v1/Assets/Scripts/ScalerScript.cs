using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerScript : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }
}
