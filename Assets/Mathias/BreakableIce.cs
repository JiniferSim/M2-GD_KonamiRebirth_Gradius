using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableIce : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Color originalColor;
    float flashDuration = 1f;


    public int HP = 3;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }
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
        meshRenderer.material.color = Color.white;
        Invoke("FlashStop", flashDuration);
    }
    private void FlashStop()
    {
        meshRenderer.material.color = originalColor;

    }
}
