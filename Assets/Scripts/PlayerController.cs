using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 8;
    public float rotationSpeed = 40;
    private Vector2 direction;

    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime * direction.y);
        transform.Rotate(0, rotationSpeed * Time.deltaTime * direction.x, 0);
    }

    public void move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
}
