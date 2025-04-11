using UnityEngine;
using UnityEngine.UI;

public class Timer2 : MonoBehaviour
{
    [SerializeField] private ConfigTimer config;
    [SerializeField] private Image arrowImage;
    [SerializeField] private Image circleImage;
    //[SerializeField] private float currentTime; // Debug
    //private float _timeLife;
    //private bool _isPossibleDelete;
    private RectTransform _arrowRect;

    // public float CurrentTime
    // {
    //     get => currentTime;
    //     set => currentTime = value;
    // }

    // public bool IsPossibleDelete
    // {
    //     get => _isPossibleDelete;
    //     set => _isPossibleDelete = value;
    // }

    // public float TimeLife
    // {
    //     get => _timeLife;
    //     set => _timeLife = value;
    // }

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

    public void UpdateTimeView()
    {
        arrowImage.sprite = config.ArrowPref;
        circleImage.sprite = config.CirclePref;
    }
    

}
