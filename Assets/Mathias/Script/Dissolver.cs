using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolver : MonoBehaviour
{
    public float dissolveDuration = 3, dissolveStrength;
    public Color startColor, endColor;
    public bool startBossBattle = false;
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
            dissolveMaterial.SetFloat("_Dissolve_Strength", dissolveStrength);

            lerpedColor = Color.Lerp(startColor, endColor, dissolveStrength);
            dissolveMaterial.SetColor("_Color", lerpedColor);

            yield return null;
            
        }

        Destroy(gameObject);
        Destroy(dissolveMaterial);

    }

    private void Update()
    {
        if (startBossBattle)
        {
            StartDissolver();
        }
    }
}
