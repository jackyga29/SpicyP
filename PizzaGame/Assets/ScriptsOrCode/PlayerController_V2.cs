using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController_V2 : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    Animator animator;

    bool isGrounded;
    bool isShooting;

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpSpeed = 8f;

    [SerializeField] int bulletDamage = 1;
    [SerializeField] Transform BulletShootPos;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 5f;

    bool keyShoot;
    bool isTakingDamage;
    bool isInvincible;
    bool keyShootRelease;
    float shootTime;

    bool hitSideRight;

    bool isFacingRight = true;

    public int currentHealth;
    public int maxHealth = 28;

    public GameObject musicPlayer;
    private AudioSource audioSource;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;

        currentHealth = maxHealth;

        audioSource = musicPlayer.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isTakingDamage)
        {
            animator.Play("Hurt_Mega");
            return;
        }

        PlayerDirectionInput();
        PlayerShootInput();
        PlayerMovement();

        float keyHorizontal = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(keyHorizontal * moveSpeed, rb2d.velocity.y);

        if (keyHorizontal < 0)
        {
            spriteRenderer.flipX = true;
            isFacingRight = false;
        }
        else if (keyHorizontal > 0)
        {
            spriteRenderer.flipX = false;
            isFacingRight = true;
        }

        animator.SetBool("Moving", keyHorizontal != 0 && isGrounded);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                isGrounded = false;
            }
        }
    }

    void PlayerDirectionInput()
    {
        // Implement player direction input logic here
    }

    void PlayerMovement()
    {
        // Implement player movement logic here
    }

    void PlayerShootInput()
    {
        keyShoot = Input.GetKeyDown(KeyCode.C);

        float shootTimeLength = 0;
        float keyShootReleaseTimeLength = 0;

        if (keyShoot && keyShootRelease)
        {
            isShooting = true;
            keyShootRelease = false;
            shootTime = Time.time;
            Invoke("ShootBullet", 0.1f);
        }
        if (!keyShoot && !keyShootRelease)
        {
            keyShootReleaseTimeLength = Time.time - shootTime;
            keyShootRelease = true;
        }
        if (isShooting)
        {
            shootTimeLength = Time.time - shootTime;
            if (shootTimeLength >= 0.25f || keyShootReleaseTimeLength >= 0.15f)
            {
                isShooting = false;
            }
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, BulletShootPos.position, Quaternion.identity);
        bullet.name = bulletPrefab.name;
        bullet.GetComponent<BulletScript>().SetDamageValue(bulletDamage);
        bullet.GetComponent<BulletScript>().SetBulletSpeed(bulletSpeed);

        Vector2 bulletDirection = isFacingRight ? Vector2.right : Vector2.left;

        bullet.GetComponent<BulletScript>().SetBulletDirection(bulletDirection);
        bullet.GetComponent<SpriteRenderer>().flipX = !isFacingRight;

        bullet.GetComponent<BulletScript>().Shoot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void HitSide(bool rightSide)
    {
        hitSideRight = rightSide;
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
            else
            {
                PlayHitSound();
                StartDamageAnimation();
                Invoke("StopDamageAnimation", 1.0f);
            }
        }
    }

    void PlayHitSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void StartDamageAnimation()
    {
        if (!isTakingDamage)
        {
            isTakingDamage = true;
            isInvincible = true;
            float hitForceX = 1.50f;
            float hitForceY = 4f;
            if (hitSideRight) hitForceX = -hitForceX;
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(new Vector2(hitForceX, hitForceY), ForceMode2D.Impulse);
        }
    }

    void StopDamageAnimation()
    {
        isTakingDamage = false;
        isInvincible = false;
        animator.Play("Hurt_Mega", -1, 0f);

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Hurt_Mega")
            {
                float hurtMegaLength = clip.length;
                Invoke("PlayIdleAnimation", hurtMegaLength);
                break;
            }
        }
    }

    void PlayIdleAnimation()
    {
        animator.Play("Idle");
    }

    void Defeat()
    {
        StartCoroutine(ReloadSceneAfterDelay(5f));
    }

    IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
