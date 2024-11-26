using UnityEngine;

public class Target : MonoBehaviour
{
    public string targetType; // Green, Gold, or Other

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            FindObjectOfType<TargetManager>().OnTargetHit(gameObject);

        }
    }
}
