using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public float speed = 8;
    private float runningSpeed;
    private float currentSpeed;
    public float rotationSpeed = 40;
    private Vector2 direction;
    [Range(0, 10)]
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
        if (!isFreeLookEnabled && cameraScript.isFPS)
        {
            MoveWithKeys();
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

        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);

        if (!cameraScript.isFPS) 
        {
            
            float newRotationX = Mathf.Clamp(transform.eulerAngles.x - mouseY * rotationSpeed * Time.deltaTime, -90f, 90f);
            transform.eulerAngles = new Vector3(newRotationX, transform.eulerAngles.y, 0f);
        }
    }
    void MoveWithKeys()
    {
        if (!cameraScript.isFPS) 
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime * direction.x, 0);
        }
        else 
        {
            transform.position += transform.right * (speed * Time.deltaTime * direction.x);
        }

        transform.position += transform.forward * (speed * Time.deltaTime * direction.y);
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
            cameraScript.CameraTPS.transform.localPosition = new Vector3(0, 2.66f, - cameraScript._offset + zoomRunning);
        }
        else if (context.canceled) 
        {
            speed = currentSpeed;
            Debug.Log("Running false");
            cameraScript.CameraTPS.transform.localPosition = new Vector3(0, 2.66f, -cameraScript._offset - zoomRunning);
        }
    }

    public void FreeLook(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Free Look true");
            isFreeLookEnabled = true;
        }
        else if (context.canceled)
        {
            Debug.Log("Free Look false");
            isFreeLookEnabled = false;
        }
    }

    public void Zoom(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Zoom true");
            if (cameraScript.isFPS)
            {
                cameraScript.FOV = cameraScript.Zoom;
            }
        }
        else if (context.canceled)
        {
            Debug.Log("Zoom false");
            if (cameraScript.isFPS)
            {
                cameraScript.FOV = cameraScript.defaultFOV;
            }
        }

    }

}
