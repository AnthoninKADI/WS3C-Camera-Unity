using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Camera Reference")]
    public GameObject target;
    public GameObject CameraTPS;
    public GameObject CameraFPS;
    public GameObject Camera2D;
    public bool isTPS; 
    public bool isFPS; 
    public bool is2D;
    [Space]

    [Header("Options")]
    public bool collisionEnabled = true;
    public bool enableClipping = true;
    private float collisionAvoidanceRadius = 1f;
    private float clippingDistance = 0.1f;
    [Space]

    [Header("TPS Options")]
    [SerializeField]
    private float _offset;
    public float movementSpeed = 5f;
    public float rotationSpeed = 2f;
    [Space]

    [Header("FPS Options")]
    [Range(30, 100)]
    public float FOV;
    public float defaultZoom = 10f;
    public float Zoom;
    private bool isZoomed = false;

    void Start()
    {
        isTPS = true;
        isFPS = false;
        is2D = false;

        //FOV = CameraFPS.main.fieldOfView;
        FOV = Camera.main.fieldOfView;
        defaultZoom = Zoom;
    }

    void Update()
    {
        if (isTPS) 
        {
            CameraTPS.SetActive(isTPS);
            CameraFPS.SetActive(isFPS);
            Camera2D.SetActive(is2D);
        }
        else if (isFPS) 
        {
            CameraTPS.SetActive(false);
            CameraFPS.SetActive(isFPS);
            Camera2D.SetActive(false);

            //CameraFPS.main.fieldOfView = FOV;
            Camera.main.fieldOfView = FOV;
        }
        else if(is2D) 
        {
            CameraTPS.SetActive(false);
            CameraFPS.SetActive(false);
            Camera2D.SetActive(true);
        }

        if (collisionEnabled)
        {
            HandleCameraCollision();
        }

        if (isFPS && Input.GetKey(KeyCode.E))
        {
            isZoomed = true; 
        }
        else
        {
            isZoomed = false; 
        }

    }

    public void TPSSwitch()
    {
        isTPS = true;
        isFPS = false;
        is2D = false;
        //CameraTPS.transform = { 0, 0, offset };
    }
    
    public void FPSSwitch()
    {
        isTPS = false;
        isFPS = true;
        is2D = false;

        //CameraFPS.main.fieldOfView = FOV;
        Camera.main.fieldOfView = FOV;
    }

    public void Switch2D()
    {
        isTPS = false;
        isFPS = false;
        is2D = true;
    }

    void HandleFreeLook()
    {
        if (isTPS) // Free look TPS 
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // rotation horizontale joueur
            target.transform.Rotate(Vector3.up * mouseX * rotationSpeed);

            float newRotationX = Mathf.Clamp(target.transform.eulerAngles.x - mouseY * rotationSpeed, -90f, 90f);
            target.transform.eulerAngles = new Vector3(newRotationX, target.transform.eulerAngles.y, 0f);
        }
    }

    void HandleCameraCollision()
    {
        if (enableClipping)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.forward, out hit, collisionAvoidanceRadius))
            {
                transform.position = hit.point + transform.forward * clippingDistance;
            }
        }
    }


    public void ToggleCollisionSystem()
    {
        collisionEnabled = !collisionEnabled;
    }

    public float Offset
    {
        get { return _offset; }
        set
        {
            _offset = value;
            if (isTPS)
            {
                CameraTPS.transform.position = new Vector3(0, 0, _offset);
            }
        }
    }

    public void ZoomIn(float zoomAmount)
    {
        if (isTPS)
        {
            _offset -= zoomAmount;
            CameraTPS.transform.position = new Vector3(0, 0, _offset);
        }
    }
}

