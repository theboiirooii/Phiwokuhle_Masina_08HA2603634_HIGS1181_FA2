using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Global: how fast the player moves, in units per second
    [SerializeField] private float moveSpeed = 5f;
    // Global: fixed spot the player returns to after touching a hazard
    public Transform startPosition;
    // Global: reference to the GameManager that holds score/health
    public GameManager gameManager;
    // Global: camera that follows the player (used by CameraFollow)
    public Transform cameraTransform;
    // Global: the player object the camera follows
    public Transform playerTarget;
    // Global: how fast the camera catches up with the player (tutorial used 3)
    // I chose 8 because the player moves at 5, so the camera keeps up without lagging
    public float camFollowSpeed = 8f;
    // Global: keeps the camera 10 units back so the 2D scene stays visible
    public Vector3 camOffset = new Vector3(0f, 0f, -10f);

    void Start()
    {
        // Local: the player's sprite, fetched at runtime so I can draw it above the pickups
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        playerSprite.sortingOrder = 10;
    }

    void Update()
    {
        Move();
    }

    // LateUpdate runs AFTER the player moves, preventing camera stutter/jitter
    void LateUpdate()
    {
        CameraFollow();
    }

    private void Move()
    {
        if (gameManager.isGameOver) return;

        float x = 0f;
        float y = 0f;
        if (Input.GetKey(KeyCode.W)) y = 1f;
        if (Input.GetKey(KeyCode.S)) y = -1f;
        if (Input.GetKey(KeyCode.A)) x = -1f;
        if (Input.GetKey(KeyCode.D)) x = 1f;

        transform.Translate(new Vector3(x, y, 0f).normalized * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage()
    {
        Debug.Log("Player took damage");
        gameManager.ReduceHealth();
        transform.position = startPosition.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameManager.isGameOver) return;

        if (other.CompareTag("Collectible"))
        {
            Debug.Log("Collectible collected");
            Destroy(other.gameObject);
            gameManager.AddScore();
        }
        else if (other.CompareTag("Hazard"))
        {
            TakeDamage();
        }
    }

    // Sourced from: "Controlling Unity Camera Behaviour", Unity Learn (https://learn.unity.com/tutorial/controlling-unity-camera-behaviour), adapted by phiwokuhle Masina.
    private void CameraFollow()
    {
        cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, playerTarget.position + camOffset, camFollowSpeed * Time.deltaTime);
    }
}