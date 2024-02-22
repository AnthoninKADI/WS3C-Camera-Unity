using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    /* 
        public float collisionAvoidanceRadius = 1f; // Le rayon pour éviter les collisions avec des objets
        public float clippingDistance = 0.1f; // La distance de clipping pour éviter les intersections avec des objets

        private Vector3 desiredPosition;
        private Vector3 smoothedPosition;
        private Vector3 rotationEulerAngles;
    */

    [Header("Camera Reference")]
    public Gameobject CameraTPS;
    public Gameobject CameraFPS;
    //public Gameobject Camera2D;
    public bool isTPS; 
    public bool iSFPS; 
    public bool is2D;
    [Space]

    [Header("TPS Options")]
    public float offset; // a definir a la place d'une des valeurs de position de la camera pour set la distance avec le joueur
    public float movementSpeed = 5f;
    public float rotationSpeed = 2f;
    [Space]

    [Header("FPS Options")]
    [Range(30, 100)]
    public float FOV;
    public float Zoom = 10f;
    [Space]

    [Header("2D Options")]
    public float smoothing = 0.1f; // La douceur du suivi
    [Space]


    void Start()
    {
        isTPS = true;
        iSFPS = false;
        iS2D = false;
    }

    void Update()
    {
        if (isTPS) 
        {
            CameraTPS.SetActive(true);
            CameraFPS.SetActive(false);
            Camera2D.SetActive(false);
        }
        else if (iSFPS) 
        {
            CameraTPS.SetActive(false);
            CameraFPS.SetActive(true);
            Camera2D.SetActive(false);
        }
        else if(!is2D) 
        {
            CameraTPS.SetActive(false);
            CameraFPS.SetActive(false);
            Camera2D.SetActive(true);
        }
    }

    public void TPSSwitch()
    {
        isTPS = true;
        iSFPS = false;
        iS2D = false;
        CameraTPS.transform = { 0, 0, offset };
    }
    
    public void FPSSwitch()
    {
        isTPS = false;
        iSFPS = true;
        iS2D = false;
    }

    public void 2DSwitch()
    {
        isTPS = false;
        iSFPS = false;
        iS2D = true;
    }
}

