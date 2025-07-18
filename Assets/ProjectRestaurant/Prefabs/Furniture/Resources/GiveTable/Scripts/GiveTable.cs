using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveTable : MonoBehaviour,IAcceptObject,IGiveObj
{
    [SerializeField] private Transform ingredientPoint;
    [SerializeField] private Transform parentFood;
    [SerializeField] private List<GameObject> unusableObjects;
    private GameObject _ingredient;
    private bool _isHeroikTrigger;
    private Heroik _heroik;
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    private GameManager _gameManager;
    private bool _isInit;

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
    
    private void CookingProcess()
    {
        if (_isInit == false)
        {
            Debug.Log("Инициализация не закончена");
            return;
        }
        
        if(_isHeroikTrigger == false)
        {
            return;
        }
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            Debug.LogWarning("Стол не работает");
            return;
        }
        
        if(_heroik.IsBusyHands == false) // руки не заняты
        {
            if (_ingredient == null) // ни одного активного объекта
            {
                Debug.Log("У вас пустые руки и прилавок пуст");
            }
            else // на столе что-то есть
            {
                _heroik.TryPickUp(GiveObj(ref _ingredient));
            }
        }
        else // заняты
        {
            if (_ingredient == null) // ни одного активного объекта
            {
                if (_heroik.CanGiveIngredient(unusableObjects))
                {
                    AcceptObject(_heroik.TryGiveIngredient());
                }
                else
                {
                    Debug.Log("Этот предмет положить нельзя");
                }
            }
            else // активного объект есть
            {
                Debug.Log("У вас полные руки и прилавок полон");
            }
        }
    }
    
    public void AcceptObject(GameObject acceptObj)
    {
        _ingredient = _gameManager.ProductsFactory.GetProduct(acceptObj, ingredientPoint, parentFood,true);
        Destroy(acceptObj);
    }
    
    public GameObject GiveObj(ref GameObject giveObj)
    {
        return giveObj;
    }
    
}
