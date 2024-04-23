using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayShooter : MonoBehaviour
{
    public GameObject lightBulletPrefab; // Prefab of the light particle
    public Transform shootingPoint; // Point from which bullets are shot, typically the camera center
    public Camera playerCamera;
    public GameObject aimScope; // UI Aim/Scope to enable when the weapon is held
    public GameObject lamp; // Reference to the lamp GameObject
    public bool hasWeapon = true;
    public float shootingDelay = 0.5f;
    private float lastShootTime;

    void Start()
    {
        aimScope.SetActive(true); // Ensure the aim is not visible until the weapon is picked up
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Attempt to toggle weapon use
            if (!IsLampCarried()) // Check if the lamp is not being carried
            {
                hasWeapon = !hasWeapon;
                aimScope.SetActive(hasWeapon);
                if (hasWeapon)
                {
                    // Optional: Add sound of picking up a weapon
                }
                else
                {
                    // Optional: Add sound of putting away a weapon
                }
            }
        }

        if (hasWeapon && Input.GetMouseButtonDown(0) && Time.time > lastShootTime + shootingDelay)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            GameObject lightBullet = Instantiate(lightBulletPrefab, shootingPoint.position, Quaternion.identity);
            lightBullet.transform.position = hit.point;
            Destroy(lightBullet, 1.0f);

            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(10);
                }
            }
        }
    }

    bool IsLampCarried()
    {
        // Check if the lamp is a child of the player
        return lamp.transform.parent == transform;
    }
}
