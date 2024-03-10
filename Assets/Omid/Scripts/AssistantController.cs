using UnityEngine;
using System.Collections.Generic;

public class AssistantController : MonoBehaviour
{
    private GameObject playerShip;
    private static List<GameObject> allAssistants = new List<GameObject>();
    private float speed;
    private float smoothness = 0.9f; // 0 to 1
    private int assistantIndex = -1;

    void Start()
    {
        speed = Random.Range(5,8);//for more fun

        playerShip = GameObject.FindGameObjectWithTag("Player");

        if (playerShip == null)
        {
            Debug.LogError("Player ship not found");
            Destroy(gameObject);
        }

        if (!allAssistants.Contains(gameObject))
        {
            allAssistants.Add(gameObject);
            assistantIndex = allAssistants.IndexOf(gameObject);
        }

        // Spawn
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float assistantX = -cameraWidth - 2f;
        transform.position = new Vector3(assistantX, 0f, 0f);
    }

    void Update()
    {
        if (playerShip != null)
        {
            Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(playerShip.transform.position.x, GetAssistantYPosition(), playerShip.transform.position.z), smoothness * Time.deltaTime * speed);

            transform.position = new Vector3(targetPosition.x, GetAssistantYPosition(), targetPosition.z);
            CopyPlayerActions();
        }
    }

    float GetAssistantYPosition()
    {

        if (assistantIndex > 0 && assistantIndex < allAssistants.Count)
        {

            return allAssistants[assistantIndex - 1].transform.position.y + 1.5f;
        }

        return playerShip.transform.position.y + 1.5f;
    }

    void CopyPlayerActions()
    {
        SpaceshipController playerController = playerShip.GetComponent<SpaceshipController>();
    }
}

