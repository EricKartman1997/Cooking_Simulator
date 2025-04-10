using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour,  IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn,IIsAllowDestroy
{
    [SerializeField] private Transform positionRawFood;
    [SerializeField] private Transform parentRawFood;
    [SerializeField] private Transform positionReadyFood;
    [SerializeField] private Transform parentReadyFood;
    
    private List<Type> _unusableObjects;
    private Animator _animator;
    private Outline _outline;
    private StovePoints _stovePoints;
    private StoveView _stoveView;
    
    private bool _isHeroikTrigger = false;
    private bool _isDeleteHelperScripts = true;
    private Heroik _heroik;
    private GameObject _ingredient;
    private IForStove _componentForStove;
    private GameObject _result;
    void Start()
    {
        _outline = GetComponent<Outline>();
        _animator = GetComponent<Animator>();
        _unusableObjects = new List<Type>()
        {
            typeof(ObjsForStove)
            //typeof(IForStove)
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            if (_isDeleteHelperScripts == true)
            {
                _stovePoints = StaticManagerWithoutZenject.HelperScriptFactory.GetStovePoint(positionRawFood, parentRawFood, positionReadyFood, parentReadyFood);
                _stoveView = StaticManagerWithoutZenject.HelperScriptFactory.GetStoveView();

                _isDeleteHelperScripts = false;
            }
            else
            {
                Debug.Log("Новый скрипт создан не был");
            }
            _isHeroikTrigger = true;

            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = null;
            _outline.OutlineWidth = 0f;
            if (IsAllowDestroy())
            {
                _stovePoints.Dispose();
                _stovePoints = null;
            
                _stoveView.Dispose();
                _stoveView = null;
            
                _isDeleteHelperScripts = true;
            }
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
        if (_isHeroikTrigger == true)
        {
            if (_heroik.IsBusyHands == true)
            {
                if (_heroik.CheckObjForReturn(_unusableObjects))
                {
                    AcceptObject(_heroik.GiveObjHands());
                    
                    _componentForStove.IsOnStove = true;
                }
                else
                {
                    Debug.LogWarning("На объекте нет нужного компонента, объект нельзя положить");
                }
            }
            else
            {
                CreateResult(_ingredient);
                _heroik.AcceptProductWithTimeRemaining(GiveObj(ref _result));
            }
        }
    }

    public GameObject GiveObj(ref GameObject giveObj)
    {
        return giveObj;
    }

    public void AcceptObject(GameObject acceptObj)
    {
        BufferCutlet bufferCutlet = new BufferCutlet(acceptObj.GetComponent<IForStove>().TimeRemaining);
        _ingredient = StaticManagerWithoutZenject.ProductsFactory.GetProduct(acceptObj,_stovePoints.PositionRawFood,_stovePoints.ParentRawFood, true);
        _componentForStove = _ingredient.GetComponent<IForStove>();
        _ingredient.GetComponent<Cutlet>().UpdateTime(bufferCutlet.TimeRemaining);
        Destroy(acceptObj);
    }

    public void CreateResult(GameObject obj) // переделать буфер
    {
        _componentForStove.IsOnStove = false;
        _result = StaticManagerWithoutZenject.ProductsFactory.GetCutlet(_componentForStove.Roasting,_stovePoints.PositionReadyFood,_stovePoints.ParentReadyFood);
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
