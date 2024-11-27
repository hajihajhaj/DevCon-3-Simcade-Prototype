using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    public float speed = 10f; // Ball speed
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
        // Launch the ball from paddle toward the wall
        Vector3 launchDirection = (new Vector3(0, 0.1f, -0.3129f) - transform.position).normalized; // Slightly upward and forward
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
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
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
