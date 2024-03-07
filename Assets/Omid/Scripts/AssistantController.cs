using UnityEngine;

public class AssistantController : MonoBehaviour
{
    private GameObject playerShip;
    private float speed = 5f;
    private float smoothness = 0.9f; // 0 to 1
    private float nextFireTime;

    void Start()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");

        if (playerShip == null)
        {
            Debug.LogError("Player ship not found. Make sure it has the 'Player' tag.");
            Destroy(gameObject);
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
            Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(playerShip.transform.position.x, playerShip.transform.position.y+1.5f, playerShip.transform.position.z), smoothness * Time.deltaTime * speed);

            transform.position = new Vector3(targetPosition.x, playerShip.transform.position.y + 1.5f, targetPosition.z);
            CopyPlayerActions();
        }
    }

    void CopyPlayerActions()
    {

        SpaceshipController playerController = playerShip.GetComponent<SpaceshipController>();
}
}
