using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float enemyMoveSpeed = 1;

    Rigidbody2D rb2d;
    ParticleSystem particlesOnDeath;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider2D;
    BoxCollider2D boxCollider2D;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        particlesOnDeath = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        rb2d.velocity = new Vector2(enemyMoveSpeed, 0);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Coin") { return; }
        enemyMoveSpeed = -enemyMoveSpeed;
        FlipEnemyFacing();    
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2 (Mathf.Sign(rb2d.velocity.x) * Mathf.Sign(transform.localScale.x) * transform.localScale.x, transform.localScale.y);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Bullet")
        {
            HandleDeath();
            Destroy(other.gameObject);
        }
    }

    void HandleDeath()
    {
        particlesOnDeath.Play();

        capsuleCollider2D.enabled = false;
        boxCollider2D.enabled = false;
        spriteRenderer.enabled = false;
        Destroy(GetComponent<EnemyMovement>());
        
    }
}
