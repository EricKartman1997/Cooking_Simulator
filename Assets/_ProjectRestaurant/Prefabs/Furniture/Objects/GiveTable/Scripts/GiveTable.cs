using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GiveTable : MonoBehaviour,IUseFurniture
{
    [SerializeField] private Transform ingredientPoint;
    [SerializeField] private Transform parentFood;
    [SerializeField] private SoundsFurniture sounds;
    
    private GameObject _ingredient;
    private bool _isHeroikTrigger;
    private Heroik _heroik;
    
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    
    private List<Product> _productsList;
    private ProductsFactory _productsFactory;

    [Inject]
    private void ConstructZenject(
        FoodsForFurnitureContainer foodsForFurnitureContainer,
        ProductsFactory productsFactory)
    {
        _productsFactory = productsFactory;
        _productsList = foodsForFurnitureContainer.GiveTable.ListForFurniture;
    }

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // if (_isInit == false)
        // {
        //     Debug.Log("Инициализация не закончена");
        //     return;
        // }
        
        if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
        {
            _outline.OutlineWidth = 2f;
            _isHeroikTrigger = true;
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
            _outline.OutlineWidth = 0f;
            _isHeroikTrigger = false;
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
        _ingredient = _productsFactory.GetProduct(acceptObj, ingredientPoint, parentFood,true);
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
                _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.ForbiddenSound);
                return;
            }
            
            // на столе что-то есть
            if (_heroik.TryPickUp(GiveObj(_ingredient)))
            {
                sounds.PlayOneShotClip(AudioNameGamePlay.TakeOnTheTableSound);
                CleanObjOnTable(_ingredient);
                return;
            } 
            
            _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.ForbiddenSound);
        }
        else // заняты
        {
            if (_ingredient == null) // ни одного активного объекта
            {
                if (!AcceptObject(_heroik.TryGiveIngredient(_productsList)))
                {
                    _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.ForbiddenSound);
                    Debug.Log("с предметом что-то пошло не так");
                    return;
                }
                
                sounds.PlayOneShotClip(AudioNameGamePlay.PutOnTheTableSound2);
                return;
            }
            
            _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.ForbiddenSound);
            Debug.Log("У вас полные руки и прилавок полон");
        }
    }

    private void CleanObjOnTable(GameObject ingredient)
    {
        Destroy(ingredient);
    }

    private bool CheckCookingProcess()
    {

        if(_isHeroikTrigger == false)
        {
            return false;
        }
        
        if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
        {
            _heroik.PlayOneShotClip?.Invoke(AudioNameGamePlay.NotWorkTableSound);
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
