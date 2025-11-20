using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TimerFurniture : System.IDisposable
{
    private TimerView _timerView;
    private float _time;
    private Transform _pointTimer;

    private RectTransform _arrowRect;
    private GameObject _timerObject;
    private float _currentTime;
    private bool _isWork;

    public bool IsWork => _isWork;

    public TimerFurniture(TimerView timerView, float time, Transform pointTimer)
    {
        _timerView = timerView;
        _time = time;
        _pointTimer = pointTimer;

        Initialization();
    }

    public async UniTask StartTimerAsync()
    {
        _timerObject.SetActive(true);
        _isWork = true;
        _currentTime = 0f;

        // Пока не прошло всё время
        while (_currentTime < _time)
        {
            _currentTime += Time.deltaTime;

            float progress = _currentTime / _time;
            float angle = 360f * progress;

            _arrowRect.localEulerAngles = new Vector3(0, 0, angle);

            await UniTask.Yield(); // ждём кадр
        }

        _isWork = false;
        _timerObject.SetActive(false);
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : TimerFurniture");
    }

    private void Initialization()
    {
        _timerObject = Object.Instantiate(_timerView.gameObject, _pointTimer);
        _timerView = _timerObject.GetComponent<TimerView>();
        _arrowRect = _timerView.ArrowRect;
        _timerObject.SetActive(false);
    }
}

