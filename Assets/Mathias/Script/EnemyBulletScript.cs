using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private SpaceshipController spaceshipController;
    private GameObject player;
    void Start()
    {
        Invoke("SelfDestroy", 15);
        player = GameObject.FindGameObjectWithTag("Player");
        spaceshipController = player.GetComponent<SpaceshipController>();
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
        }
    }
    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
