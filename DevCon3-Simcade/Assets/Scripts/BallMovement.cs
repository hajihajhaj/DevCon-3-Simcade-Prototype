using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 10f; // Ball speed
    private Rigidbody rb; // Reference to the Rigidbody component
    public ScoreManager scoreManager; // Reference to the ScoreManager

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on the Ball!");
            return;
        }

        LaunchBall();
    }

    void LaunchBall()
    {
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, 1).normalized; // Random direction
        rb.velocity = direction * speed; // Set the ball's velocity
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Racket"))
        {
            Vector3 bounceDirection = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = bounceDirection * speed; 

            speed *= 1.05f; 

            if (scoreManager != null)
            {
                scoreManager.AddScore(10); // Add points for hitting the racket
            }
        }

        if (collision.gameObject.CompareTag("Bounds"))
        {
            Debug.Log("Game Over! Ball hit the bounds.");
        }
    }
}
