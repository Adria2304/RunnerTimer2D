using UnityEngine;

[RequireComponent(typeof(PlayerRespawn))]
public class PlayerTimeLimit : MonoBehaviour
{
    [SerializeField] private float startTime = 60f;

    private float currentTime;
    private PlayerRespawn respawn;

    private void Awake()
    {
        respawn = GetComponent<PlayerRespawn>();
        currentTime = startTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            TimeOut();
        }
    }

    private void TimeOut()
    {
        currentTime = startTime;
        respawn.ResetToStart();
        respawn.Respawn();
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public void AddTime(float seconds)
    {
        currentTime += seconds;
    }

}
