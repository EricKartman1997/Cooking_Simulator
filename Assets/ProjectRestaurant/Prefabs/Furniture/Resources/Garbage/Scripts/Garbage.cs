using System;
using System.Collections;
using UnityEngine;
public class Garbage : MonoBehaviour, IAcceptObject
{
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private bool _isHeroikTrigger;
    private bool _isInit;
    private GameObject _obj;
    private Outline _outline;
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
    
    public void AcceptObject(GameObject acceptObj)
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
        
        try
        { 
            AcceptObject(_heroik.TryGiveIngredient());
            DeleteObj();
        }
        catch (Exception e)
        {
            Debug.Log("Вам нечего выкидывать" + e);
        }
    }
    
    private void DeleteObj()
    {
        _obj.SetActive(false);
        Destroy(_obj);
        _obj = null;
    }
    
}
