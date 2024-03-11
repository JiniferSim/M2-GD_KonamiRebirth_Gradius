using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public Transform gunTransform; 
    public GameObject bulletPrefab; 
    public float rotationSpeed = 5f; 
    public float shootingInterval = 2f; 
    public AudioClip shootSound; 
    public float bulletSpeed = 10f;
    public float shootRadius = 100f;

    private float lastShootTime; 
    private AudioSource audioSource; 
    private Transform playerTransform;
    private SpaceshipController spaceshipController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        spaceshipController = playerObject.GetComponent<SpaceshipController>();
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < shootRadius)
        {
            if (!SpaceshipController.timeStopOn)
            {
                RotateGunTowardsPlayer();


                if (Time.time - lastShootTime > shootingInterval)
                {

                    Shoot();


                    lastShootTime = Time.time;
                }
            }
        }

    }

    void RotateGunTowardsPlayer()
    {
        if (playerTransform != null)
        {
            Vector3 directionToPlayer = playerTransform.position - gunTransform.position;


            gunTransform.up = directionToPlayer.normalized;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);


        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = gunTransform.up * bulletSpeed;
        }

        if (audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            SpaceshipController.score++;
            spaceshipController.EnemyDie(transform);
        }
        if (other.CompareTag("Laser"))
        {
            Destroy(gameObject);
            SpaceshipController.score++;
            spaceshipController.EnemyDie(transform);
        }
    }
}





