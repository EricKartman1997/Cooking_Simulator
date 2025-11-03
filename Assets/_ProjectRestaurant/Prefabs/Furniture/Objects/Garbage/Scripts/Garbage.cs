using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Garbage : MonoBehaviour,IUseFurniture
{
    private Heroik _heroik;
    private bool _isHeroikTrigger;
    private bool _isInit;
    private GameObject _obj;
    private Outline _outline;
    private GameManager _gameManager;
    private FoodsForFurnitureContainer _foodsForFurnitureContainer;
    private DecorationFurniture _decorationFurniture;
    
    private bool IsAllInit => _gameManager.BootstrapLvl2.IsAllInit;
    
    private List<Product> ListProduct => _foodsForFurnitureContainer.Garbage.ListForFurniture;

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
        Debug.Log("Garbage Init");
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
    
    private void OnEnable()
    {
        //EventBus.PressE += CookingProcess;
    }

    private void OnDisable()
    {
        //EventBus.PressE -= CookingProcess;
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
        if (_isInit == false)
        {
            Debug.Log("Инициализация не закончена");
            return;
        }
        
        if (_isHeroikTrigger == false)
        {
            return;
        }
        
        if (_decorationFurniture.DecorationTableTop == EnumDecorationTableTop.TurnOff )
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
