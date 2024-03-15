using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 18f;
    public int numberOfEnemies = 3;
    public float spawnInterval = 0.2f;

    private float lastSpawnTime;
    private bool spawned;
    private Transform player;


    private void Start()
    {
        spawned = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {

        if (Vector3.Distance(transform.position, player.position) < spawnRadius
            && player.position.y > transform.position.y
            && player.position.x < transform.position.x)
        {
            if (!spawned)
            {
                if (Time.time - lastSpawnTime > spawnInterval)
                {

                    StartCoroutine(SpawnEnemiesWithInterval());


                    lastSpawnTime = Time.time;
                }
                spawned = true;
            }

        }
    }

    IEnumerator SpawnEnemiesWithInterval()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        GameObject level = GameObject.Find("Level");
        GameObject spawnedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        spawnedEnemy.transform.parent = level.transform;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            SpaceshipController.score += 500;
        }
        if (other.CompareTag("Laser"))
        {
            Destroy(gameObject);
            SpaceshipController.score += 500;
        }
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}

