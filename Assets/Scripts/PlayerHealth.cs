using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public CameraShake cameraShake;
    public DamageFlash damageFlash;
    public GameObject gameOverPanel;

    private bool isDead = false;
    private float damageCooldown = 0f;
    private const float DAMAGE_INTERVAL = 0.5f;

    [Header("Audio")]
    public AudioClip hurtSound;
    public AudioClip fallSound;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
            healthSlider.value = 1f;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (damageCooldown > 0) damageCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && isDead)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (transform.position.y < -8f)
        {
            if (audioSource != null && fallSound != null)
                audioSource.PlayOneShot(fallSound);
            TakeDamage(maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead || damageCooldown > 0) return;
        damageCooldown = DAMAGE_INTERVAL;
        currentHealth -= amount;
        if (audioSource != null && hurtSound != null)
            audioSource.PlayOneShot(hurtSound);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null)
            healthSlider.value = (float)currentHealth / maxHealth;

        if (cameraShake != null) cameraShake.Shake();
        if (damageFlash != null) damageFlash.Flash();

        if (currentHealth <= 0) Die();
    }

    public void Heal(int amount)
    {
        if (isDead) return;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (healthSlider != null)
            healthSlider.value = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        isDead = true;
        GetComponent<PlayerMovement>().SetDead(true);

        Shoot shoot = GetComponent<Shoot>();
        if (shoot != null) shoot.enabled = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }
}