using UnityEngine;

public class DecorationFurniture : MonoBehaviour
{
    [SerializeField] private DecorationTableConfig config;
    [SerializeField] private Transform positionViewTableTop;
    [SerializeField] private Transform positionViewLowerSurface;

    private GameObject _decorationTableTop;
    private GameObject _decorationLowerSurface;

    private GameManager _gameManager;

    public DecorationTableConfig Config => config;

    private void Start()
    {
        _gameManager = StaticManagerWithoutZenject.GameManager;
        _decorationLowerSurface = _gameManager.ViewFactory.GetDecorationLowerSurface(config.DecorationLowerSurface,positionViewLowerSurface);
        _decorationTableTop = _gameManager.ViewFactory.GetDecorationTableTop(config.DecorationTableTop,positionViewTableTop);
    }
}
