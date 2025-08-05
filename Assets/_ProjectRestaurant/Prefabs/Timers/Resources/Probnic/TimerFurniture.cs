using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class TimerFurniture : IDisposable
{
    private TimerView _timerView;
    private float _time;
    private Transform _pointTimer;
    
    private RectTransform _arrowRect;
    private GameObject _timerObject;
    private float _currentTime;
    private bool _isWork;

    public bool IsWork => _isWork;

    public TimerFurniture(TimerView timerView,float time, Transform pointTimer)
    {
        _timerView = timerView;
        _time = time;
        _pointTimer = pointTimer;

        Initialization();
        //Debug.Log("Создал объект: TimerFurniture");
    }
    
    public IEnumerator StartTimer()
    {
        _timerObject.gameObject.SetActive(true);
        _isWork = true;
        while (_time >= _currentTime)
        {
            _currentTime += Time.deltaTime;
            
            // Рассчитываем процент оставшегося времени
            float progress = _currentTime / _time;
        
            // Вычисляем угол вращения (360 градусов за время жизни)
            float angle = 360f * progress;
        
            // Применяем вращение (с учетом Z-оси для 2D)
            _arrowRect.localEulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }

        _currentTime = 0;
        _isWork = false;
        _timerObject.gameObject.SetActive(false);
    }

    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : TimerFurniture");
    }

    private void Initialization()
    {
        _timerObject = Object.Instantiate(_timerView.gameObject,_pointTimer);
        _timerView = _timerObject.GetComponent<TimerView>();
        _arrowRect = _timerView.ArrowRect;
        _timerObject.gameObject.SetActive(false);
    }
}
