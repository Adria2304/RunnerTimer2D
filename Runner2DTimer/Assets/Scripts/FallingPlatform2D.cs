using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
public class FallingPlatform2D : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float fallDelay = 1.5f;
    [SerializeField] private float blinkTime = 0.5f;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private float fallDuration = 2f;
    [SerializeField] private float respawnDelay = 3f;

    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color warningColor = Color.red;
    [SerializeField] private Color fallenColor = Color.gray;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    private Coroutine fallCoroutine;
    private bool isFalling;
    private Vector3 startPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        startPosition = transform.position;
        ResetPlatform();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling) return;
        if (!collision.collider.CompareTag("Player")) return;

        fallCoroutine ??= StartCoroutine(FallRoutine());
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isFalling) return;
        if (!collision.collider.CompareTag("Player")) return;

        if (fallCoroutine != null)
        {
            StopCoroutine(fallCoroutine);
            fallCoroutine = null;
            spriteRenderer.color = normalColor;
        }
    }

    private IEnumerator FallRoutine()
    {
        yield return new WaitForSeconds(fallDelay - blinkTime);
        yield return StartCoroutine(BlinkRoutine());

        isFalling = true;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        spriteRenderer.color = fallenColor;

        yield return new WaitForSeconds(fallDuration);

        rb.simulated = false;
        col.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        Respawn();
    }

    private IEnumerator BlinkRoutine()
    {
        float timer = 0f;
        bool toggle = false;

        while (timer < blinkTime)
        {
            spriteRenderer.color = toggle ? warningColor : normalColor;
            toggle = !toggle;

            timer += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private void Respawn()
    {
        transform.position = startPosition;

        rb.simulated = true;
        rb.bodyType = RigidbodyType2D.Static;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        col.enabled = true;
        spriteRenderer.enabled = true;
        spriteRenderer.color = normalColor;

        isFalling = false;
        fallCoroutine = null;
    }

    private void ResetPlatform()
    {
        rb.bodyType = RigidbodyType2D.Static;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        spriteRenderer.color = normalColor;
        isFalling = false;
    }
}
