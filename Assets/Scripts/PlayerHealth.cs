using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [Tooltip("The maximum health the player can have.")]
    public float maxHealth = 100f;
    
    [Tooltip("The current health of the player.")]
    public float currentHealth;

    [Header("Regeneration Settings")]
    [Tooltip("The amount of health regenerated per second.")]
    public float regenerationRate = 1f;

    void Start()
    {
        // Initialize current health to max health at the start of the game
        currentHealth = maxHealth;
        Debug.Log($"Player health initialized: {currentHealth}/{maxHealth}");
    }

    void Update()
    {
        // Regenerate health over time, but don't exceed max health
        if (currentHealth < maxHealth)
        {
            currentHealth += regenerationRate * Time.deltaTime;
            // Ensure health doesn't go above maxHealth
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }

        // Optional: Log health for debugging purposes
        // Debug.Log($"Current Health: {currentHealth}");
    }

    /// <summary>
    /// Reduces the player's current health by a specified amount.
    /// </summary>
    /// <param name="amount">The amount of damage to take.</param>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        Debug.Log($"Player took {amount} damage. Current Health: {currentHealth}/{maxHealth}");
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // Implement game over logic here (e.g., reload scene, show game over screen)
        // For now, we'll just destroy the player object as an example.
        // Destroy(gameObject); 
    }
}