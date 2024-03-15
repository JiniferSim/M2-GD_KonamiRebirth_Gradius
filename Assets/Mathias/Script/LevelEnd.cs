using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    public ParticleSystem sunDeath;
    public GameObject bottomBar, topScore, centerScore;
    public GameObject[] celeb;
    public int current = 0;
    public Text centerScoreText;
    public float interval;
    bool switched;
    private void Update()
    {
        centerScoreText.text = "" + SpaceshipController.score;
    }
    public void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            bottomBar.SetActive(false);
            topScore.SetActive(false);
            InvokeRepeating("Celebrate", interval, interval);
            Invoke("NextScene", 15);
        }
    }
    void Celebrate()
    {
        switched = !switched;
        centerScore.SetActive(switched);
        ParticleSystem sunParticlo = Instantiate(sunDeath, celeb[current].transform.position, Quaternion.identity);
        Destroy(sunParticlo, 5f);
        current = (current + 1) % 6;
    }

    void NextScene()
    {
        SceneManager.LoadScene(0);
    }
}
