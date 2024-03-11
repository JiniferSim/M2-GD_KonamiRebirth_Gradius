using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserVolcano : MonoBehaviour
{
    public GameObject sunSphere, sunGlow, sunSphere2, sunGlow2;
    public Camera cam;
    public float glowDelay , sphereDelay, shakeD, shotD;

    public GameObject bulletPrefab;
    public float bulletSpeed, shotDelay;
    public AudioClip shootSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


    private void SummonGlow()
    {
        sunGlow.SetActive(true);
        sunGlow2.SetActive(true);

        Invoke("SummonSphere", sphereDelay);
    }
    private void SummonSphere()
    {
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
