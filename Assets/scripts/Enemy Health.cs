using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int hp = 10;
    public GameObject collectiblePrefab;  // Prefab for collectibles dropped upon death
    public float dropChance = 0.25f;  // Chance to drop a collectible
    GameObject player;  
    public float attackDistance = 5.0f;  // Distance within which the enemy can attack
    public int attackDamage = 5;  // Damage dealt to the player
    public float attackCooldown = 3.0f;  // Time between attacks

    private float lastAttackTime = 0;  // Time when last attack was made
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            DropCollectible();
            Destroy(gameObject);
        }
    }

    void DropCollectible()
    {
        if (Random.value < dropChance)
        {
            Instantiate(collectiblePrefab, transform.position, Quaternion.identity);
        }
    }

    void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
