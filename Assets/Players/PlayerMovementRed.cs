using UnityEngine;
using System.Collections;

public class PlayerControllerRed : MonoBehaviour
{
    public float speed = 10.0f;
    public float drag = 1.0f;
    public float bounceForce = 5.0f;
    public float shrinkSpeed = 0.01f;
    public Sprite deathSprite; // Assign the BlobDeath sprite in the Inspector

    private Rigidbody2D rb;
    private bool isInArena = true;
    private bool isDead = false;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = drag;
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isInArena && !isDead)
        {
            float moveHorizontal = 0;
            float moveVertical = 0;

            if (Input.GetKey(KeyCode.W))
            {
                moveVertical = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveVertical = -1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                moveHorizontal = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveHorizontal = 1;
            }

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb.AddForce(movement * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Arena"))
        {
            isInArena = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the game object is active before starting the coroutine
        if (other.gameObject.CompareTag("Arena") && gameObject.activeSelf)
        {
            // Start the coroutine only if the game object is active
            StartCoroutine(FallOffAndDie());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Blob"))
    {
        Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (otherRb != null)
        {
            // Calculate the direction of the bounce based on the angle of collision
            Vector2 direction = collision.GetContact(0).normal;

            // Apply the bounce force
            rb.AddForce(-direction * bounceForce, ForceMode2D.Impulse);
            otherRb.AddForce(direction * bounceForce, ForceMode2D.Impulse);
        }
    }
}


    IEnumerator FallOffAndDie()
    {
        if (!isDead)
        {
            isDead = true;
            rb.velocity = Vector2.zero; // Stop the blob's movement

            // Make the blob shrink
            for (float i = 1f; i >= 0; i -= shrinkSpeed)
            {
                transform.localScale = originalScale * i;
                yield return new WaitForSeconds(0.01f);
            }

            // Change sprite to BlobDeath
            GetComponent<SpriteRenderer>().sprite = deathSprite;

            // Reset scale
            transform.localScale = originalScale;

            // Disable movement
            isInArena = false;

            // Notify GameManager
            GameManager.instance.BlobDied();
        }
    }
}
