using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int hp = 10;
    GameObject player;  
    public float attackDistance = 1.0f; 
    public int attackDamage = 5;  
    public float attackCooldown = 3.0f;
    public Animator animator;

    private float lastAttackTime = 0;  
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                animator.SetTrigger("attack"); 
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
