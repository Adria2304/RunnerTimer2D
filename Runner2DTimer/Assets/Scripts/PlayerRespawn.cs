using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Vector2 startPosition;

    private Rigidbody2D rb;
    private Vector2 currentCheckpoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentCheckpoint = startPosition == Vector2.zero
            ? transform.position
            : startPosition;
    }

    public void SetCheckpoint(Vector2 position)
    {
        currentCheckpoint = position;
    }

    public void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = currentCheckpoint;
    }

    public void ResetToStart()
    {
        currentCheckpoint = startPosition;
    }

}
