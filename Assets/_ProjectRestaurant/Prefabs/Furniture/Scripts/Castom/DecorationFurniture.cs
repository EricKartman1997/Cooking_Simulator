using System.Collections;
using UnityEngine;

public class DecorationFurniture : MonoBehaviour
{
    [SerializeField] private Transform positionViewTableTop;
    [SerializeField] private Transform positionViewLowerSurface;

    private EnumDecorationTableTop _decorationTableTop;
    private EnumDecorationLowerSurface _decorationLowerSurface;
    private GameObject _decorationTableTopObj;
    private GameObject _decorationLowerSurfaceObj;
    private GameManager _gameManager;
    private bool _isInit;

    public EnumDecorationTableTop DecorationTableTop => _decorationTableTop;
    public bool IsInit => _isInit;

    private IEnumerator Start()
    {
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }
        
        _decorationLowerSurfaceObj = _gameManager.ViewFactory.GetDecorationLowerSurface(_decorationLowerSurface,positionViewLowerSurface);
        _decorationTableTopObj = _gameManager.ViewFactory.GetDecorationTableTop(_decorationTableTop,positionViewTableTop);
        
        _isInit = true;
        //Debug.Log("DecorationFurniture Init");
    }
    
    public void Init(EnumDecorationTableTop decorationTableTop, EnumDecorationLowerSurface decorationLowerSurface)
    {
        _decorationTableTop = decorationTableTop;
        _decorationLowerSurface = decorationLowerSurface;
    }
}
