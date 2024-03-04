using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    Animator animator; // Added Animator component

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpSpeed = 8f;

    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Get Animator component
    }

    // Update is called once per frame
    void Update()
    {
        float keyHorizontal = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(keyHorizontal * moveSpeed, rb2d.velocity.y);

        // Flip sprite if moving left
        if (keyHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Flip sprite if moving right
        else if (keyHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Set "Moving" parameter in the Animator
        animator.SetBool("Moving", keyHorizontal != 0 && isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

