using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class TimeGame : IDisposable
{
    public event Action<float,float> UpdateTime;
    public event Action ShowTime;
    public bool IsInit;
    
    private GameManager _gameManager;
    private UIManager _uiManager;
    private MonoBehaviour _coroutineMonoBehaviour;
    
    private TextMeshProUGUI _timeText;
    
    private float[] _timeLevel;
    private float _currentSeconds;
    private float _currentMinutes;
    private float _secondsLevel;
    private float _minutesLevel;

    public float[] TimeLevel => _timeLevel;
    public float CurrentSeconds => _currentSeconds;

    public float CurrentMinutes => _currentMinutes;

    public TimeGame(MonoBehaviour coroutineMonoBehaviour)
    {
        _coroutineMonoBehaviour = coroutineMonoBehaviour;
        
        _coroutineMonoBehaviour.StartCoroutine(Init());
        //Debug.Log("Создать объект: TimeGame");
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : TimeGame");
    }

    private IEnumerator Init()
    {
        _secondsLevel = Random.Range(45, 60);
        _minutesLevel = Random.Range(1, 2);
        _currentSeconds = _secondsLevel;
        _currentMinutes = _minutesLevel;
        _timeLevel = new float[]{_secondsLevel,_minutesLevel};
        
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }
        
        // while (_uiManager == null)
        // {
        //     _uiManager = _gameManager.UIManager;
        //     yield return null;
        // }

        // _timeText = _uiManager.TimeText;
        IsInit = true;    
        Debug.Log("Создать объект: TimeGame");
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
        // _timeText.text = string.Format("{0:00}:{1:00}", _currentMinutes, _currentSeconds);
        ShowTime?.Invoke();
        UpdateTime?.Invoke(_currentMinutes, _currentSeconds);
    }
}
