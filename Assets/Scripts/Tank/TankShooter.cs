using UnityEngine;

public class TankShooter : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private ParticleSystem[] muzzleFlashes;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip;
    
    [Header("Settings")]
    [SerializeField] private TankConfig config;
    [SerializeField] private Transform firePoint;

    private float nextFireTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryShoot();
    }

    private void TryShoot()
    {
        if (config == null || config.projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("TankShooter налаштований не повністю.");
            return;
        }

        if (Time.time < nextFireTime)
            return;

        Projectile projectile = Instantiate(config.projectilePrefab, firePoint.position, firePoint.rotation);

        projectile.Initialize(
            config.damage,
            config.projectileSpeed,
            config.projectileLifetime
        );

        if (muzzleFlashes[0] != null)
            foreach (ParticleSystem ps in muzzleFlashes)
                ps.Play();

        if (audioSource != null && shootClip != null)
            audioSource.PlayOneShot(shootClip);
        
        nextFireTime = Time.time + config.fireCooldown;
    }
}