using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 2f;
    public GameObject projectilePrefab; 
    public float fireRate = 0.5f; 
    public float backgroundScrollSpeed = 1f; 

    private float verticalBoundary;
    private float horizontalBoundary;
    private float nextFireTime;
    private GameObject background;

    void Start()
    {
        Camera mainCamera = Camera.main;

        verticalBoundary = mainCamera.orthographicSize;
        horizontalBoundary = verticalBoundary * mainCamera.aspect;

        background = GameObject.Find("Background");
    }

    void Update()
    {
        // movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        // Boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalBoundary, horizontalBoundary);
        newPosition.y = Mathf.Clamp(newPosition.y, -verticalBoundary, verticalBoundary);

        transform.position = newPosition;

        // rotation
        if (verticalInput != 0f)
        {
            Quaternion toRotation = Quaternion.Euler(verticalInput > 0f ? 45f : -45f, 0f, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, rotationSpeed * Time.deltaTime);

            float horizontalRotation = horizontalInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, -horizontalRotation);
        }

        if (Input.GetKey(KeyCode.X) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        ScrollBackground();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChooseAbility();
        }
    }

    void ScrollBackground()
    {
        // Bacground scrolling
        float backgroundScroll = backgroundScrollSpeed * Time.deltaTime;
        background.transform.Translate(Vector3.left * backgroundScroll);

        if (background.transform.position.x < -horizontalBoundary * 2f)
        {
            background.transform.Translate(Vector3.right * (horizontalBoundary * 4f));
        }
    }

    void Shoot()
    {

        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        Vector3 projectileDirection = transform.right;
        projectile.GetComponent<Rigidbody>().velocity = projectileDirection * speed * 2f; 
    }

    void ChooseAbility()
    {
        Debug.Log("Выбор способности!");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
