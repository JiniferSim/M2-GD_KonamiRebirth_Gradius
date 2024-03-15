using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelEnd : MonoBehaviour
{
    public ParticleSystem sunDeath;
    public GameObject bottomBar, topScore, centerScore;
    public GameObject[] celeb;
    public Text centerScoreText;
    public float interval;
    bool switched;

    private List<int> availableIndices = new List<int>();
    private int current = 0;

    private void Start()
    {
        InitializeAvailableIndices();
    }

    private void InitializeAvailableIndices()
    {
        for (int i = 0; i < celeb.Length; i++)
        {
            availableIndices.Add(i);
        }
        Shuffle(availableIndices);
    }

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

    private void Celebrate()
    {
        if (availableIndices.Count == 0)
        {
            Debug.Log("All indices used up!");
            return;
        }

        switched = !switched;
        centerScore.SetActive(switched);

        int randomIndex = availableIndices[0];
        availableIndices.RemoveAt(0);

        ParticleSystem sunParticle = Instantiate(sunDeath, celeb[randomIndex].transform.position, Quaternion.identity);
        Destroy(sunParticle, 5f);
    }

    private void NextScene()
    {
        SceneManager.LoadScene(0);
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
