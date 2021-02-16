using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorAnim : MonoBehaviour
{
    [Range(1, 10)] public float speed = 1;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", speed);

        if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetTrigger("Open");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Close");
        }
    }
}
