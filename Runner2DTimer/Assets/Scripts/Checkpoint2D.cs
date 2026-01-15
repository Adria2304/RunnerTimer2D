using UnityEngine;

public class Checkpoint2D : MonoBehaviour
{
    [SerializeField] private bool deactivateAfterUse = true;
    [SerializeField] private float extraTime = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
        if (respawn == null) return;

        respawn.SetCheckpoint(transform.position);

        PlayerTimeLimit timeLimit = other.GetComponent<PlayerTimeLimit>();
        if (timeLimit != null)
        {
            timeLimit.AddTime(extraTime);
        }

        if (deactivateAfterUse)
            gameObject.SetActive(false);
    }
}
