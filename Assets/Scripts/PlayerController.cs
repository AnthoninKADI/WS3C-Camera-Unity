using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 8;
    private float runningSpeed;
    private float currentSpeed;
    public float rotationSpeed = 40;
    private Vector2 direction;
    public float zoomRunning;
    public float mouseSensitivity = 2f; 
    private bool movementEnabled = true;

    public CameraScript cameraScript;

    private bool hasZoomed = false;
    private bool isFreeLookEnabled = false;

    void Start() 
    {
        currentSpeed = speed;
        runningSpeed = speed * 2;
    }

    void Update()
    {
        if (cameraScript.isFPS && movementEnabled)
        {
            RotateWithMouse();
        }
        else
        {
            MoveWithKeys();
        }

        if (isFreeLookEnabled && cameraScript.isTPS)
        {
            cameraScript.HandleFreeLook();
        }

    }

    void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotation horizontale du joueur
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);

        // Rotation verticale du joueur
        transform.Rotate(Vector3.left * mouseY * rotationSpeed * Time.deltaTime);
    }
    void MoveWithKeys()
    {
        transform.position += transform.forward * (speed * Time.deltaTime * direction.y);
        transform.Rotate(0, rotationSpeed * Time.deltaTime * direction.x, 0);
    }

    public void move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

        if (cameraScript.is2D)
        {
            direction.x = 0;
        }
    }

    public void SwitchTPS(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            cameraScript.TPSSwitch();
            hasZoomed = false;
        }
    }

    public void SwitchFPS(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (context.performed)
            {
                cameraScript.FPSSwitch();
                movementEnabled = !movementEnabled;
                hasZoomed = false;
            }
        }
    }

    public void Switch2D(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cameraScript.Switch2D();
            hasZoomed = false;
        }
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            speed = runningSpeed;
            Debug.Log("Running true");
            cameraScript.ZoomIn(zoomRunning);
            hasZoomed = false;
        }
        else if (context.canceled) 
        {
            speed = currentSpeed;
            Debug.Log("running false");
            cameraScript.ZoomIn(-zoomRunning);
        }
    }

    public void FreeLook(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Running true");
            isFreeLookEnabled = true;
        }
        else if (context.canceled)
        {
            Debug.Log("running false");
            isFreeLookEnabled = false;
        }
    }
}
