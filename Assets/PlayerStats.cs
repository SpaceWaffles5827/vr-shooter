using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damageTaken);
}

public class PlayerStats : MonoBehaviour, IDamageable
{
    private int currentHealth = 100;
    private int maxHealth = 100;

    [SerializeField]
    private GameObject objectWithMaterial; // Reference to the object with the material you want to change.
    [SerializeField]
    private Transform respawnPoint; // The point where the player should respawn.

    private Renderer objRenderer; // Store the renderer of that object.

    private void Start()
    {
        if (objectWithMaterial)
        {
            objRenderer = objectWithMaterial.GetComponent<Renderer>();
        }
        UpdateMaterialColor();
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        Debug.Log(currentHealth);
        UpdateMaterialColor();

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        currentHealth = maxHealth; // Reset health
        transform.position = respawnPoint.position; // Move player to respawn point
        Debug.Log("Player respawned.");
        UpdateMaterialColor(); // Ensure the color gets updated after respawning
    }

    private void UpdateMaterialColor()
    {
        if (objRenderer)
        {
            float healthPercentage = (float)currentHealth / maxHealth;
            Color currentColor = Color.Lerp(Color.red, Color.green, healthPercentage);
            objRenderer.material.color = currentColor;
        }
    }
}
