using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 15;
    float xSpeed;

    Rigidbody2D rb2d;
    PlayerMovement player;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        rb2d.velocity = new Vector2 (xSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Exit") { return; }
        Destroy(gameObject);
    }
}
