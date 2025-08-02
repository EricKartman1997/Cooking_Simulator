using System.Collections;
using UnityEngine;

public class GetTable : MonoBehaviour, IGiveObj
{
    [SerializeField] private GetTableConfig getTableConfig;
    [SerializeField] private Transform parentViewDish;
    
    private Outline _outline;
    private GameObject _objectOnTheTable;
    private GameObject _objectFoodView;
    private Heroik _heroik; // только для объекта героя, а надо и другие...
    private bool _isHeroikTrigger;
    private bool _isInit;
    private GameManager _gameManager;
    private DecorationFurniture _decorationFurniture;

    private bool IsAllInit => _gameManager.BootstrapLvl2.IsAllInit;
    

    

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
    }

    private IEnumerator Start()
    {
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }
        
        while (_objectFoodView == null)
        {
            _objectFoodView = _gameManager.ViewFactory.GetProduct(getTableConfig.FoodView,parentViewDish);
            yield return null;
        }
        
        while (_objectOnTheTable == null)
        {
            _objectOnTheTable = _gameManager.ProductsFactory.GetProductRef(getTableConfig.GiveFood);
            yield return null;
        }
        
        while (IsAllInit == false)
        {
            yield return null;
        }
        
        Debug.Log("GetTable Init");
        _isInit = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_isInit == false)
        {
            Debug.Log("Инициализация не закончена");
            return;
        }
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            _outline.OutlineWidth = 2f;
            _isHeroikTrigger = true;
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            _isHeroikTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isInit == false)
        {
            Debug.Log("Инициализация не закончена");
            return;
        }
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            _outline.OutlineWidth = 0f;
            _isHeroikTrigger = false;
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _heroik = null;
            _outline.OutlineWidth = 0f;
            _isHeroikTrigger = false;
        }
    }
    
    private void OnEnable()
    {
        EventBus.PressE += CookingProcess;
    }

    private void OnDisable()
    {
        EventBus.PressE -= CookingProcess;
    }

    public GameObject GiveObj(ref GameObject giveObj)
    {
        GameObject giveObjCopy = _gameManager.ProductsFactory.GetProduct(giveObj, false);
        return giveObjCopy;
    }
    
    private void CookingProcess()
    {
        if (_isInit == false)
        {
            Debug.Log("Инициализация не закончена");
            return;
        }

        if (_isHeroikTrigger == false)
        {
            return;
        }
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            Debug.LogWarning("Стол не работает");
            return;
        }

        if (_heroik.IsBusyHands == false) //объект есть на столе, руки незаняты
        {
            _heroik.TryPickUp(GiveObj(ref _objectOnTheTable));
        }
        else// объект есть на столе, руки заняты
        {
            Debug.Log("объект есть на столе,руки заняты");
        }
    }
    
}
