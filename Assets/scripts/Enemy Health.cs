using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int hp = 10;
    GameObject player;  
    public float attackDistance = 5.0f; 
    public int attackDamage = 5;  
    public float attackCooldown = 3.0f; 

    private float lastAttackTime = 0;  
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

    void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
