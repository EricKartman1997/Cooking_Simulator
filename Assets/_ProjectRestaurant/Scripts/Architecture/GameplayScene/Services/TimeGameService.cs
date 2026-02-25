using System;
using UnityEngine;
using Zenject;

public class TimeGameService : IDisposable, ITickable, IPause
{
    public event Action GameOver;
    
    private bool _work;
    private float[] _timeLevel;
    private float _currentSeconds;
    private float _currentMinutes;
    private float _secondsLevel;
    private float _minutesLevel;
    
    private bool _isPause;
    private IHandlerPause _pauseHandler;
    private GamePlaySceneSettings _settings;
    private TimeGameUI _timeGameUI;
    private FactoryUIGameplay _factoryUIGameplay;
    
    public float[] TimeLevel => _timeLevel;
    public float CurrentSeconds => _currentSeconds;

    public float CurrentMinutes => _currentMinutes;

    public TimeGameService(IHandlerPause pauseHandler,GamePlaySceneSettings settings,FactoryUIGameplay factoryUIGameplay)
    {
        _settings = settings;
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);
        _factoryUIGameplay = factoryUIGameplay;
        CreateTimeLevel();
    }
    
    public void Dispose()
    {
        _pauseHandler.Remove(this);
    }
    
    public void Init()
    {
        _work = true;
        _timeGameUI = _factoryUIGameplay.TimeGameUI;
    }

    private void CreateTimeLevel()
    {
        _secondsLevel = _settings.Seconds;
        _minutesLevel = _settings.Minutes;
        
        _currentSeconds = _secondsLevel;
        _currentMinutes = _minutesLevel;
        _timeLevel = new float[]{_secondsLevel,_minutesLevel};
        
    }

    public void Tick()
    {
        if(_work == false)
            return;
        
        if(_isPause)
            return;
        
        _currentSeconds -= Time.deltaTime;
        
        if (_currentMinutes <= 0f && _currentSeconds <= 0f)
        {
            GameOver?.Invoke();
            Debug.Log("Сработал GameOverService в TimeGameService");
        }
        else
        {
            if (_currentSeconds <= 0f)
            {
                --_currentMinutes;
                _currentSeconds = 60f;
            }
        }
        _timeGameUI.Show();
        _timeGameUI.UpdateTime(_currentMinutes, _currentSeconds);
    }

    public void SetPause(bool isPaused) => _isPause = isPaused;

}
