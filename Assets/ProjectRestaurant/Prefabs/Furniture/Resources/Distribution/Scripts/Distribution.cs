using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distribution : MonoBehaviour , IAcceptObject, ITurnOffOn
{ 
    private const string AnimNONE = "None";
    private const string AnimDISTRIBUTION = "Distribution";
    private Checks _checks;
    [SerializeField] private Transform pointDish;

    private InfoAboutCheck _currentCheck;
    private Animator _animator;
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    private Heroik _heroik;
    
    private bool _isWork = false;
    private GameObject _currentDish;
    private bool _isHeroikTrigger = false;
    
    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = StaticManagerWithoutZenject.GameManager;
        _checks = StaticManagerWithoutZenject.GameManager.Checks;
        _animator = GetComponent<Animator>();
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

    public void AcceptObject(GameObject acceptObj)
    {
        _currentDish = _gameManager.ProductsFactory.GetProduct(acceptObj, pointDish, pointDish,true);
        Destroy(acceptObj);
    }

    public void TurnOff()
    {
        _animator.Play(AnimNONE);
        //_animator.SetBool(AnimWORK,false);
        _isWork = false;
    }

    public void TurnOn()
    {
        _animator.Play(AnimDISTRIBUTION);
        //_animator.SetBool(AnimWORK,true);
        _isWork = true;
        _currentCheck.StopUpdateTime();
    }
    
    private void CookingProcess()
    {
        if(_isHeroikTrigger == false)
        {
            return;
        }
        
        if (_decorationFurniture.Config.DecorationTableTop == EnumDecorationTableTop.TurnOff )
        {
            Debug.LogWarning("Раздача не работает");
            return;
        }
        
        if(_heroik.IsBusyHands == false) // руки не заняты
        {
            Debug.Log("У вас пустые руки");
        }
        else// руки заняты
        {
            if (_isWork)
            {
                Debug.Log("Ждите блюдо еще не забрали");
            }
            else
            {
                if (_heroik.CanGiveIngredient(new List<Type>(){typeof(ObjsForDistribution)}))
                {
                    _currentCheck = _checks.CheckTheCheck(_heroik.CurrentTakenObjects);
                    if (_currentCheck!= null)
                    {
                        AcceptObject(_heroik.TryGiveIngredient());
                        TurnOn();
                        StartCoroutine(ContinueWorkCoroutine());
                    }
                    else
                    {
                        Debug.Log("Этого блюдо нет в чеках");
                    }
                }
                else 
                {
                    Debug.Log("Это блюдо нельзя подавать гостям");
                }
                        
            }
        }
    }
    
    private void TakeToTheHall()
    {
        _currentDish.SetActive(false);
        _checks.DeleteCheck(_currentCheck);
        Destroy(_currentDish);
        _currentDish = null;
    }
    
    private IEnumerator ContinueWorkCoroutine()
    {
        while (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Distribution"))
        {
            yield return null;
        }
        
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }
        TakeToTheHall();
        TurnOff();
    }
    
}
