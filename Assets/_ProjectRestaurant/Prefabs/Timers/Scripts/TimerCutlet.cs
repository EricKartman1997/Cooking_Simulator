using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimerCutlet : MonoBehaviour
{
    [SerializeField] private ConfigTimer config;
    [SerializeField] private Image arrowImage;
    [SerializeField] private Image circleImage;
    private RectTransform _arrowRect;

    
    private void Awake()
    {
        UpdateTimeView();
        _arrowRect = arrowImage.GetComponent<RectTransform>();
    }
    
    public void UpdateArrowRotation(float currentTime,float cookingTime)
    {
        if (currentTime < cookingTime)
        {
            // Рассчитываем процент оставшегося времени
            float progress = currentTime / cookingTime;
        
            // Вычисляем угол вращения (360 градусов за время жизни)
            float angle = 360f * progress;
        
            // Применяем вращение (с учетом Z-оси для 2D)
            _arrowRect.localEulerAngles = new Vector3(0, 0, -angle);
            
        }
    }

    private void UpdateTimeView()
    {
        arrowImage.sprite = config.ArrowPref;
        circleImage.sprite = config.CirclePref;
    }
    
}
