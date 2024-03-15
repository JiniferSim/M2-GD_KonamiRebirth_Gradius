using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LaserVolcano : MonoBehaviour
{
    public GameObject sunSphere, sunGlow, sunSphere2, sunGlow2, forceField1, forceField2, activeMesh, activeMesh2;
    public Camera cam;
    public float glowDelay , sphereDelay, shakeD, shotD;

    public GameObject bulletPrefab;
    public float bulletSpeed, shotDelay;
    public AudioClip shootSound;
    private AudioSource audioSource;
    // Start is called before the first frame update

    void Update()
    {
        if (sunSphere == null && sunSphere2 == null)
        {
            Debug.Log("Fuckup");
            Invoke("MoveOn", 2f);
        }   
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            cam.GetComponent<ShakeDuration>().shakeDuration = shakeD;
            Invoke("SummonGlow", glowDelay);
            Invoke("StartShooting", shotD);

        }
    }


    private void MoveOn()
        
    {
            SpaceshipController.levelStop = false;
    }
    

    private void SummonGlow()
    {
        sunGlow.SetActive(true);
        sunGlow2.SetActive(true);
        forceField1.GetComponent<Dissolver>().startBossBattle = true;
        forceField2.GetComponent<Dissolver>().startBossBattle = true;

        Invoke("SummonSphere", sphereDelay);
    }
    private void SummonSphere()
    {
        activeMesh.SetActive(true);
        activeMesh2.SetActive(true);
        sunSphere.SetActive(true);
        sunSphere2.SetActive(true);
    }

    void StartShooting()
    {
        InvokeRepeating("Shoot", shotDelay, shotDelay);
        InvokeRepeating("Shoot2", shotDelay, shotDelay);
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, sunSphere.transform.position, Quaternion.identity);


        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = Random.insideUnitCircle * bulletSpeed;
        }

        if (audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
    void Shoot2()
    {
        GameObject bullet = Instantiate(bulletPrefab, sunSphere2.transform.position, Quaternion.identity);


        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = Random.insideUnitCircle * bulletSpeed;
        }

        if (audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }


}
