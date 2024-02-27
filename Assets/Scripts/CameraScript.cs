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

    [Header("Canvas Reference")]
    public GameObject InputTPS;
    public GameObject InputFPS;
    public GameObject Input2D;
    public GameObject ArmFPS;
    [Space]

    [Header("Options")]
    public bool collisionEnabled = true;
    public bool enableClipping = true;
    private bool isFreeLook = false;
    private float collisionAvoidanceRadius = 1f;
    private float clippingDistance = 0.1f;
    [Space]

    [Header("TPS Options")]
    public float _offset;
    private float defaultOffset;
    public float movementSpeed = 5f;
    public float rotationSpeed = 2f;
    [Space]

    [Header("FPS Options")]
    [Range(30, 100)]
    public float FOV;
    [Range(0, 20)]
    public float Zoom;
    public bool isZoomed = false;

    [Header("Don't Touch")]
    public float defaultFOV;

    void Start()
    {
        
        isTPS = true;
        isFPS = false;
        is2D = false;
        
        defaultOffset = _offset;
        defaultFOV = FOV;
        FOV = Camera.main.fieldOfView;

        UpdateCameraPosition();
    }

    void Update()
    {
        if (isTPS) 
        {
            CameraTPS.SetActive(true);
            CameraFPS.SetActive(false);
            Camera2D.SetActive(false);

            InputTPS.SetActive(true);
            InputFPS.SetActive(false);
            Input2D.SetActive(false);
            ArmFPS.SetActive(false);
 
        }
        else if (isFPS) 
        {
            CameraTPS.SetActive(false);
            CameraFPS.SetActive(true);
            Camera2D.SetActive(false);

            InputTPS.SetActive(false);
            InputFPS.SetActive(true);
            Input2D.SetActive(false);
            ArmFPS.SetActive(true);

            Camera.main.fieldOfView = FOV;
        }
        else if(is2D) 
        {
            CameraTPS.SetActive(false);
            CameraFPS.SetActive(false);
            Camera2D.SetActive(true);

            InputTPS.SetActive(false);
            InputFPS.SetActive(false);
            Input2D.SetActive(true);
            ArmFPS.SetActive(false);
        }

        if (collisionEnabled)
        {
            HandleCameraCollision();
        }
    }

    public void TPSSwitch()
    {
        isTPS = true;
        isFPS = false;
        is2D = false;
        _offset = defaultOffset;
        UpdateCameraPosition();  
    }
    
    public void FPSSwitch()
    {
        isTPS = false;
        isFPS = true;
        is2D = false;

        Camera.main.fieldOfView = FOV;
    }

    public void Switch2D()
    {
        isTPS = false;
        isFPS = false;
        is2D = true;
    }

    public bool IsFreeLook
    {
        get { return isFreeLook; }
    }

    public void HandleFreeLook()
    {
        if (isTPS) 
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

            target.transform.Rotate(Vector3.up * mouseX);
            isFreeLook = true;
        }
        else
        {
            isFreeLook = false;
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
            UpdateCameraPosition();
        }
    }

    public void ZoomIn(float zoomAmount)
    {
        if (isTPS)
        {
            _offset -= zoomAmount;
            CameraTPS.transform.position = new Vector3(0, 0, _offset);
        }
        else if (isFPS && isZoomed)
        {
            FOV = Mathf.Clamp(FOV - zoomAmount, float.NegativeInfinity, 100f);
            Camera.main.fieldOfView = FOV;
        }
    }

    private void UpdateCameraPosition()
    {
        if (isTPS)
        {
            CameraTPS.transform.localPosition = new Vector3(0, 2.66f, -_offset);
        }
    }

}

