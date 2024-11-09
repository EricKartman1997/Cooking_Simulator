using System;
using TMPro;
using UnityEngine;

public class TimeGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    //private float _currentTime = 0f;
    [SerializeField] private float _seconds = 0f;
    [SerializeField] private float _minutes = 0f;

    private void Start()
    {
        _seconds = Level.TimeLevel[0];
        _minutes = Level.TimeLevel[1];
    }

    void Update()
    {
        // _currentTime += Time.deltaTime;
        // if (_currentTime >= 1f)
        // {
        //     timeText.text = $"0{_minutes}:0{_seconds}";
        //     _seconds = Mathf.Round(_currentTime);
        //     if (_seconds >= 60f)
        //     {
        //         _minutes++;
        //         _currentTime = 0f;
        //         if (_minutes >= 60f)
        //         {
        //             _minutes = 0f;
        //             Debug.Log("Game Over");
        //         }
        //     }
        // }
        // timeText.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
        
        _seconds -= Time.deltaTime;
        
        if (_minutes <= 0f && _seconds <= 0f)
        {
            Debug.Log("Game Over");
        }
        else
        {
            if (_seconds <= 0f)
            {
                _minutes--;
                _seconds = 60f;
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
