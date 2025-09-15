using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveTable : MonoBehaviour,IUseFurniture
{
    [SerializeField] private Transform ingredientPoint;
    [SerializeField] private Transform parentFood;
    private GameObject _ingredient;
    private bool _isHeroikTrigger;
    private Heroik _heroik;
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    private GameManager _gameManager;
    private FoodsForFurnitureContainer _foodsForFurnitureContainer;
    private bool _isInit;
    
    private bool IsAllInit => _gameManager.BootstrapLvl2.IsAllInit;
    
    private List<Product> ListProduct => _foodsForFurnitureContainer.GiveTable.ListForFurniture;

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
        
        while (_foodsForFurnitureContainer== null)
        {
            _foodsForFurnitureContainer = _gameManager.FoodsForFurnitureContainer;
            yield return null;
        }
        
        while (IsAllInit == false)
        {
            yield return null;
        }
        
        _isInit = true;
        Debug.Log("GiveTable Init");

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
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
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
    
    private bool AcceptObject(GameObject acceptObj)
    {
        if (acceptObj == null)
        {
            Debug.Log("Объект не передался");
            return false;
        }
        _ingredient = _gameManager.ProductsFactory.GetProduct(acceptObj, ingredientPoint, parentFood,true);
        _heroik.CleanObjOnHands();
        return true;
    }
    
    private GameObject GiveObj(GameObject giveObj)
    {
        return giveObj;
    }
    
    public void UpdateCondition()
    {
        if (CheckUseFurniture() == false)
        {
            //_heroik = null;
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
    
    private void CookingProcess()
    {
        if (CheckCookingProcess() == false)
        {
            return;
        }
        
        if(_heroik.IsBusyHands == false) // руки не заняты
        {
            if (_ingredient == null) // ни одного активного объекта
            {
                Debug.Log("У вас пустые руки и прилавок пуст");
                return;
            }
            
            // на столе что-то есть
            if (_heroik.TryPickUp(GiveObj(_ingredient)))
            {
                CleanObjOnTable(_ingredient);
            }
        }
        else // заняты
        {
            if (_ingredient == null) // ни одного активного объекта
            {
                if (!AcceptObject(_heroik.TryGiveIngredient(ListProduct)))
                {
                    Debug.Log("с предметом что-то пошло не так");
                }
            }
            else // активного объект есть
            {
                Debug.Log("У вас полные руки и прилавок полон");
            }
        }
    }

    private void CleanObjOnTable(GameObject ingredient)
    {
        Destroy(ingredient);
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
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
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
