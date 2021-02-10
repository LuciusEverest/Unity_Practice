using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Impact : MonoBehaviour
{
    public float force = 10;
    public float damage = 10;

    SphereCollider sphereCollider;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.AddHealth(-damage);
        }

        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.AddExplosionForce(force, transform.position, sphereCollider.radius, 2);
        }
    }
}
