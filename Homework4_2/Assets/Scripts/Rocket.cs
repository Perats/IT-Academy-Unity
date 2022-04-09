using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    private float radius = 5f;
    private float force = 2000f;
    public GameObject explosionPrefab;

    void Boom()
    {
        Collider[] collidersArray = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearlyObject in collidersArray)
        {
            Rigidbody getBody = nearlyObject.GetComponent<Rigidbody>();
            if (getBody != null)
            {
                getBody.AddExplosionForce(force, transform.position, radius);
            }
        }
        var esplose = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(esplose.gameObject, 2f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Boom();
    }
}
