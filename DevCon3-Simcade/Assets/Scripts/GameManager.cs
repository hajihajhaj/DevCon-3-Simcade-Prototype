using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Ball Settings")]
    public Rigidbody ballRigidbody;
    public float ballSpeed = 10f;
    public float maxBallSpeed = 15f;

    [Header("Paddle Settings")]
    public Transform paddle;
    public float paddleSpeed = 10f;
    public float paddleBoundary = 7f;

    [Header("Game Settings")]
    public int score = 0;
    public int maxScore = 10;

    private Vector3 ballStartPos;
    private Vector3 paddleStartPos;

    void Start()
    {
        ballStartPos = ballRigidbody.transform.position;
        paddleStartPos = paddle.position;
        LaunchBall();
    }

    void Update()
    {
        HandlePaddleMovement();
        ClampBallSpeed();
    }

    void LaunchBall()
    {
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.transform.position = ballStartPos;

        Vector3 randomDirection = new Vector3(Random.Range(-0.5f, 0.5f), 0f, 1f).normalized;
        ballRigidbody.velocity = randomDirection * ballSpeed;
    }

    void HandlePaddleMovement()
    {
        float input = Input.GetAxis("Horizontal"); // A/D or Arrow Keys
        Vector3 newPosition = paddle.position + Vector3.right * input * paddleSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -paddleBoundary, paddleBoundary);
        paddle.position = newPosition;
    }

    void ClampBallSpeed()
    {
        if (ballRigidbody.velocity.magnitude > maxBallSpeed)
        {
            ballRigidbody.velocity = ballRigidbody.velocity.normalized * maxBallSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Reflect the ball when it hits the paddle
            Vector3 reflection = Vector3.Reflect(ballRigidbody.velocity.normalized, collision.contacts[0].normal);

            float offset = (ballRigidbody.position.x - paddle.position.x) * 0.5f;
            reflection.x += offset;

            ballRigidbody.velocity = reflection.normalized * ballSpeed;
        }
        else if (collision.gameObject.CompareTag("Bounds"))
        {
            // Reset ball when it goes out of bounds
            Debug.Log("Ball out of bounds!");
            LaunchBall();
        }
        else if (collision.gameObject.CompareTag("Target"))
        {
            // Destroy target and update score
            score++;
            Destroy(collision.gameObject);

            if (score >= maxScore)
            {
                Debug.Log("You Win!");
                SceneManager.LoadScene("EndScreen");
            }
        }
    }
}