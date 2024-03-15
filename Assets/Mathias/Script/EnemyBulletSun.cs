using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSun : MonoBehaviour
{
    public ParticleSystem burst;
    void Start()
    {
        Invoke("SelfDestroy", 10);
    }
    //Instantiate an Fx burst
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("DeadlyGround"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            SpaceshipController.score+= 5;
        }
        if (other.CompareTag("Laser"))
        {
            Destroy(gameObject);
            SpaceshipController.score++;
        }
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
