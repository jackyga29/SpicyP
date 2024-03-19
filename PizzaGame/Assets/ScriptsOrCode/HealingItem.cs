using UnityEngine;

public class HealingItem : MonoBehaviour
{
    [SerializeField] int healAmount = 1; // Amount of health to heal
    [SerializeField] AudioClip pickupSound; // Sound to play when the item is picked up

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the player collects the item
        {
            // Heal the player
            PlayerController_V2 player = collision.GetComponent<PlayerController_V2>();
            if (player != null)
            {
                player.Heal(healAmount);
            }

            // Play pickup sound
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // Destroy the item
            Destroy(gameObject);
        }
    }
}
