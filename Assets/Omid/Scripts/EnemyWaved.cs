using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaved : MonoBehaviour
{
    public float amplitude = 2f; 
    public float frequency = 2f; 
    public float speed = 2f;

    private Transform player;
    private float startTime;
    private bool visible;
    private SpaceshipController spaceshipController;
    public ParticleSystem death;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spaceshipController = player.GetComponent<SpaceshipController>();
        startTime = Time.time;
    }

    void Update()
    {
        if (visible)
        {
            if (!SpaceshipController.timeStopOn)
            {
                float waveOffset = Mathf.Sin((Time.time - startTime) * frequency);


                float newX = transform.position.x - Time.deltaTime * speed;
                transform.Translate(Vector3.up * waveOffset * amplitude * Time.deltaTime);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
        }
    }

    private void OnBecameVisible()
    {
        visible = true;
    }
    private void OnBecameInvisible()
    {
        visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ParticleSystem deathPS = Instantiate(death, transform.position, Quaternion.identity);
            Destroy(deathPS, 3f);
            Destroy(other.gameObject);
            Destroy(gameObject);
            SpaceshipController.score += 100;
            spaceshipController.EnemyDie(transform);
        }
        if (other.CompareTag("Laser"))
        {
            ParticleSystem deathPS = Instantiate(death, transform.position, Quaternion.identity);
            Destroy(deathPS, 3f);
            Destroy(gameObject);
            SpaceshipController.score += 100;
            spaceshipController.EnemyDie(transform);
        }
    }

}
