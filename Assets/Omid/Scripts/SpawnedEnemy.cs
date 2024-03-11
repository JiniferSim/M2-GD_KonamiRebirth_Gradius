using Unity.VisualScripting;
using UnityEngine;

public class SpawnedEnemy : MonoBehaviour
{
    public float speed = 5f;
    public float activationRadius = 18f;


    private Transform player;
    private bool moveLeft = false;
    private SpaceshipController spaceshipController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spaceshipController = player.GetComponent<SpaceshipController>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < activationRadius)
        {
            if (!SpaceshipController.timeStopOn)
            {
                MoveEnemy();
            }
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

    void MoveEnemy()
    {
        if (transform.position.y < player.position.y && !moveLeft)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            if (transform.position.y >= player.position.y)
            {
                moveLeft = true;
            }
        }
        else
        {
            float newX = transform.position.x - Time.deltaTime * speed * 3;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            
        }
    }
}
