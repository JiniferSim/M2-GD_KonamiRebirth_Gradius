using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private SpaceshipController spaceshipController;
    void Start()
    {
        Invoke("SelfDestroy", 15);
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
        if (other.CompareTag("Player"))
        {
            spaceshipController.Diyng();
            Destroy(gameObject);
        }
    }
    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
