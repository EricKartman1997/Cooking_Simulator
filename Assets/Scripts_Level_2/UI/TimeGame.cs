using TMPro;
using UnityEngine;

public class TimeGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    private float _currentTime = 0f;
    private float _seconds = 0f;
    private float _minutes = 0f;
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= 1f)
        {
            timeText.text = $"0{_minutes}:0{_seconds}";
            _seconds = Mathf.Round(_currentTime);
            if (_seconds >= 60f)
            {
                _minutes++;
                _currentTime = 0f;
                if (_minutes >= 60f)
                {
                    _minutes = 0f;
                    Debug.Log("Game Over");
                }
            }
        }
        timeText.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }

    public float GetSeconds()
    {
        return _seconds;
    }
    public float GetMinutes()
    {
        return _minutes;
    }
}
