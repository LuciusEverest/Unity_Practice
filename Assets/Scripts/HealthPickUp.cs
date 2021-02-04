using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float health = 10;
    public GameObject spawnObject;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.AddHealth(this.health);
            if(spawnObject != null)
            {
                GameObject gameObject = Instantiate(spawnObject, transform.position, transform.rotation);
                Destroy(gameObject, 2);
            }

            Destroy(gameObject);
        }
    }
}
