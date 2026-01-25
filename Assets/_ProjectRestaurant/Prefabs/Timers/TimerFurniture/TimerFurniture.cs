using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Cysharp.Threading.Tasks;

public class TimerFurniture : IDisposable, IPause
{
    private TimerView _timerView;
    private float _time;
    private Transform _pointTimer;

    private Transform _arrow;
    private GameObject _timerObject;
    private float _currentTime;
    //private bool _isWork;
    
    private bool _isPause;
    private IHandlerPause _pauseHandler;

    public TimerFurniture(TimerView timerView, float time, Transform pointTimer,IHandlerPause pauseHandler)
    {
        _timerView = timerView;
        _time = time;
        _pointTimer = pointTimer;
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);

        Initialization();
    }
    
    public void Dispose()
    {
        _pauseHandler.Remove(this);
    }

    public async UniTask StartTimerAsync()
    {
        _timerObject.SetActive(true);
        //_isWork = true;
        _currentTime = 0f;

        // Пока не прошло всё время
        while (_currentTime < _time)
        {
            _currentTime += Time.deltaTime;

            float progress = _currentTime / _time;
            float angle = 360f * progress;

            _arrow.localRotation = Quaternion.Euler(0f, 0f, angle);

            await UniTask.WaitUntil(() => _isPause == false);
            await UniTask.Yield(); // ждём кадр
        }

        //_isWork = false;
        _timerObject.SetActive(false);
    }
    
    public void SetPause(bool isPaused) => _isPause = isPaused;
    
    private void Initialization()
    {
        _timerObject = Object.Instantiate(_timerView.gameObject, _pointTimer);
        _timerView = _timerObject.GetComponent<TimerView>();
        _arrow = _timerView.Arrow;
        _timerObject.SetActive(false);
    }
}

