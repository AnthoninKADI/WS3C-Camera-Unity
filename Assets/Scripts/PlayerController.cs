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

    void Start() 
    {
        currentSpeed = speed;
        runningSpeed = speed * 2;
    }

    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime * direction.y);
        transform.Rotate(0, rotationSpeed * Time.deltaTime * direction.x, 0);
    }

    public void move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void SwitchTPS(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            // activer la condition du code pour la camera
        }
    }

    public void SwitchFPS(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // activer la condition du code pour la camera
        }
    }

    public void Switch2D(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // activer la condition du code pour la camera
        }
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            speed = runningSpeed;
            Debug.Log(speed);
            // ajouter le zoom de camera pendant la course
        }
        else if (context.canceled) 
        {
            speed = currentSpeed;
            Debug.Log(speed);
            // enlever le zoom de camera pendant la course
        }
    }
}
