using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image timerObject;
    [SerializeField] private float time;
    private float _timer = 0f;
    void Update()
    {
        if (time >= _timer) 
        { 
            _timer += Time.deltaTime;
            timerObject.fillAmount = _timer / time;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

// public class Timer1 : MonoBehaviour
// {
//     [SerializeField] private ConfigTimer config;
//     [SerializeField] private Image arrowImage;
//     [SerializeField] private Image circleImage;
//     [SerializeField] private float currentTime;
//     private float _timeLife;
//     
//     private RectTransform _arrowRect;
//
//     private void Start()
//     {
//         arrowImage.sprite = config.ArrowPref;
//         circleImage.sprite = config.ArrowPref;
//         _timeLife = config.LifeTime;
//         
//         _arrowRect = arrowImage.GetComponent<RectTransform>();
//     }
//     
//     void Update()
//     {
//         if (currentTime < _timeLife)
//         {
//             currentTime += Time.deltaTime;
//             
//             UpdateArrowRotation();
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }
//     
//     private void UpdateArrowRotation()
//     {
//         // Рассчитываем процент оставшегося времени
//         float progress = currentTime / _timeLife;
//         
//         // Вычисляем угол вращения (360 градусов за время жизни)
//         float angle = 360f * progress;
//         
//         // Применяем вращение (с учетом Z-оси для 2D)
//         _arrowRect.localEulerAngles = new Vector3(0, 0, -angle);
//     }
// }


