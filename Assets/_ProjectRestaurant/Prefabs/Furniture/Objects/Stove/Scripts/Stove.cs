using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Stove : MonoBehaviour, IUseFurniture
{
    [SerializeField] private Transform positionRawFood;
    
    private Animator _animator;
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    
    private StovePoints _stovePoints;
    private StoveView _stoveView;
    
    private bool _isHeroikTrigger;
    private Heroik _heroik;
    private GameObject _ingredient;
    private IForStove _componentForStove;
    private GameObject _result;
    
    private FoodsForFurnitureContainer _foodsForFurnitureContainer;
    private HelperScriptFactory _helperScriptFactory;
    private ProductsFactory _productsFactory;
    
    private List<Product> ListProduct => _foodsForFurnitureContainer.Stove.ListForFurniture;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _animator = GetComponent<Animator>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
    }

    private void Start()
    {
        _stovePoints = _helperScriptFactory.GetStovePoints(positionRawFood);
        _stoveView = _helperScriptFactory.GetStoveView();
        
        //Debug.Log("Stove Init");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
        {
            _heroik = other.GetComponent<Heroik>();
            _heroik.ToInteractAction.Subscribe(CookingProcess);
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
        if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
        {
            _heroik.ToInteractAction.Unsubscribe(CookingProcess);
            ExitTrigger();
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _heroik.ToInteractAction.Unsubscribe(CookingProcess);
            ExitTrigger();
        }
    }
    
    [Inject]
    private void ConstructZenject( 
        HelperScriptFactory helperScriptFactory,
        ProductsFactory productsFactory,
        FoodsForFurnitureContainer foodsForFurnitureContainer)
    {
        _productsFactory = productsFactory;
        _helperScriptFactory = helperScriptFactory;
        _foodsForFurnitureContainer = foodsForFurnitureContainer;
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

    private bool AcceptObject(GameObject acceptObj)
    {
        if (acceptObj == null)
        {
            Debug.Log("Объект не передался");
            return false;
        }
        _ingredient = _productsFactory.GetProduct(acceptObj,_stovePoints.PositionRawFood,_stovePoints.PositionRawFood, true);
        _heroik.CleanObjOnHands();
        _componentForStove = _ingredient.GetComponent<IForStove>();
        return true;
    }
    
    private void TurnOff()
    {
        
    }

    private void TurnOn()
    {
        
    }
    
    private void CreateResult() 
    {
        _componentForStove.IsOnStove = false;
        if (_componentForStove != null)
        {
            _result = _productsFactory.GetCutlet(_componentForStove.Roasting);
            _result.GetComponent<Cutlet>().UpdateTime(_componentForStove.TimeRemaining); 
            Destroy(_ingredient);
            _ingredient = null;
            _componentForStove = null;
        }
        else
        {
            Debug.LogError("Ошибка в CreateResult");
        }

    }
    
    private void CookingProcess()
    {
        
        if (CheckCookingProcess() == false)
        {
            return;
        }
        
        if (_heroik.IsBusyHands == true)
        {
            if (AcceptObject(_heroik.TryGiveIngredient(ListProduct)))
            {
                _componentForStove.IsOnStove = true;
            }
            else
            {
                Debug.Log("с предметом что-то пошло не так");
            }
        }
        else
        {
            if (_ingredient != null)
            {
                CreateResult();
                if (_heroik.TryPickUp(GiveObj(_result)))
                {
                    CleanObjOnTable(_result);
                }
            }
            else
            {
                Debug.LogWarning("Забирать нечего");
            }

        }
    }
    
    private bool CheckCookingProcess()
    {
        if(_isHeroikTrigger == false)
        {
            return false;
        }
        
        if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
        {
            Debug.LogWarning("Плита не работает");
            return false;
        }
        
        if (CheckUseFurniture() == false)
        {
            return false;
        }

        return true;
    }
    
    private void CleanObjOnTable(GameObject ingredient)
    {
        Destroy(ingredient);
    }
    
}
