using UnityEngine;

public class Despawn : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerRespawn respawn = collision.gameObject.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.Respawn();
            }
            return;
        }

        if (collision.gameObject.CompareTag("suelo cae"))
        {
            // Do nothing: FallingPlatform2D handles its own respawn
            return;
        }
    }
}
