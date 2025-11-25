using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Garbage : MonoBehaviour,IUseFurniture
{
    private Heroik _heroik;
    private bool _isHeroikTrigger;
    private GameObject _obj;
    private Outline _outline;
    private FoodsForFurnitureContainer _foodsForFurnitureContainer;
    private DecorationFurniture _decorationFurniture;
    
    private List<Product> ListProduct => _foodsForFurnitureContainer.Garbage.ListForFurniture;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
    }

    private void Start()
    {
        //Debug.Log("Garbage Init");
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
    
    private void OnEnable()
    {
        //EventBus.PressE += CookingProcess;
    }

    private void OnDisable()
    {
        //EventBus.PressE -= CookingProcess;
    }
    
    [Inject]
    private void ConstructZenject(FoodsForFurnitureContainer foodsForFurnitureContainer)
    {
        _foodsForFurnitureContainer = foodsForFurnitureContainer;
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
    
    private bool AcceptObject(GameObject acceptObj)
    {
        if (acceptObj == null)
        {
            Debug.Log("Объект не передался");
            return false;
        }
        _obj = acceptObj;
        //_heroik.CleanObjOnHands();
        return true;
    }
    
    private void CookingProcess()
    {
        if (_isHeroikTrigger == false)
        {
            return;
        }
        
        if (_decorationFurniture.DecorationTableTop == CustomFurnitureName.TurnOff )
        {
            Debug.LogWarning("Мусорка не работает");
            return;
        }
        
        if (CheckUseFurniture() == false)
        {
            return;
        }

        if (_heroik.IsBusyHands == true)
        {
            if (AcceptObject(_heroik.TryGiveIngredient(ListProduct)))
            {
                DeleteObj();
            }
            else
            {
                Debug.Log("с предметом что-то пошло не так");
            }
            
        }
        else
        {
            Debug.Log("Вам нечего выкидывать");
        }

    }
    
    private void DeleteObj()
    {
        _obj.SetActive(false);
        Destroy(_obj);
        _obj = null;
    }
    
}
