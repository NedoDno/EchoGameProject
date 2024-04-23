using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayShooter : MonoBehaviour
{
    public GameObject lightBulletPrefab; 
    public Transform shootingPoint; 
    public Camera playerCamera;
    public GameObject aimScope; 
    public GameObject lamp; 
    public bool hasWeapon = true;
    public float shootingDelay = 0.5f;
    private float lastShootTime;

    void Start()
    {
        aimScope.SetActive(true); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!IsLampCarried()) 
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

            if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("SpawnPoint"))
            {
                hit.collider.GetComponent<Damageable>().TakeDamage(10);
            }
        }
    }

    bool IsLampCarried()
    {
        return lamp.transform.parent == transform;
    }
}
