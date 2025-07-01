using System;
using System.Collections;
using UnityEngine;

public class TimerFurniture : IDisposable
{
    private TimerView _timerView;
    private RectTransform _arrowRect;
    private float _time;
    private float _currentTime;
    private bool _isWork;

    public bool IsWork => _isWork;

    public TimerFurniture(TimerView timerView,float time)
    {
        _timerView = timerView;
        _time = time;
        _arrowRect = _timerView.ArrowRect;
        
        Debug.Log("Создал объект: TimerFurniture");
    }
    
    public IEnumerator StartTimer()
    {
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
        Dispose();
    }

    public void Dispose()
    {
        _timerView.gameObject.SetActive(false);
        Debug.Log("У объекта вызван Dispose : TimerFurniture");
    }
}
