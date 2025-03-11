using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimeGame : MonoBehaviour
{
    public static float[] TimeLevel;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private float _currentSeconds = 0f;
    [SerializeField] private float _currentMinutes = 0f;
    [SerializeField] private float _secondsLevel;
    [SerializeField] private float _minutesLevel;

    private void Awake()
    {
        _secondsLevel = Random.Range(45, 60);
        _minutesLevel = Random.Range(1, 2);
        _currentSeconds = _secondsLevel;
        _currentMinutes = _minutesLevel;
        TimeLevel = new float[]{_secondsLevel,_minutesLevel};
    }

    void Update()
    {
        _currentSeconds -= Time.deltaTime;
        
        if (_currentMinutes <= 0f && _currentSeconds <= 0f)
        {
            //Debug.Log("Game Over");
            EventBus.GameOver.Invoke();
            Debug.Log("Сработал GameOver в TimeGame");
        }
        else
        {
            if (_currentSeconds <= 0f)
            {
                --_currentMinutes;
                _currentSeconds = 60f;
            }
        }
        timeText.text = string.Format("{0:00}:{1:00}", _currentMinutes, _currentSeconds);
    }

    public float GetSeconds()
    {
        return _currentSeconds;
    }
    public float GetMinutes()
    {
        return _currentMinutes;
    }
}
