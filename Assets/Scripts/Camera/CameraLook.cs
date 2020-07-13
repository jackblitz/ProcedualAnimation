using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class CameraLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 3f;
    public float sensitivityY = 3f;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public Transform Target;

    public float Delta;

    float rotationY = 0F;
    private PlayerInputActions inputActions;
    private Vector2 mousePosition;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.PlayerControls.Direction.performed += Direction_performed;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Direction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        mousePosition = obj.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + mousePosition.x * sensitivityX;

            rotationY += mousePosition.y * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, mousePosition.x * sensitivityX, 0);
        }
        else
        {
            rotationY += mousePosition.y * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }

    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(Target.position.x, transform.position.y, Target.position.z);

        transform.position = Vector3.MoveTowards(transform.position, newPosition, Delta * Time.deltaTime);
    }
}