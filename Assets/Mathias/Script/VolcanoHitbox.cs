using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoHitbox : MonoBehaviour
{
    public ParticleSystem sunDeath;
    public GameObject whiteMesh, laserSun, mainDeathObj, deathMeshL, deathMeshR, deathCube;
    public float flashDuration = 1f;
    public bool died;

    public int HP = 20;

    private void Update()
    {
        if (HP <= 0)
        {
            
            //Instantiate(deathMeshR, deathMeshR.transform.position, Quaternion.identity);
            //Instantiate(deathMeshL, deathMeshL.transform.position, Quaternion.identity);
            mainDeathObj.SetActive(true);
            deathMeshR.SetActive(true);
            deathMeshL.SetActive(true);
            deathCube.SetActive(true);

            deathCube.GetComponent<Dissolver>().startBossBattle = true;
            deathMeshR.GetComponent<Dissolver>().startBossBattle = true;
            deathMeshL.GetComponent<Dissolver>().startBossBattle = true;
            
            Invoke("SelfDestroy", 1f);
            HP = 1;
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
        if (other.CompareTag("Laser"))
        {
            FlashStart();
            HP--;
        }
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }

    private void SelfDestroy()
    {
        died = true;
        SpaceshipController.score += 5000;
        ParticleSystem sunParticlo = Instantiate(sunDeath, laserSun.transform.position, Quaternion.identity);
        Destroy(sunParticlo, 10f);
        laserSun.SetActive(false);
        
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
