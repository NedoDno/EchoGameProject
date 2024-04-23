using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int durability = 40;  
    public GameObject dropPrefab;
    public float dropChance = 0.25f;

    public void TakeDamage(int damage)
    {
        durability -= damage;
        if (durability <= 0)
        {
            if (dropPrefab != null)
            {
                if (Random.value <= dropChance)
                {
                    Instantiate(dropPrefab, transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }
    }
}
