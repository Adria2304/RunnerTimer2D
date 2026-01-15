using UnityEngine;

public class MetaGoal2D : MonoBehaviour
{
    [SerializeField] private GameObject winUI;

    private void Awake()
    {
        winUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        winUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
