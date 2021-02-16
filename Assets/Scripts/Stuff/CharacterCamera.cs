using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CharacterCamera : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 offset;
    [Range(0, 20)] public float rate = 1;
    public bool orientToTarget = true;
    public bool clampYaw = true;

    Vector2 inputRotation = Vector2.zero;
    float pitch = 20;
    float yah;
    float distance = 2;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        Quaternion rotationBase = (orientToTarget) ? targetTransform.rotation : Quaternion.identity;
        Quaternion rotation = rotationBase * Quaternion.AngleAxis(yah, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);

        Vector3 targetPosition = targetTransform.position + (rotation * (Vector3.back * distance));
        
        Ray ray = new Ray(targetTransform.position, (targetPosition - targetTransform.position));
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            targetPosition = raycastHit.point;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, rate * Time.deltaTime);

        Vector3 direction = targetTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void OnPitch(InputAction.CallbackContext callbackContext)
    {
        pitch += callbackContext.ReadValue<float>();
        pitch = Mathf.Clamp(pitch, 0, 20);
    }

    public void OnYah(InputAction.CallbackContext callbackContext)
    {
        yah += callbackContext.ReadValue<float>();
        if (clampYaw) yah = Mathf.Clamp(yah, -70, 70);
    }

    public void OnDistance(InputAction.CallbackContext callbackContext)
    {
        distance += callbackContext.ReadValue<float>();
        distance = Mathf.Clamp(distance, 2, 8);
    }

    public void OnCenter(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.started)
        {
            yah = 0;
            pitch = 0;
        }
    }
}
