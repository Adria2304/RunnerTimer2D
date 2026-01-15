using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private PlayerTimeLimit playerTime;
    [SerializeField] private TextMeshProUGUI timeText;

    private void Update()
    {
        float time = playerTime.GetCurrentTime();
        timeText.text = Mathf.CeilToInt(time).ToString();
    }
}
