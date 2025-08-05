using TMPro;
using UnityEngine;

public class TimeGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    public void UpdateTime(float minutes, float seconds)
    {
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Show() => gameObject.SetActive(true);
    
    public void Hide() => gameObject.SetActive(false);
}
