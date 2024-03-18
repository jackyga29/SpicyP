using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover_Proto : MonoBehaviour
{
    // Define variables
    public float horizontalAmplitude = 5.0f; // Amount of horizontal oscillation
    public float horizontalFrequency = 1.0f; // Speed of horizontal oscillation
    public float verticalAmplitude = 2.0f; // Amount of vertical oscillation
    public float verticalFrequency = 1.5f; // Speed of vertical oscillation
    public float movingSpeed = 1.0f;

    private float initialX;
    private float initialY;

    // Use this for initialization
    void Start()
    {
        initialX = transform.position.x;
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalOffset = Mathf.Sin(Time.time * horizontalFrequency) * horizontalAmplitude;
        float verticalOffset = Mathf.Cos(Time.time * verticalFrequency) * verticalAmplitude;

        Vector3 newPosition = transform.position;
        newPosition.x = initialX + horizontalOffset;
        newPosition.y = initialY + verticalOffset;
        transform.position = newPosition;

        // Move forward
        transform.Translate(Vector3.right * movingSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Make the player a child of the platform
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Release the player from being a child of the platform
            collision.transform.parent = null;
        }
    }
}