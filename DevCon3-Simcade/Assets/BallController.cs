using UnityEngine;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LaunchBall();
    }

    void LaunchBall()
    {
        // Launch the ball in a random direction
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, 1).normalized;
        rb.velocity = direction * initialSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Increase speed slightly on each hit for difficulty
        if (collision.gameObject.CompareTag("Racket"))
        {
            rb.velocity *= 1.1f;
        }
    }
}
