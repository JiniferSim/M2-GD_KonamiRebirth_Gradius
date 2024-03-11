using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoHitbox : MonoBehaviour
{
    
    public GameObject whiteMesh;
    public float flashDuration = 1f;


    public int HP = 20;

    private void Update()
    {
        if (HP <= 0)
        {
            SelfDestroy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);

            FlashStart();
            HP--;
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void FlashStart()
    {
        whiteMesh.SetActive(true);
        Invoke("FlashStop", flashDuration);
    }
    private void FlashStop()
    {
        whiteMesh.SetActive(false);
    }
}
