using System.Collections.Generic;
using UnityEngine;

public class GiveTable : MonoBehaviour,IAcceptObject,IGiveObj
{
    [SerializeField] private GameObject _ingredient;
    [SerializeField] private Transform _ingredientPoint;
    [SerializeField] private Transform _parentFood;
    [SerializeField] private List<GameObject> _unusableObjects;
    
    private bool _isHeroikTrigger;
    private Heroik _heroik;
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    
    void Start()
    {
        _outline = GetComponent<Outline>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
    }
    private void OnTriggerEnter(Collider other)
    {
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
                if (_heroik.CanGiveIngredient(_unusableObjects))
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
        _ingredient = StaticManagerWithoutZenject.ProductsFactory.GetProduct(acceptObj, _ingredientPoint, _parentFood,true);
        Destroy(acceptObj);
    }
    
    public GameObject GiveObj(ref GameObject giveObj)
    {
        return giveObj;
    }
    
}
