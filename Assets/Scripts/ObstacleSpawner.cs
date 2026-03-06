using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject wallPrefab;
    public float spawnInterval = 2.5f;
    public float scrollSpeed = 3f;
    public float maxSpeed = 8f;
    public float speedIncreaseRate = 0.05f;
    public float gapVerticalRange = 2f;

    public GameObject healthPackPrefab;
    public GameObject enemyPrefab;

    private bool isSpawning = true;
    private float timer = 0f;

    void Update()
    {
        if (!isSpawning) return;

        scrollSpeed = Mathf.Min(scrollSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnWall();
        }
    }

    void SpawnWall()
    {
        float randomY = Random.Range(-gapVerticalRange, gapVerticalRange);
        Vector3 spawnPos = new Vector3(12f, randomY, 0);
        GameObject wall = Instantiate(wallPrefab, spawnPos, Quaternion.identity);
        wall.AddComponent<ScrollLeft>().speed = scrollSpeed;

        if (healthPackPrefab != null && Random.value < 0.3f)
        {
            float packY = Random.Range(-3f, 3f);
            GameObject pack = Instantiate(healthPackPrefab, new Vector3(12f, packY, 0), Quaternion.identity);
            pack.AddComponent<ScrollLeft>().speed = scrollSpeed;
        }

        if (enemyPrefab != null && Random.value < 0.4f)
        {
            float enemyY = Random.Range(-2.5f, 2.5f);
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(13f, enemyY, 0), Quaternion.identity);
            enemy.AddComponent<ScrollLeft>().speed = scrollSpeed;
        }
    }

    public float GetCurrentSpeed() { return scrollSpeed; }

    public void StopSpawning() { isSpawning = false; }
}