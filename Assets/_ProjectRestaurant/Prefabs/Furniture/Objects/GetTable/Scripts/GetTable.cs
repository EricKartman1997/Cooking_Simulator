using UnityEngine;
using Zenject;

public class GetTable : MonoBehaviour, IUseFurniture
{
    [SerializeField] private Transform parentViewDish;
    [SerializeField] private SoundsFurniture sounds;
    
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    
    private GameObject _objectOnTheTable;
    private GameObject _objectFoodView;
    private Heroik _heroik; // только для объекта героя, а надо и другие...
    private bool _isHeroikTrigger;
    
    private IngredientName _giveFood;
    private ViewDishName _viewFood;

    private ViewFactory _viewFactory;
    private ProductsFactory _productsFactory;
    
    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
    }

    private void Start()
    {
        _objectFoodView = _viewFactory.GetProduct(_viewFood,parentViewDish);
        _objectOnTheTable = _productsFactory.GetProductRef(_giveFood);
        
        //Debug.Log("GetTable Init");
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
        {
            _heroik = other.GetComponent<Heroik>();
            _heroik.ToInteractAction.Subscribe(CookingProcess);
            EnterTrigger();
            //Debug.Log("зашел");
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
            // Debug.Log("Вышел");
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
    
    // private void OnEnable()
    // {
    //     EventBus.PressE += CookingProcess;
    // }
    //
    // private void OnDisable()
    // {
    //     EventBus.PressE -= CookingProcess;
    // }
    
    [Inject]
    private void ConstructZenject(
        ViewFactory viewFactory,
        ProductsFactory productsFactory)
    {
        _productsFactory = productsFactory;
        _viewFactory = viewFactory;
    }
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

        if (_heroik.TryPickUp(GiveObj(_objectOnTheTable)))
        {
            sounds.PlayOneShotClip(AudioNameGamePlay.TakeOnTheTableSound);
            return;
        }
        
        _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.ForbiddenSound);
        
    }
    
    private bool CheckCookingProcess()
    {
        if(_isHeroikTrigger == false)
        {
            return false;
        }
        
        if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
        {
            Debug.Log("Стол не работает");
            _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.NotWorkTableSound);
            return false;
        }
        
        if (CheckUseFurniture() == false)
        {
            //Debug.Log("Зашел4");
            return false;
        }

        return true;
    }
    
}
