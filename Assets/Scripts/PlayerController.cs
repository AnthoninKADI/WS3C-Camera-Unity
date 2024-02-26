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
    }

    void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotation horizontale du joueur
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);

        // Rotation verticale du joueur (limiter l'angle vertical entre -90 et 90 degrés)
        float newRotationX = Mathf.Clamp(transform.eulerAngles.x - mouseY * rotationSpeed * Time.deltaTime, -90f, 90f);
        transform.eulerAngles = new Vector3(newRotationX, transform.eulerAngles.y, 0f);
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
            cameraScript.isTPS = true;
        }
    }

    public void SwitchFPS(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cameraScript.isFPS = true;
            movementEnabled = !movementEnabled;
        }
    }

    public void Switch2D(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cameraScript.is2D = true;
        }
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            speed = runningSpeed;
            Debug.Log("Running true");
            cameraScript.ZoomIn(zoomRunning);
        }
        else if (context.canceled) 
        {
            speed = currentSpeed;
            Debug.Log("running false");
            cameraScript.ZoomIn(-zoomRunning);
        }
    }
}
