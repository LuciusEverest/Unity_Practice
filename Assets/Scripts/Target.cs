using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int points = 100;
    public Material material;
    public GameObject destroyGameObject;

    private void Start()
    {
        GetComponent<Renderer>().material = material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            //Destroy(collision.gameObject, 1);

            // add score to game
            Game.Instance.AddPoints(points);
            if (destroyGameObject != null)
            {
                Destroy(destroyGameObject, 1);
            }
        }
    }
}
