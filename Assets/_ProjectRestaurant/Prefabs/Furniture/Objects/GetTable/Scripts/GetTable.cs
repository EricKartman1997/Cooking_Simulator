using System.Collections;
using UnityEngine;

public class GetTable : MonoBehaviour, IUseFurniture
{
    [SerializeField] private Transform parentViewDish;
    
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    
    private GameObject _objectOnTheTable;
    private GameObject _objectFoodView;
    private Heroik _heroik; // только для объекта героя, а надо и другие...
    private bool _isHeroikTrigger;
    private bool _isInit;
    
    private IngredientName _giveFood;
    private ViewDishName _viewFood;
    
    private GameManager _gameManager;

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
            _objectFoodView = _gameManager.ViewFactory.GetProduct(_viewFood,parentViewDish);
            yield return null;
        }
        
        while (_objectOnTheTable == null)
        {
            _objectOnTheTable = _gameManager.ProductsFactory.GetProductRef(_giveFood);
            //_gameManager.ProductsFactory.GetProduct(_objectOnTheTable, Transform transform,Transform parent, bool setActive = true)
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
        
        if (_decorationFurniture.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            EnterTrigger();
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _heroik.ToInteractAction.Subscribe(CookingProcess);
            EnterTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isInit == false)
        {
            Debug.Log("Инициализация не закончена");
            return;
        }
        
        if (_decorationFurniture.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            ExitTrigger();
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _heroik.ToInteractAction.Unsubscribe(CookingProcess);
            ExitTrigger();
        }
    }
    
    // private void OnEnable()
    // {
    //     EventBus.PressE += CookingProcess;
    // }
    //
    // private void OnDisable()
    // {
    //     EventBus.PressE -= CookingProcess;
    // }
    public void Init(IngredientName giveFood, ViewDishName viewFood)
    {
        _giveFood = giveFood;
        _viewFood = viewFood;
    }
    
    public void UpdateCondition()
    {
        if (CheckUseFurniture() == false)
        {
            _outline.OutlineWidth = 0f;
        }
    }
    
    private void EnterTrigger()
    {
        _outline.OutlineWidth = 2f;
        _isHeroikTrigger = true;
        _heroik.CurrentUseFurniture = this;
    }
    
    private void ExitTrigger()
    {
        _outline.OutlineWidth = 0f;
        _isHeroikTrigger = false;
    }
    
    private bool CheckUseFurniture()
    {
        if (ReferenceEquals(_heroik.CurrentUseFurniture, this))
        {
            return true;
        }

        return false;
    }

    private GameObject GiveObj(GameObject giveObj)
    {
        return giveObj;
    }
    
    private void CookingProcess()
    {
        if (CheckCookingProcess() == false)
        {
            return;
        }

        _heroik.TryPickUp(GiveObj(_objectOnTheTable));
    }
    
    private bool CheckCookingProcess()
    {
        if (_isInit == false)
        {
            Debug.Log("Инициализация не закончена");
            return false;
        }
        
        if(_isHeroikTrigger == false)
        {
            return false;
        }
        
        if (_decorationFurniture.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            Debug.LogWarning("Стол не работает");
            return false;
        }
        
        if (CheckUseFurniture() == false)
        {
            return false;
        }

        return true;
    }
    
}
