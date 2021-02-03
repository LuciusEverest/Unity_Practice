using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float healthMax;
    public float health;

    void Start()
    {
        health = healthMax;
    }

    void Update()
    {
        health -= Time.deltaTime * 5;
    }
    
    public void AddHealth(float health)
    {
        this.health += health;
        this.health = Mathf.Min(this.health, healthMax);
    }
}
