using UnityEngine;
using Zenject;

public class DecorationFurniture : MonoBehaviour
{
    [SerializeField] private Transform positionViewTableTop;
    [SerializeField] private Transform positionViewLowerSurface;

    private CustomFurnitureName _decorationTableTop;
    private CustomFurnitureName _decorationLowerSurface;
    private GameObject _decorationTableTopObj;
    private GameObject _decorationLowerSurfaceObj;
    
    private ViewFactory _viewFactory;

    public CustomFurnitureName DecorationTableTop => _decorationTableTop;

    [Inject]
    private void ConstructZenject(ViewFactory viewFactory)
    {
        _viewFactory = viewFactory;
    }

    private void Start()
    {
        if (_decorationLowerSurface == CustomFurnitureName.TurnOff)
        {
            Debug.LogError("Ошибка установки нижней поверхности - TurnOff быть не может");
        }
        _decorationLowerSurfaceObj = _viewFactory.GetDecorationLowerSurface(_decorationLowerSurface,positionViewLowerSurface);
        _decorationTableTopObj = _viewFactory.GetDecorationTableTop(_decorationTableTop,positionViewTableTop);
    }
    
    public void Init(CustomFurnitureName decorationTableTop, CustomFurnitureName decorationLowerSurface)
    {
        _decorationTableTop = decorationTableTop;
        _decorationLowerSurface = decorationLowerSurface;
    }
}
