using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenede : MonoBehaviour
{
    public float radius = 5f;
    public float force = 2000f;

    void Boom()
    {
        Collider[] collidersArray = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearlyObject in collidersArray)
        {
            Rigidbody getBody = nearlyObject.GetComponent<Rigidbody>();
            if (getBody != null)
            {
                getBody.AddExplosionForce(force, transform.position, radius);
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Target")
        {
            Boom();
        }
    }
}
