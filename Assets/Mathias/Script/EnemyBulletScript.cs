using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    void Start()
    {
        Invoke("SelfDestroy", 15);
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
            Destroy(other.gameObject);
        }
    }
    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
