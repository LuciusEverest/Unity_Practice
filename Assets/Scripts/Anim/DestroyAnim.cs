using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnim : MonoBehaviour
{
    public GameObject destroyGameObject;

    public void DestroyEvent(float time)
    {
        Destroy(destroyGameObject, time);
    }
}
