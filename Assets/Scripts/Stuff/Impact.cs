using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Impact : MonoBehaviour
{
    public float force = 10;
    public float damage = 10;
    public bool isRadial = false;

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
            float scale = 1;
            if (isRadial)
            {
                float distance = Vector3.Distance(transform.position, other.transform.position);
                scale = 1 - (distance / sphereCollider.radius);
            }

            health.AddHealth(-damage * scale);
        }

        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.AddExplosionForce(force, transform.position, sphereCollider.radius, 2);
        }
    }
}
