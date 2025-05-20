using UnityEngine;

public class DecorationFurniture : MonoBehaviour
{
    [SerializeField] private DecorationTableConfig config;
    [SerializeField] private Transform positionViewTableTop;
    [SerializeField] private Transform positionViewLowerSurface;

    private GameObject _decorationTableTop;
    private GameObject _decorationLowerSurface;

    private void Start()
    {
        _decorationLowerSurface = StaticManagerWithoutZenject.ViewFactory.GetDecorationLowerSurface(config.DecorationLowerSurface,positionViewLowerSurface);
        _decorationTableTop = StaticManagerWithoutZenject.ViewFactory.GetDecorationTableTop(config.DecorationTableTop,positionViewTableTop);
    }
}
