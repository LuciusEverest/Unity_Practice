using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehavior : Behavior
{
    public float displacement;
    public float radius;
    public float distance;
    float angle;

    public override Vector3 Execute()
    {
        Vector3 force = Vector3.zero;

        angle = angle + Random.Range(-displacement, displacement);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);

        Vector3 point = rotation * Vector3.forward * radius;
        Vector3 forward = Agent.Direction * distance;
        Vector3 direction = (forward + point).normalized;
        Vector3 desired = direction * Agent.maxSpeed;

        force = Vector3.ClampMagnitude(desired - Agent.Velocity, Agent.maxForce);

        Debug.DrawLine(transform.position, transform.position + forward, Color.yellow);
        Debug.DrawLine(transform.position + forward, transform.position + forward + point, Color.yellow);

        Debug.DrawRay(transform.position, desired, Color.red); // desired
        Debug.DrawRay(transform.position + Agent.Velocity, force, Color.green); // steering

        return force;    
    }
}
