using UnityEngine;

public class ZigzagEnemy : MonoBehaviour
{
    public float speed = 5f;
    public float zigzagSpeed = 2f; 
    public float amplitude = 2f; 
    public float frequency = 1f; 


    private Transform player; 
    private bool moveLeft = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        MoveEnemy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other);
            Destroy(gameObject);
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

            
            float yPosition = Mathf.Sin(Time.time * frequency) * amplitude;
            transform.Translate(Vector3.up * yPosition * zigzagSpeed * Time.deltaTime);

        }
    }
}

