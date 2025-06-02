using UnityEngine;
using UnityEngine.UI;

public class NewTimer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Image arrowImage;
    [SerializeField] private Image circleImage;
    
    private RectTransform _arrowRect;
    private float _currentTime;

    private void Start()
    {
        arrowImage.sprite = arrowSprite;
        circleImage.sprite = circleSprite;
        _arrowRect = arrowImage.GetComponent<RectTransform>();
    }
    
    private void Update()
    {
        _currentTime += Time.deltaTime ;
        if (time >= _currentTime)
        {
            // Рассчитываем процент оставшегося времени
            float progress = _currentTime / time;
        
            // Вычисляем угол вращения (360 градусов за время жизни)
            float angle = 360f * progress;
        
            // Применяем вращение (с учетом Z-оси для 2D)
            _arrowRect.localEulerAngles = new Vector3(0, 0, -angle);
        }
        else
        {
            _currentTime = 0;
            gameObject.SetActive(false);
        }
    }
}
