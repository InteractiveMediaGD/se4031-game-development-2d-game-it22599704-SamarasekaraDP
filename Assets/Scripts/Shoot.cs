using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject spiritOrbPrefab;
    public float cooldown = 0.3f;
    private float timer = 0f;

    [Header("Audio")]
    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timer <= 0f)
        {
            timer = cooldown;
            FireOrb();
        }
    }

    void FireOrb()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;
        Vector2 direction = (mouseWorld - transform.position).normalized;

        GameObject orb = Instantiate(spiritOrbPrefab, transform.position, Quaternion.identity);
        orb.GetComponent<SpiritOrb>().SetDirection(direction);

        if (audioSource != null && shootSound != null)
            audioSource.PlayOneShot(shootSound);
    }
}
