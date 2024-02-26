using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    private CameraScript cameraScript;

    void Start()
    {
        cameraScript = GetComponentInParent<CameraScript>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasReleasedThisFrame && cameraScript.isFPS)
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
}
