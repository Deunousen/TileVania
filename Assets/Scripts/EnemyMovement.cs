using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float enemyMoveSpeed = 1;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
}
