using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public List<Rigidbody> elements;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().enabled = false;
            foreach (var element in elements)
            {
                element.isKinematic = false;
            }
        }
    }
}
