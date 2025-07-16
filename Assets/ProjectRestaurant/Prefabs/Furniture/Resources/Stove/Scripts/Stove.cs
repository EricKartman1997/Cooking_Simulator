using System;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour,  IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn,IIsAllowDestroy
{
    [SerializeField] private Transform positionRawFood;
    
    private List<Type> _unusableObjects;
    private Animator _animator;
    private Outline _outline;
    private StovePoints _stovePoints;
    private StoveView _stoveView;
    private DecorationFurniture _decorationFurniture;
    
    private bool _isHeroikTrigger;
    private Heroik _heroik;
    private GameObject _ingredient;
    private IForStove _componentForStove;
    private GameObject _result;
    
    private GameManager _gameManager;
    
    void Start()
    {
        _gameManager = StaticManagerWithoutZenject.GameManager;
        _stovePoints = _gameManager.HelperScriptFactory.GetStovePoints(positionRawFood);
        _stoveView = _gameManager.HelperScriptFactory.GetStoveView();
        
        _outline = GetComponent<Outline>();
        _animator = GetComponent<Animator>();
        _decorationFurniture = GetComponent<DecorationFurniture>();
        _unusableObjects = new List<Type>()
        {
            typeof(ObjsForStove)
            //typeof(IForStove)
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            _isHeroikTrigger = true;
            _outline.OutlineWidth = 2f;
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
            _isHeroikTrigger = false;
            _outline.OutlineWidth = 0f;
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
            Debug.LogWarning("Плита не работает");
            return;
        }
        
        if (_heroik.IsBusyHands == true)
        {
            if (_heroik.CanGiveIngredient(_unusableObjects))
            {
                AcceptObject(_heroik.TryGiveIngredient());
                    
                _componentForStove.IsOnStove = true;
            }
            else
            {
                Debug.LogWarning("На объекте нет нужного компонента, объект нельзя положить");
            }
        }
        else
        {
            if (_ingredient != null)
            {
                CreateResult(_ingredient);
                _heroik.TryPickUp(GiveObj(ref _result));
            }
            else
            {
                Debug.LogWarning("Забирать нечего");
            }

        }
    }

    public GameObject GiveObj(ref GameObject giveObj)
    {
        return giveObj;
    }

    public void AcceptObject(GameObject acceptObj)
    {
        _ingredient = _gameManager.ProductsFactory.GetProduct(acceptObj,_stovePoints.PositionRawFood,_stovePoints.PositionRawFood, true);
        _componentForStove = _ingredient.GetComponent<IForStove>();
        Destroy(acceptObj);
    }

    public void CreateResult(GameObject obj) 
    {
        _componentForStove.IsOnStove = false;
        _result = _gameManager.ProductsFactory.GetCutlet(_componentForStove.Roasting);
        _result.GetComponent<Cutlet>().UpdateTime(_componentForStove.TimeRemaining); 
        Destroy(_ingredient);
        _ingredient = null;
        _componentForStove = null;
    }

    public void TurnOff()
    {
        throw new NotImplementedException();
    }

    public void TurnOn()
    {
        throw new NotImplementedException();
    }

    public bool IsAllowDestroy()
    {
        if (_ingredient == null && _result == null )
        {
            return true;
        }
        return false;
    }
}
