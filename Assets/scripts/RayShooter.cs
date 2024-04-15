using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayShooter : MonoBehaviour
{
    public Text killCountText;
    public GameObject collectiblePrefab;
    public GameObject impactPrefab;  // Prefab for the sphere that appears when ray hits a non-enemy object
    public float dropChance = 0.25f;
    private int killCount = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Shooting is triggered by the left mouse button
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))  // You might want to limit the ray range
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Destroy(hit.collider.gameObject);
                    killCount++;
                    UpdateKillCountText();

                    // Chance to drop a collectible
                    if (Random.value < dropChance)
                    {
                        Instantiate(collectiblePrefab, hit.collider.transform.position, Quaternion.identity);
                    }
                }
                else
                {
                    // Instantiate the impact prefab at the hit location and destroy it after 1 second
                    GameObject tempImpact = Instantiate(impactPrefab, hit.point, Quaternion.identity);
                    Destroy(tempImpact, 1.0f);
                }
            }
        }
    }

    void UpdateKillCountText()
    {
        killCountText.text = "Kills: " + killCount;
    }
}
