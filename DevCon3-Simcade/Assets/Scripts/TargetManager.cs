using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject[] targetPrefabs; // Array of different target prefabs (green, gold, normal).
    public GameObject wall; // Reference to the wall.
    public float spawnInterval = 2f; // Interval between spawns.
    private float gameTime = 0f; // Track the game time.
    public float sizeReductionRate = 0.1f; // Rate at which target size decreases over time.
    public int maxTargets = 10; // Maximum targets allowed at a time.
    private List<GameObject> activeTargets = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnTargets());
    }

    IEnumerator SpawnTargets()
    {
        while (true)
        {
            if (activeTargets.Count < maxTargets)
            {
                SpawnTarget();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnTarget()
    {
        // Randomly select a target prefab
        GameObject targetPrefab = targetPrefabs[Random.Range(0, targetPrefabs.Length)];

        // Instantiate the target
        GameObject newTarget = Instantiate(targetPrefab);

        // Set the scale (size) within the specified limits
        float size = Random.Range(0.004499463f, 0.02842327f);
        newTarget.transform.localScale = new Vector3(size, size, size);

        // Get the wall's position and scale for bounds
        Vector3 wallPosition = wall.transform.position;
        Vector3 wallScale = wall.transform.localScale;

        // Ensure targets spawn within the wall’s bounds and in front of the wall
        float halfWidth = wallScale.x / 2f;   // Half the width of the wall
        float halfHeight = wallScale.y / 2f; // Half the height of the wall
        float depthOffset = 0.05f;            // Distance in front of the wall

        // Random position within the wall bounds
        Vector3 spawnPosition = new Vector3(
            Random.Range(wallPosition.x - halfWidth, wallPosition.x + halfWidth),
            Random.Range(wallPosition.y - halfHeight, wallPosition.y + halfHeight),
            wallPosition.z - depthOffset
        );

        // Check for overlapping positions (prevents targets from spawning on top of each other)
        Collider[] colliders = Physics.OverlapSphere(spawnPosition, size * 1.5f); // Adjust multiplier if needed
        if (colliders.Length > 0)
        {
            Destroy(newTarget); // Prevent spawning if overlapping
            return;
        }

        // Set the position and rotation of the new target
        newTarget.transform.position = spawnPosition;
        newTarget.transform.rotation = Quaternion.Euler(90, 0, 180);
    }





    void Update()
    {
        // Increment game time
        gameTime += Time.deltaTime;
    }

    public void OnTargetHit(GameObject target)
    {
        // Handle logic when a target is hit
        // For example, destroy the target
        Destroy(target);

        // Add any additional logic for scoring or spawning new targets here
    }

}
