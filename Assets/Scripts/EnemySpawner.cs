using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject healthPackPrefab;

    public float spawnInterval = 5f;
    public float minInterval = 2f;
    public float intervalDecreaseRate = 0.05f;
    public float enemyBaseSpeed = 2f;
    public float enemyMaxSpeed = 6f;
    public float enemySpeedIncreaseRate = 0.01f;

    private float timer;

    // Y positions that match your platform surface heights
    // Edit these values to match where your platforms actually sit
    public float[] platformYPositions = { -2.5f, -2f, -1.8f, -2.3f, -2.5f };

    // How far off the right edge enemies spawn
    public float spawnX = 12f;

    void Start() { timer = spawnInterval; }

    void Update()
    {
        // Decrease spawn interval over time
        if (spawnInterval > minInterval)
            spawnInterval -= intervalDecreaseRate * Time.deltaTime;

        // Increase enemy walk speed over time
        if (enemyBaseSpeed < enemyMaxSpeed)
            enemyBaseSpeed += enemySpeedIncreaseRate * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = spawnInterval;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // We calculate the X position to spawn at
        float spawnXPos = spawnX;

        // Try to find the exact top surface of a platform by shooting a raycast straight down from the top of the level
        Vector2 rayStart = new Vector2(spawnXPos, 15f); // Start high up
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 30f, LayerMask.GetMask("Ground"));

        // If we hit a gap at spawnX, we will shift slightly to the left until we find a platform
        for (int i = 0; i < 10 && hit.collider == null; i++)
        {
            spawnXPos -= 1f;
            rayStart = new Vector2(spawnXPos, 15f);
            hit = Physics2D.Raycast(rayStart, Vector2.down, 30f, LayerMask.GetMask("Ground"));
        }

        if (hit.collider != null)
        {
            // Found a platform! Calculate the spawn position right on top of its surface
            // We add 0.5f to Y so the enemy spawns on top and doesn't clip into the ground
            Vector3 spawnPos = new Vector3(spawnXPos, hit.point.y + 0.5f, 0);

            // Spawn enemy
            GameObject e = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            e.GetComponent<Enemy>().walkSpeed = enemyBaseSpeed;

            // 25% chance to also spawn a health pack nearby on the same surface level
            if (Random.Range(0, 4) == 0)
            {
                Vector3 hpPos = new Vector3(spawnXPos - 2f, hit.point.y + 0.5f, 0);
                Instantiate(healthPackPrefab, hpPos, Quaternion.identity);
            }
        }
    }
}