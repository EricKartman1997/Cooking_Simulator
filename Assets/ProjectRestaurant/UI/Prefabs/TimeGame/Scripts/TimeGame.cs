using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimeGame : IDisposable
{
    public static float[] TimeLevel; // DataManager
    
    private TextMeshProUGUI _timeText;
    private float _currentSeconds;
    private float _currentMinutes;
    private float _secondsLevel;
    private float _minutesLevel;

    public float CurrentSeconds => _currentSeconds;

    public float CurrentMinutes => _currentMinutes;

    public TimeGame(TextMeshProUGUI timeText)
    {
        _timeText = timeText;
        Init();
        Debug.Log("Создать объект: TimeGame");
    }

    private void Init()
    {
        _secondsLevel = Random.Range(45, 60);
        _minutesLevel = Random.Range(1, 2);
        _currentSeconds = _secondsLevel;
        _currentMinutes = _minutesLevel;
        TimeLevel = new float[]{_secondsLevel,_minutesLevel};
    }

    public void Update()
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
        _timeText.text = string.Format("{0:00}:{1:00}", _currentMinutes, _currentSeconds);
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : TimeGame");
    }
}
