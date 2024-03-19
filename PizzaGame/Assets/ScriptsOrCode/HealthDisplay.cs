using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Sprite[] healthSprites; // Array to hold all health sprites

    SpriteRenderer spriteRenderer;
    PlayerController_V2 playerController;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = FindObjectOfType<PlayerController_V2>(); // Find the PlayerController_V2 component in the scene
        UpdateHealthSprite(); // Update the health sprite initially
    }

    void Update()
    {
        UpdateHealthSprite(); // Update the health sprite continuously
    }

    void UpdateHealthSprite()
    {
        int health = playerController.currentHealth; // Get the current health of the player

        // Ensure the health value is within the range of available sprites
        int spriteIndex = Mathf.Clamp(health, 0, healthSprites.Length - 1);

        // Set the sprite renderer's sprite to the corresponding health sprite
        spriteRenderer.sprite = healthSprites[spriteIndex];
    }
}
