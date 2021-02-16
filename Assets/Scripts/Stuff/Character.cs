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
    public Weapon weapon;
    public eSpace space = eSpace.World;
    public eMovement movement = eMovement.Free;
    public float turnRate = 3;

    public enum eSpace
    {
        World, 
        Camera,
        Object
    }

    public enum eMovement
    {
        Free,
        Tank,
        Strafe
    }

    CharacterController characterController;
    Rigidbody rb;
    Health health;

    bool onGround = false;
    Vector3 inputDirection = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    Transform cameraTransform;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        onGround = characterController.isGrounded;
        if (onGround && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // ****
        Quaternion orientation = Quaternion.identity;
        switch (space)
        {
            case eSpace.Camera:
                Vector3 forward = cameraTransform.forward;
                forward.y = 0;
                orientation = Quaternion.LookRotation(forward);
                break;
            case eSpace.Object:
                orientation = transform.rotation;
                break;
            default:
                break;
        }

        Vector3 direction = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        switch (movement)
        {
            case eMovement.Free:
                direction = orientation * inputDirection;
                rotation = (direction.sqrMagnitude > 0) ? Quaternion.LookRotation(direction) : transform.rotation;
                break;
            case eMovement.Tank:
                direction.z = inputDirection.z;
                direction = orientation * direction;

                rotation = orientation * Quaternion.AngleAxis(inputDirection.x, Vector3.up);
                break;
            case eMovement.Strafe:
                direction = orientation * inputDirection;
                rotation = Quaternion.LookRotation(orientation * Vector3.forward);
                break;
            default:
                break;
        }
        //****

        characterController.Move(direction * speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnRate * Time.deltaTime);

        // animator
        animator.SetFloat("Speed", inputDirection.magnitude);
        animator.SetBool("OnGround", onGround);
        animator.SetFloat("VelocityY", velocity.y);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (health.isActiveAndEnabled && health.health <= 0)
        {
            ///animator.SetTrigger("Death");
            //Game.Instance.State = Game.eState.GameOver;
        }
    }

    public void OnFire()
    {
        weapon.Fire(transform.forward);
        //Debug.Log("OnFire");
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