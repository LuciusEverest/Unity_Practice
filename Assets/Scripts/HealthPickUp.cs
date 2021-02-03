using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float health = 10;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.AddHealth(this.health);
            GetComponent<AudioSource>().Play();
            Destroy(gameObject, 1);
        }
    }
}
