using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 10f;

    private Rigidbody rb;
    private bool hasLaunched = false; // Flag to track if the ball has launched

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // Ensure gravity is enabled
        LaunchBall();
    }

    void LaunchBall()
    {
        // Launch the ball in a random direction
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, 1).normalized;
        rb.velocity = direction * initialSpeed;
        hasLaunched = true; // Mark the ball as launched
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision with the "Racket"
        if (collision.gameObject.CompareTag("Racket"))
        {
            rb.velocity *= 1.1f; // Increase speed slightly
        }

        if (collision.gameObject.name == "Bounds")
        {
            SceneManager.LoadScene("EndScreen EDIT THIS (NOT WORKING)"); // Load the Game Over scene
        }
    }
}
