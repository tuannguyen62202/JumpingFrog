using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    public Transform player;                  
    public GameObject projectilePrefab;       
    public Transform firePoint;               

    [Header("Enemy Settings")]
    public float detectionRange = 10f;     
    public float fireCooldown = 2f;           
    public float projectileSpeed = 5f;        

    private float fireTimer = 0f;
    public int health;
    public int maxHealth = 100;
    Animator animator;
    public int scoreToAdd = 10;

    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();

        // Force player assignment at runtime, ignore prefab-assigned player
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    void Update()
    {
        if (player == null)
            return;

        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
 
            Vector2 direction = (player.position - transform.position).normalized;

            // Fire projectile if cooldown allows
            if (fireTimer <= 0f)
            {
                FireProjectile(direction);
                fireTimer = fireCooldown;
            }
        }

        // Countdown fire timer
        fireTimer -= Time.deltaTime;
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
    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hit Trigger");
        health -= damage;

        if (health <= 0)
        {
            var playerScript = player.GetComponent<Player>();
            if (playerScript == null)
            {
 
            }
            else
            {
                playerScript.UpdateScore(scoreToAdd);
                
            }

            Destroy(gameObject);
        }
    }

}
