using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSun : MonoBehaviour
{
    public ParticleSystem burst;

    private SpaceshipController spaceshipController;
    void Start()
    {
        Invoke("SelfDestroy", 10);
        spaceshipController = GameObject.FindGameObjectWithTag("Player").GetComponent<SpaceshipController>();
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
            Destroy(gameObject);
            spaceshipController.Diyng();
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
