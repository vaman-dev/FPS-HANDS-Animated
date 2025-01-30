using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    public Animator animator; // Assign in Inspector
    public Camera playerCamera;
    public AudioSource gunFireSound; // Assign in Inspector
    public ParticleSystem muzzleFlash; // Assign in Inspector
    public Light gunFlashLight; // Assign in Inspector
    public BulletManager bulletManager; // Reference to BulletManager script

    public Transform firingPoint; // Assign in Inspector (where bullets spawn)
    public GameObject bulletPrefab; // Bullet to instantiate
    public float bulletSpeed = 50f; // Speed of the bullet

    public float fireRate = 0.2f; // Fire rate in seconds (adjust as needed)
    private float nextFireTime = 0f; // Tracks when the player can shoot again
    private float flashDuration = 0.05f; // How long the light stays on

    void Update()
    {
        // Right-click to aim
        animator.SetBool("isAiming", Input.GetMouseButton(1));

        // Left-click to shoot (Only if aiming, fire rate cooldown allows, and has bullets)
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime && bulletManager.CanShoot())
        {
            nextFireTime = Time.time + fireRate; // Set next fire time
            animator.SetBool("Shoot", true);
            bulletManager.FireBullet(); // Reduce ammo count
            PlayGunEffects();
            FireProjectile();
        }
        else
        {
            animator.SetBool("Shoot", false);
        }

        // Reload when pressing 'R'
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(bulletManager.Reload());
        }
    }

    void PlayGunEffects()
    {
        if (gunFireSound != null)
        {
            gunFireSound.Play(); // Play gunfire sound
        }

        if (muzzleFlash != null)
        {
            muzzleFlash.Play(); // Play muzzle flash effect
        }

        if (gunFlashLight != null)
        {
            StartCoroutine(FlashGunLight());
        }
    }

    IEnumerator FlashGunLight()
    {
        gunFlashLight.enabled = true; // Turn on the light
        yield return new WaitForSeconds(flashDuration); // Wait for 0.05 seconds
        gunFlashLight.enabled = false; // Turn off the light
    }

    void FireProjectile()
    {
        if (bulletPrefab != null && firingPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = firingPoint.forward * bulletSpeed; // Shoot in the firing direction
            }
        }
    }
}
