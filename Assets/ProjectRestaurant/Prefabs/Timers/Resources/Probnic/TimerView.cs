using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Image arrowImage;
    [SerializeField] private Image circleImage;

    public Image ArrowImage => arrowImage;

    public Image CircleImage => circleImage;
    
    public RectTransform ArrowRect => arrowImage.GetComponent<RectTransform>();
}
