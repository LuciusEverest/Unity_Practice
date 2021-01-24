using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0.1f;


    int ammo = 100;
    float fireTimer = 0;

    public GameObject bullet;

    void Start()
    {
        
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            GameObject gameObject = Instantiate(this.bullet, transform.position, Quaternion.identity);
            gameObject.GetComponent<Bullet>().Fire(ray.direction);
        }*/
    }

    public bool Fire(Vector3 position, Vector3 direction)
    {
        if (fireTimer >= fireRate)
        {
            fireTimer = 0;
            GameObject gameObject = Instantiate(this.bullet, position, Quaternion.identity);
            gameObject.GetComponent<Bullet>().Fire(direction);

            return true;
        }

        return false;
    }
}
