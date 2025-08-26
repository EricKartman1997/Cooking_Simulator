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
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            EnterTrigger();
            return;
        }
        
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
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
            ExitTrigger();
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
    
    private void AcceptObject(GameObject acceptObj)
    {
        _obj = acceptObj;
        Destroy(acceptObj);
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
            Debug.LogWarning("Мусорка не работает");
            return;
        }
        
        if (CheckUseFurniture() == false)
        {
            return;
        }

        if (_heroik.IsBusyHands == true)
        {
            if (!_heroik.CanGiveIngredient(ListProduct))
            {
                Debug.Log("Объект нельзя выбросить");
                return;
            }
            AcceptObject(_heroik.TryGiveIngredient());
            DeleteObj();
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
