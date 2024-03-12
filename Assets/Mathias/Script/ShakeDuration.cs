using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeDuration : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = .5f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            gameObject.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
            shakeAmount -= Time.deltaTime * decreaseFactor;
            if (shakeAmount <= 0) shakeAmount = 0;
        }
        else
        {
            shakeDuration = 0f;
            gameObject.transform.localPosition = originalPos;
        }
    }
}
