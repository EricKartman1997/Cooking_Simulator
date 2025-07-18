using System.Collections;
using UnityEngine;

public class DecorationFurniture : MonoBehaviour
{
    [SerializeField] private DecorationTableConfig config;
    [SerializeField] private Transform positionViewTableTop;
    [SerializeField] private Transform positionViewLowerSurface;

    private GameObject _decorationTableTop;
    private GameObject _decorationLowerSurface;
    private GameManager _gameManager;
    private bool _isInit;

    public DecorationTableConfig Config => config;
    public bool IsInit => _isInit;

    private IEnumerator Start()
    {
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }
        
        _decorationLowerSurface = _gameManager.ViewFactory.GetDecorationLowerSurface(config.DecorationLowerSurface,positionViewLowerSurface);
        _decorationTableTop = _gameManager.ViewFactory.GetDecorationTableTop(config.DecorationTableTop,positionViewTableTop);
        
        _isInit = true;
        //Debug.Log("DecorationFurniture Init");
    }
}
