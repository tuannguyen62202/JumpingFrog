using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool grounded = true;
    private Rigidbody2D rb2d;
    public float jumpPower;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask platformsLayerMask;
    public float speed = 10f;
    private Rigidbody2D rb;
    private int score = 0;
    private int jumpCount;
    public int jumpCountMax;
    private SpriteRenderer spriteRenderer;
    public float health;
    private float maxHealth;
    public GameObject gameManager;
    Animator animator;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireCooldown = 1f;
    public float projectileSpeed = 5f;
    private float fireCooldownTimer = 0f;



    public int maxBullets = 50;       // Maximum bullets
    private int currentBullets;

    void Start()
    {
        rb2d = rb2d = GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        jumpCountMax = 2;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        maxHealth = health;
        animator = GetComponent<Animator>();
        currentBullets = maxBullets;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right arrows

        // Move in air or on ground
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        // Flip the sprite based on movement direction
        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (IsGrounded())
        {
            jumpCount = 1;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (IsGrounded())
            {
                rb2d.linearVelocity = Vector2.up * jumpPower;

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (jumpCount < jumpCountMax)
                    {
                        animator.SetTrigger("Double Jump Trigger");
                        rb2d.linearVelocity = Vector2.up * jumpPower;
                        jumpCount++;
                    }
                }
            }
        }
        // Fire projectile with F key (and cooldown and bullets)
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (fireCooldownTimer <= 0f && currentBullets > 0)
            {
                
                FireProjectile(Vector2.up);
                fireCooldownTimer = fireCooldown;
                currentBullets--;    // Decrease bullet count
                Debug.Log("Bullets left: " + currentBullets);
            }
            else if (currentBullets <= 0)
            {
                Debug.Log("Out of bullets! Cannot fire.");
            }
        }

        // Countdown cooldown timer
        fireCooldownTimer -= Time.deltaTime;
    }


    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        gameManager.GetComponent<UIManager>().UpdateScoreUI(score);
        FindObjectOfType<GameManager>().currentScore += scoreToAdd;
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit Trigger");
        health -= damage;
        Debug.Log("Player took " + damage);
        if (health <= 0)
        {
            gameManager.GetComponent<GameManager>().EndGame();

        }

        gameManager.GetComponent<UIManager>().UpdateHealthUI(health);
    }

    public void BoostJump(float boostPower)
    {
        rb2d.linearVelocity = Vector2.up * boostPower;
    }
    void FireProjectile(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
    }
}