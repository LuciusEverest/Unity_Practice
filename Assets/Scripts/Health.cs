using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float healthMax;
    public float decay;
    public Slider slider;
    public bool destroyOnDeath;
    public float health { get; set; }

    void Start()
    {
        health = healthMax;
    }

    void Update()
    {
        if (Game.Instance.State == Game.eState.Game)
        {
            AddHealth(-Time.deltaTime * decay);
        }
        if (slider != null)
        {
            slider.value = health / healthMax;
        }

        if (health <= 0)
        {
            if (destroyOnDeath) GameObject.Destroy(gameObject);
        }
    }
    
    public void AddHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, healthMax);
    }
}
