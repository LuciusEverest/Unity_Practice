using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPerception : Perception
{
    public Transform rayCastTransform;
    [Range(1, 40)] public float distance = 1;
    [Range(0, 90)] public float angle = 0;
    [Min(2)] public int numRayCast = 2;

    public override GameObject[] GetGameObjects()
    {
        List<GameObject> gameObjects = new List<GameObject>();

        float angleOffset = (angle * 2) / (numRayCast - 1);

        for (int i = 0; i < numRayCast; i++)
        {
            float rayDistance = distance;

            Quaternion rotation = Quaternion.AngleAxis(-angle + (angleOffset * i), Vector3.up);
            Vector3 forward = rotation * rayCastTransform.forward;
            Ray ray = new Ray(rayCastTransform.position, forward);
            if(Physics.Raycast(ray, out RaycastHit rayCastHit, rayDistance))
            {
                rayDistance = rayCastHit.distance;
                gameObjects.Add(rayCastHit.collider.gameObject);
            }
            Debug.DrawRay(ray.origin, ray.direction * rayDistance);
        }

        return gameObjects.ToArray();
    }
}
