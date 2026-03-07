using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject healthPackPrefab;
    public GameObject enemyPrefab;

    public float spawnInterval = 2.5f;
    public float scrollSpeed = 3f;
    public float maxSpeed = 8f;
    public float speedIncreaseRate = 0.05f;

    private bool isSpawning = true;
    private float timer = 0f;
    private GroundScroll groundScroll;

    void Start()
    {
        groundScroll = GetComponent<GroundScroll>();
    }

    void Update()
    {
        if (!isSpawning) return;

        scrollSpeed = Mathf.Min(
            scrollSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);

        if (groundScroll != null)
            groundScroll.UpdateSpeed(scrollSpeed);

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObstacles();
        }
    }

    void SpawnObstacles()
    {
        if (wallPrefab != null)
        {
            GameObject wall = Instantiate(wallPrefab,
                new Vector3(12f, -1.5f, 0), Quaternion.identity);
            wall.AddComponent<ScrollLeft>().speed = scrollSpeed;
        }

        if (healthPackPrefab != null && Random.value < 0.3f)
        {
            GameObject pack = Instantiate(healthPackPrefab,
                new Vector3(14f, -2.8f, 0), Quaternion.identity);
            pack.AddComponent<ScrollLeft>().speed = scrollSpeed;
        }

        if (enemyPrefab != null && Random.value < 0.4f)
        {
            GameObject enemy = Instantiate(enemyPrefab,
                new Vector3(15f, -3.2f, 0), Quaternion.identity);
            enemy.AddComponent<ScrollLeft>().speed = scrollSpeed;
        }
    }

    public float GetCurrentSpeed() { return scrollSpeed; }

    public void StopSpawning()
    {
        isSpawning = false;
        if (groundScroll != null)
            groundScroll.UpdateSpeed(0);
    }
}