using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Range(0, 3)] public float fireRate = 0.1f;
    [Range(0, 30)] public float angle = 10.0f;
    public int ammoMax = 100;
    public GameObject projectile;
    public Transform emitTransform;

    public float fireTimer = 0;
    void Start()
    {
        
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
    }

    public bool Fire(Vector3 position, Vector3 direction)
    {
        if (fireTimer >= fireRate)
        {
            fireTimer = 0;
            GameObject gameObject = Instantiate(this.projectile, position, Quaternion.identity);
            gameObject.GetComponent<Projectile>().Fire(direction);

            return true;
        }

        return false;
    }

    public bool Fire(Vector3 direction)
    {
        if (fireTimer >= fireRate)
        {
            fireTimer = 0;
            GameObject gameObject = Instantiate(this.projectile, emitTransform.position, emitTransform.rotation);
            gameObject.GetComponent<Projectile>().Fire(direction);

            return true;
        }

        return false;
    }
}
