using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    [Range(0, 20)] public float speed = 1;
    [Range(0, 20)] public float jump = 1;
    [Range(-20, 20)] public float gravity = -9.81f;
    public Animator animator;

    CharacterController characterController;
    bool onGround = false;
    Vector3 inputDirection = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        onGround = characterController.isGrounded;
        if (onGround && velocity.y < 0)
        {
            velocity.y = 0f;
        }
                
        Quaternion cameraRotation = Camera.main.transform.rotation;
        Quaternion rotation = Quaternion.AngleAxis(cameraRotation.eulerAngles.y, Vector3.up);
        Vector3 direction = rotation * inputDirection;

        characterController.Move(direction * speed * Time.deltaTime);

        if (inputDirection.magnitude > 0.1f)
        {
            Quaternion target = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, 5 * Time.deltaTime);
        }
        animator.SetFloat("Speed", inputDirection.magnitude);
        animator.SetBool("OnGround", onGround);
        animator.SetFloat("VelocityY", velocity.y);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void OnJump()
    {
        if (onGround)
        {
            velocity.y += jump;
        }
    }

    public void OnPunch()
    {
        animator.SetTrigger("Punch");
    }

    public void OnThrowing()
    {
        animator.SetTrigger("Throwing");
    }

    public void OnWASD(InputValue value)
    {
        Vector2 v2 = value.Get<Vector2>();
        inputDirection.x = v2.x;
        inputDirection.z = v2.y;
    }
}