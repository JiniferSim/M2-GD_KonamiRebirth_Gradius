using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 5f;
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

        if (Vector3.Distance(transform.position, player.position) < spawnRadius)
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

        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}

