using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float healthMax;
    public float decay;
    public Slider slider;
    public GameObject destroySpawnObject;
    public UnityEvent deathEvent;
    public bool destroyOnDeath;
    public float health { get; set; }
    public bool isDead { get; set; } = false;

    void Start()
    {
        health = healthMax;
    }

    void Update()
    {
/*        if (Game.Instance.State == Game.eState.Game)
        {
            AddHealth(-Time.deltaTime * decay);
        }*/
        AddHealth(-Time.deltaTime * decay);
        if (slider != null)
        {
            slider.value = health / healthMax;
        }

        if (health <= 0 && !isDead)
        {
            isDead = true;
            deathEvent?.Invoke();
            if (destroySpawnObject != null)
            {
                Instantiate(destroySpawnObject, transform.position, transform.rotation);
            }
            if (destroyOnDeath) GameObject.Destroy(gameObject);
        }
    }
    
    public void AddHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, healthMax);
    }
}
