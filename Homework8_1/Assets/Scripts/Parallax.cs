using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed;
    public float coorX;
    public float breakX;
  
    void FixedUpdate()
    {
        transform.position += Vector3.left * speed * Time.fixedDeltaTime;
        if (transform.position.x < breakX)
        {
            var deltaX = breakX - transform.position.x;
            transform.position = new Vector3(coorX + deltaX, transform.position.y, transform.position.z);
        }
       
    }
}
