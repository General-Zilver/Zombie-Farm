using UnityEngine;

public class ZombieChase : MonoBehaviour
{
    // The speed at which the zombie moves
    public float moveSpeed = 2f; // Adjusted for better balance with attack

    [Header("Attack Settings")]
    [Tooltip("How close the zombie needs to be to the player to start attacking.")]
    public float attackRange = 1.5f;
    [Tooltip("The amount of damage the zombie deals per attack.")]
    public float damageAmount = 10f;
    [Tooltip("The time in seconds between consecutive attacks.")]
    public float attackCooldown = 2f;

    // A reference to the player's transform
    private Transform playerTransform;
    // A reference to the player's health component
    private PlayerHealth playerHealth;
    // Timer to track when the zombie can attack next
    private float lastAttackTime;

    void Start()
    {
        // Find the player GameObject by its tag.
        // Make sure your player GameObject has the tag "Player" in Unity!
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            playerTransform = player.transform;
            // Get the PlayerHealth component from the player
            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("Player GameObject with tag 'Player' found but does not have a PlayerHealth component! Zombie cannot deal damage.");
            }
        }
        else
        {
            Debug.LogWarning("Player GameObject with tag 'Player' not found. Zombie will not chase or attack.");
        }

        // Initialize lastAttackTime to allow immediate attack when first encountering player
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        // Ensure both player transform and health component are found
        if (playerTransform != null && playerHealth != null)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // Make the zombie look at the player
            transform.LookAt(playerTransform.position);

            // If the player is outside the attack range, move towards them
            if (distanceToPlayer > attackRange)
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            // If the player is within attack range, try to attack
            else
            {
                // Check if enough time has passed since the last attack
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    playerHealth.TakeDamage(damageAmount);
                    lastAttackTime = Time.time; // Reset the attack timer
                }
            }
        }
    }
}