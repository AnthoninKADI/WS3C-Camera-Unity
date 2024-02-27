using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f;
    public float distanceFromPlayer = 10f;
    public float smoothing = 1f; 

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Pas de joueur défini.");
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            float targetX = player.position.x + distanceFromPlayer;
            Vector3 targetPosition = new Vector3(targetX, transform.position.y, player.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * smoothing * Time.deltaTime);
        }
    }
}
