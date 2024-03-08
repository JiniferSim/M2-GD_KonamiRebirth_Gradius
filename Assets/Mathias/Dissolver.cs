using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolver : MonoBehaviour
{
    public float dissolveDuration = 2, dissolveStrength;
    public Color startColor, endColor;

    public void StartDissolver()
    {
        StartCoroutine(dissolver());
    }

    public IEnumerator dissolver()
    {
        float elapsedTime = 0;
        Material dissolveMaterial = GetComponent<Renderer>().material;
        Color lerpedColor; 

        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            dissolveStrength = Mathf.Lerp(0,1, elapsedTime/ dissolveDuration);

            lerpedColor = Color.Lerp(startColor, endColor, dissolveStrength);
            dissolveMaterial.SetColor("_Color", lerpedColor);

            yield return null;
            
        }

        Destroy(gameObject);
        Destroy(dissolveMaterial);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartDissolver();
        }
    }
}
