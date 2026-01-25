using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimerCutlet : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    private RectTransform _arrowRect;

    
    public void UpdateArrowRotation(float currentTime,float cookingTime)
    {
        if (currentTime < cookingTime)
        {
            // Рассчитываем процент оставшегося времени
            float progress = currentTime / cookingTime;
        
            // Вычисляем угол вращения (360 градусов за время жизни)
            float angle = 360f * progress;
        
            // Применяем вращение (с учетом Z-оси для 2D)
            arrow.localRotation = Quaternion.Euler(0f, 0f, angle);
            
        }
    }
}
