using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    public float maxHealth = 100f;
    public float damageAmount = 25f;
    public float healAmount = 25f;

    public Slider healthBarSlider;
    public GameObject gameOverPanel;

    private float currentHealth;
    private bool isDead = false;
    private float damageCooldown = 0.5f;
    private float lastDamageTime = -1f;
    private SpriteRenderer spriteRenderer;

    void Awake() { Instance = this; }

    void Start()
    {
        currentHealth = maxHealth;
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
        gameOverPanel.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TakeDamage(float amount)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;
        if (isDead) return;

        lastDamageTime = Time.time;
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        healthBarSlider.value = currentHealth;

        if (CameraShake.Instance != null)
            CameraShake.Instance.Shake();

        if (currentHealth <= 0) StartCoroutine(DeathFlash());
    }

    public void Heal(float amount)
    {
        if (isDead) return;
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        healthBarSlider.value = currentHealth;
    }

    IEnumerator DeathFlash()
    {
        isDead = true;
        GetComponent<PlayerMovement>().SetGameOver();
        for (int i = 0; i < 4; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.green;
            yield return new WaitForSeconds(0.1f);
        }
        gameOverPanel.SetActive(true);
        ObstacleSpawner spawner = FindObjectOfType<ObstacleSpawner>();
        if (spawner != null) spawner.StopSpawning();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HealthPack"))
        {
            Heal(healAmount);
            Destroy(other.gameObject);
        }
    }
}