using UnityEngine;

public class BallAudio : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the Audio Source component
    public AudioClip bounceSound; // The bounce sound to play

    void Start()
    {
        // Ensure the AudioSource is assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the ball collided with the table or wall
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Table"))
        {
            PlayBounceSound(); // Play the bounce sound
        }
    }

    private void PlayBounceSound()
    {
        if (audioSource != null && bounceSound != null)
        {
            audioSource.PlayOneShot(bounceSound); // Play the sound effect
        }
        else
        {
            Debug.LogWarning("AudioSource or BounceSound is not assigned!");
        }
    }
}
