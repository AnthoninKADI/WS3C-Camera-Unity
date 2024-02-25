using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public Transform player; 
    public float followDelay = 0.1f; 
    public float smoothing = 5f; 

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("pas de joueur defini");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Appliquer le delai avant de commencer a suivre
            if (followDelay > 0f)
            {
                followDelay -= Time.deltaTime;
            }
            else
            {
                // Calculer la position cible avec interpolation lerp pour la douceur
                Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing * Time.deltaTime);
            }
        }
    }
}
