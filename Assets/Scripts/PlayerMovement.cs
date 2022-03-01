using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float playerSpeed = 5;
    [SerializeField] float jumpSpeed = 2;
    [SerializeField] float climbSpeed = 2;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnPoint;
    

    Vector2 moveInput;
    Rigidbody2D rb2d;
    float gravityScaleAtStart;
    CapsuleCollider2D playerCollider2D;
    BoxCollider2D playerLegsCollider2D;
    Animator playerAnimator;
    PlayerInput playerInput;
    bool isPlayerDead;


    void Start()
    {
        
        rb2d = GetComponent<Rigidbody2D>();
        gravityScaleAtStart = rb2d.gravityScale;
        playerCollider2D = GetComponent<CapsuleCollider2D>();
        playerLegsCollider2D = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        isPlayerDead = false;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
        CheckIfAirborne();
        Die();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && playerLegsCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb2d.velocity += new Vector2 (0, jumpSpeed);
            
        }
        
    }

    void OnFire(InputValue value)
    {
        if (isPlayerDead) { return; }
        Instantiate(bullet, bulletSpawnPoint.position, transform.rotation);
        playerAnimator.SetTrigger("Shoot");
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * playerSpeed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            playerAnimator.SetBool("isRunning", true);
            transform.localScale = new Vector2 (Mathf.Sign(rb2d.velocity.x), 1);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
        
    }

    void ClimbLadder()
    {

        bool playerHasVerticalSpeed = Mathf.Abs(rb2d.velocity.y) > Mathf.Epsilon;

        if (playerLegsCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb2d.gravityScale = 0;
            Vector2 climbVelocity = new Vector2 (rb2d.velocity.x, moveInput.y * climbSpeed);
            rb2d.velocity = climbVelocity;
            playerAnimator.SetBool("isClimbing", false);

            if (rb2d.velocity.y > 0 || rb2d.velocity.y < 0)
            {
                playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
            }
            
        }
        else
        {
            rb2d.gravityScale = gravityScaleAtStart;
            playerAnimator.SetBool("isClimbing", false);
        }

        

    }

    void Die() 
    {
        if (rb2d.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isPlayerDead = true;
            playerInput.enabled = false;

            playerAnimator.SetTrigger("Death");
            rb2d.velocity = new Vector2(80, 15);
            playerCollider2D.enabled = false;
            playerLegsCollider2D.enabled = false;

            StartCoroutine(FindObjectOfType<GameSession>().ProcessPlayerDeath());
        }
    }

    void CheckIfAirborne()
    {
        if (!playerLegsCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground", "Ladder")))
        {
            playerAnimator.SetBool("isAirborne", true);
        }
        else
        {
            playerAnimator.SetBool("isAirborne", false);
        }

        if (isPlayerDead)
        {
            playerAnimator.SetBool("isAirborne", false);
            return;
        }
    }

    

}