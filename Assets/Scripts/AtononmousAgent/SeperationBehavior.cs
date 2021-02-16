using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeperationBehavior : Behavior
{
    public override Vector3 Execute()
    {
        Vector3 force = Vector3.zero;

        GameObject[] gameObjects = perception.GetGameObjects();
        if (gameObjects != null && gameObjects.Length > 0)
        {
            // ****
            Vector3 directions = Vector3.zero;
            foreach(GameObject gameObject in gameObjects)
            {
                Vector3 v = Agent.transform.position - gameObject.transform.position;//< create a direction vector from the gameObject position to the agent position > ;
                v = v.normalized / v.magnitude;
                directions += v;
            }
            Vector3 direction = (directions / gameObjects.Length).normalized;
            // ****

            Vector3 desired = direction * Agent.maxSpeed;
            force = Vector3.ClampMagnitude(desired - Agent.Velocity, Agent.maxForce);

            Debug.DrawRay(transform.position, desired, Color.red); // desired
            Debug.DrawRay(transform.position + Agent.Velocity, force, Color.green); // steering
        }

        return force;
    }
}
