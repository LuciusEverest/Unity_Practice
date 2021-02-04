using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float healthMax;
    public float decay;
    public Slider slider;
    public float health { get; set; }

    void Start()
    {
        health = healthMax;
    }

    void Update()
    {
        AddHealth(-Time.deltaTime * decay);
        slider.value = health / healthMax;
    }
    
    public void AddHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, healthMax);
    }
}
