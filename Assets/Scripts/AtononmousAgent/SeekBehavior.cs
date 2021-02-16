using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehavior : Behavior
{
    public override Vector3 Execute()
    {
        Vector3 force = Vector3.zero;

        GameObject[] gameObjects = perception.GetGameObjects();
        if (gameObjects != null && gameObjects.Length > 0)
        {
            GameObject target = gameObjects[0]; // nearest

            Vector3 direction = (target.transform.position - transform.position).normalized;
            Vector3 desired = direction * Agent.maxSpeed;
            force = Vector3.ClampMagnitude(desired - Agent.Velocity, Agent.maxForce);

            Debug.DrawRay(transform.position, desired, Color.red); // desired
            Debug.DrawRay(transform.position + Agent.Velocity, force, Color.green); // steering
        }

        return force;
    }
}
