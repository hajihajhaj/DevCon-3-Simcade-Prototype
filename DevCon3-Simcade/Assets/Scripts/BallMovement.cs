using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    public float speed = 10f; // Ball speed
    public float paddleBounceMultiplier = 1.5f; // Multiplier for ball velocity after hitting the paddle
    public Transform paddle; // Reference to the paddle
    public ScoreManager scoreManager; // Reference to the ScoreManager
    private Rigidbody rb; // Reference to the Rigidbody component

    private int boundsHitCount = 0; // Tracks the number of times the ball hits the bounds
    private int maxBoundsHits = 3; // Maximum allowed hits before game over

    // Target launch position
    private Vector3 targetPosition = new Vector3(0, 0, -0.3129f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on the Ball!");
            return;
        }

        if (paddle == null)
        {
            Debug.LogError("No paddle assigned to the Ball!");
            return;
        }

        // Set the ball's initial position and launch
        ResetBall();
        LaunchBall();
    }

    void ResetBall()
    {
        // Reset ball position and velocity
        rb.velocity = Vector3.zero; // Stop any movement
        transform.position = new Vector3(paddle.position.x, paddle.position.y, -1.112f); // Set to starting position z = -1.112
    }

    void LaunchBall()
    {
        // Calculate a realistic launch direction
        Vector3 launchDirection = new Vector3(0, 1, 1).normalized; // Up and forward
        rb.velocity = launchDirection * speed; // Apply velocity
        Debug.Log("Ball launched with velocity: " + rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Ball hit the wall!");
        }

        if (collision.gameObject.CompareTag("Bounds"))
        {
            Debug.Log("Game Over!");
            HandleBoundsCollision();
        }

        if (collision.gameObject.CompareTag("Racket"))
        {
            HandlePaddleCollision(collision);
        }
    }

    void HandlePaddleCollision(Collision collision)
    {
        // Get the contact point of the collision
        ContactPoint contact = collision.GetContact(0);

        // Calculate the new velocity based on the direction of the collision
        Vector3 newDirection = (transform.position - contact.point).normalized;

        // Apply the new velocity with increased intensity
        rb.velocity = newDirection * speed * paddleBounceMultiplier;

        Debug.Log("Ball bounced off the paddle with velocity: " + rb.velocity);
    }

    void HandleBoundsCollision()
    {
        boundsHitCount++;

        if (boundsHitCount >= maxBoundsHits)
        {
            Debug.Log("Game Over! Max bounds hits reached.");
            ShowEndScreen();
        }
        else
        {
            Debug.Log($"Bounds hit {boundsHitCount}/{maxBoundsHits}");
            ResetBall(); // Reset the ball to the paddle position
            LaunchBall(); // Launch again automatically
        }
    }

    void ShowEndScreen()
    {
        SceneManager.LoadScene("EndScreen"); // Load the end screen scene
    }

    void FixedUpdate()
    {
        // Limit the ball's velocity to prevent it from going too fast
        if (rb.velocity.magnitude > speed * paddleBounceMultiplier)
        {
            rb.velocity = rb.velocity.normalized * speed * paddleBounceMultiplier;
        }
    }

    void LateUpdate()
    {
        if (transform.position.y < -5 || transform.position.z < -10 || transform.position.z > 10)
        {
            Debug.Log("Ball went out of bounds. Resetting...");
            ResetBall();
            LaunchBall();
        }
    }
}