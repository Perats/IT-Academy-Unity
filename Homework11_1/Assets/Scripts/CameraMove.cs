using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position += new Vector3(0f, 0.001f, 0f);
    }
}
