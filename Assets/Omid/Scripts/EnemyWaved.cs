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
    private SpaceshipController spaceshipController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spaceshipController = player.GetComponent<SpaceshipController>();
        startTime = Time.time;
    }

    void Update()
    {
        if (!SpaceshipController.timeStopOn)
        {
            float waveOffset = Mathf.Sin((Time.time - startTime) * frequency);


            float newX = transform.position.x - Time.deltaTime * speed;
            transform.Translate(Vector3.up * waveOffset * amplitude * Time.deltaTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
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
