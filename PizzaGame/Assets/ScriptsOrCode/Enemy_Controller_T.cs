using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller_T : MonoBehaviour
{
    bool isInvincible;

    public int currentHealth;
    public int maxHealth = 1;
    public int contactDamage = 1;

    // Oscillation variables
    public float horizontalAmplitude = 5.0f; // Amount of horizontal oscillation
    public float horizontalFrequency = 1.0f; // Speed of horizontal oscillation
    public float verticalAmplitude = 5.0f; // Amount of vertical oscillation
    public float verticalFrequency = 1.0f; // Speed of vertical oscillation
    private float initialX;
    private float initialY;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        initialX = transform.position.x; // Store the initial X position for horizontal oscillation
        initialY = transform.position.y; // Store the initial Y position for vertical oscillation
    }

    public void Invincible(bool invincibility)
    {
        isInvincible = invincibility;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            Mathf.Clamp(currentHealth, 0, maxHealth);
            if (currentHealth <= 0)
            {
                Defeat();
            }
        }
    }

    void Defeat()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController_V2 player = other.gameObject.GetComponent<PlayerController_V2>();
            player.HitSide(transform.position.x > player.transform.position.x);
            player.TakeDamage(this.contactDamage);
            Debug.Log("Player Hit");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Oscillate(); // Call the oscillation function
    }

    void Oscillate()
    {
        // Calculate the horizontal offset using sine function
        float horizontalOffset = Mathf.Sin(Time.time * horizontalFrequency) * horizontalAmplitude;

        // Calculate the vertical offset using cosine function
        float verticalOffset = Mathf.Cos(Time.time * verticalFrequency) * verticalAmplitude;

        // Set the new position with oscillation effect
        Vector3 newPosition = transform.position;
        newPosition.x = initialX + horizontalOffset;
        newPosition.y = initialY + verticalOffset;
        transform.position = newPosition;
    }
}