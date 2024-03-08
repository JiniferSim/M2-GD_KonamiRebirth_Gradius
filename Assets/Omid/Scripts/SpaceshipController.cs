using UnityEngine;
using UnityEngine.UI;

public class SpaceshipController : MonoBehaviour
{
    public float initialSpeed = 5f;
    public float rotationSpeed = 2f;
    public GameObject projectilePrefab; 
    public GameObject laserPrefab;
    public GameObject missilePrefab;
    public GameObject assistantPrefab;
    public float initialFireRate = 0.5f; 
    public float backgroundScrollSpeed = 1f;


    private float speed;
    private float verticalBoundary;
    private float horizontalBoundary;
    private float nextFireTime;
    public static float fireRate;
    private GameObject background;

    private Image speedupImage;
    private Image missileImage;
    private Image laserImage;
    private Image optionImage;
    private Image barrierImage;

    private int powerUpCounter = 0;
    private bool laserOn;
    private int barrierLife;
    private bool missileOn;
    private int optionCounter;
    private int speedCounter;
    void Start()
    {
        Camera mainCamera = Camera.main;

        verticalBoundary = mainCamera.orthographicSize;
        horizontalBoundary = verticalBoundary * mainCamera.aspect;

        speed = initialSpeed;
        fireRate = initialFireRate;

        background = GameObject.Find("Background");

        speedupImage = GameObject.Find("SpeedupImage").GetComponent<Image>();
        missileImage = GameObject.Find("MissileImage").GetComponent<Image>();
        laserImage = GameObject.Find("LaserImage").GetComponent<Image>();
        optionImage = GameObject.Find("OptionImage").GetComponent<Image>();
        barrierImage = GameObject.Find("BarrierImage").GetComponent<Image>();
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

        // Powerup colors
        switch (powerUpCounter)
        {
            case 0:
                speedupImage.color = Color.blue;
                missileImage.color = Color.blue;
                laserImage.color = Color.blue;
                optionImage.color = Color.blue;
                barrierImage.color = Color.blue;
                break;
            case 1:
                speedupImage.color = Color.yellow;
                missileImage.color = Color.blue;
                laserImage.color = Color.blue;
                optionImage.color = Color.blue;
                barrierImage.color = Color.blue;
                break;
            case 2:
                speedupImage.color = Color.blue;
                missileImage.color = Color.yellow;
                laserImage.color = Color.blue;
                optionImage.color = Color.blue;
                barrierImage.color = Color.blue;
                break;
            case 3:
                speedupImage.color = Color.blue;
                missileImage.color = Color.blue;
                laserImage.color = Color.yellow;
                optionImage.color = Color.blue;
                barrierImage.color = Color.blue;
                break;
            case 4:
                speedupImage.color = Color.blue;
                missileImage.color = Color.blue;
                laserImage.color = Color.blue;
                optionImage.color = Color.yellow;
                barrierImage.color = Color.blue;
                break;
            case 5:
                speedupImage.color = Color.blue;
                missileImage.color = Color.blue;
                laserImage.color = Color.blue;
                optionImage.color = Color.blue;
                barrierImage.color = Color.yellow;
                break;
            default:
                speedupImage.color = Color.blue;
                missileImage.color = Color.blue;
                laserImage.color = Color.blue;
                optionImage.color = Color.blue;
                barrierImage.color = Color.blue;
                break;
        }
    }

    void ScrollBackground()
    {
        // Background scrolling
        float backgroundScroll = backgroundScrollSpeed * Time.deltaTime;
        background.transform.Translate(Vector3.left * backgroundScroll);

        if (background.transform.position.x < -horizontalBoundary * 2f)
        {
            background.transform.Translate(Vector3.right * (horizontalBoundary * 4f));
        }
    }

    public void Shoot()
    {
        GameObject[] assistants = GameObject.FindGameObjectsWithTag("Assistant");
        if (laserOn)
        {
            foreach (GameObject assistant in assistants)
            {
                GameObject laser = Instantiate(laserPrefab, assistant.transform.position, new Quaternion(0f, 0f, 0f, 0f));

                Vector3 laserDirection = assistant.transform.right;
                laser.GetComponent<Rigidbody>().velocity = laserDirection * speed * 2f;
            }

            GameObject blast = Instantiate(laserPrefab, transform.position, new Quaternion(0f, 0f, 0f, 0f));

            Vector3 blastDirection = transform.right;
            blast.GetComponent<Rigidbody>().velocity = blastDirection * speed * 2f;
        }
        else
        {
            foreach (GameObject assistant in assistants)
            {
                GameObject projectile = Instantiate(projectilePrefab, assistant.transform.position, new Quaternion(0f, 0f, 0f, 0f));

                Vector3 projectileDirection = assistant.transform.right;
                projectile.GetComponent<Rigidbody>().velocity = projectileDirection * speed * 2f;
            }

            GameObject bullet = Instantiate(projectilePrefab, transform.position, new Quaternion(0f,0f,0f,0f));

            Vector3 bulletDirection = transform.right;
            bullet.GetComponent<Rigidbody>().velocity = bulletDirection * speed * 2f;
        }
        if (missileOn)
        {
            foreach (GameObject assistant in assistants)
            {
                GameObject bomb = Instantiate(missilePrefab, assistant.transform.position, new Quaternion(0f, 0f, 0f, 0f));

                Vector3 bombDirection = assistant.transform.up;
                bomb.GetComponent<Rigidbody>().velocity = -bombDirection * speed * 2f;
            }

            GameObject missile = Instantiate(missilePrefab, transform.position, new Quaternion(0f, 0f, 0f, 0f));

            Vector3 missileDirection = transform.up;
            missile.GetComponent<Rigidbody>().velocity = -missileDirection * speed * 2f;
        }
        nextFireTime = Time.time + fireRate;
    }
    void ChooseAbility()
    {
        switch (powerUpCounter)
        {
            case 0:
                
                break;
            case 1:
                if (speedCounter < 6)
                {
                    SpeedUp();
                    powerUpCounter = 0;
                    speedCounter++;
                }

                break;
            case 2:
                if (!missileOn)
                {
                    Missile();
                    powerUpCounter = 0;
                }
                break;
            case 3:
                if (!laserOn)
                {
                    Laser();
                    powerUpCounter = 0;
                }
                break;
            case 4:
                if (optionCounter<=3)
                {
                    Option();
                    powerUpCounter = 0;
                }
                break;
            case 5:
                Barrier();
                powerUpCounter = 0;
                break;
            default:

                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (barrierLife<=0)
            {
                Destroy(gameObject);
            }
            else
            {
                barrierLife--;
            }
        }

        if (other.CompareTag("Energy"))
        {
            if (powerUpCounter == 5)
            {
                powerUpCounter = 1;
            }
            else
            {
                powerUpCounter++;
            }
            

        }
    }

    void SpeedUp()
    {
        speed *= 1.2f;
        fireRate /= 1.1f;

    }
    void Missile()
    {
        missileOn = true;
    }
    void Laser()
    {
        laserOn = true;
    }
    void Option()
    {
        Instantiate(assistantPrefab);
        optionCounter++;
    }
    void Barrier()
    {
        barrierLife = 10;
    }
}
