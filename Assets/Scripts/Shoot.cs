using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootCooldown = 0.3f;
    private float lastShootTime = -1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastShootTime < shootCooldown) return;
            lastShootTime = Time.time;
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }
}