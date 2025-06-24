using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NewTimer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Image arrowImage;
    [SerializeField] private Image circleImage;
    
    private RectTransform _arrowRect;
    private float _currentTime;
    private bool _isWork;

    public bool IsWork => _isWork;

    private void Awake()
    {
        arrowImage.sprite = arrowSprite;
        circleImage.sprite = circleSprite;
        _arrowRect = arrowImage.GetComponent<RectTransform>();
    }
    
    public IEnumerator StartTimer()
    {
        _isWork = false;
        while (time >= _currentTime)
        {
            _currentTime += Time.deltaTime;
            
            // Рассчитываем процент оставшегося времени
            float progress = _currentTime / time;
        
            // Вычисляем угол вращения (360 градусов за время жизни)
            float angle = 360f * progress;
        
            // Применяем вращение (с учетом Z-оси для 2D)
            _arrowRect.localEulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }

        _currentTime = 0;
        _isWork = true;
        gameObject.SetActive(false);
    }
}
