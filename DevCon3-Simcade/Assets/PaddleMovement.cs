using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public Camera mainCamera; 
    public float paddleDepth = 0.8f; 
    public Vector2 xLimits = new Vector2(-1.0f, 1.0f); 
    public Vector2 yLimits = new Vector2(0.5f, 1.5f);

    void Start()
    {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Confined; 
    }

    void Update()
    {
        
        Vector3 mousePosition = Input.mousePosition;

        
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(
            mousePosition.x,
            mousePosition.y,
            mainCamera.WorldToScreenPoint(new Vector3(0, 0, paddleDepth)).z 
        ));

        
        worldPosition.x = Mathf.Clamp(worldPosition.x, xLimits.x, xLimits.y);
        worldPosition.y = Mathf.Clamp(worldPosition.y, yLimits.x, yLimits.y);
        worldPosition.z = paddleDepth; 

        
        transform.position = worldPosition;
    }
}
