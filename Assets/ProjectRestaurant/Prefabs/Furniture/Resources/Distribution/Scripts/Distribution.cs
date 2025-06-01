using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Distribution : MonoBehaviour , IAcceptObject, ITurnOffOn
{ 
    
    [SerializeField] private Transform pointDish;
    [SerializeField] private Checks checks;
    
    private Animator _animator;
    private Outline _outline;
    private DecorationFurniture _decorationFurniture;
    private Heroik _heroik;
    
    private bool _isWork = false;
    private GameObject _currentDish;
    private bool _isHeroikTrigger = false;
    
    void Start()
    {
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
        _currentDish = StaticManagerWithoutZenject.ProductsFactory.GetProduct(acceptObj, pointDish, pointDish,true);
        Destroy(acceptObj);
    }

    public void TurnOff()
    {
        _animator.Play("None");
        _isWork = false;
    }

    public void TurnOn()
    {
        _animator.Play("Distribution");
        _isWork = true;
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
                    if (checks.CheckTheCheck(_heroik.CurrentTakenObjects))
                    {
                        Debug.Log("Это блюдо есть в чеках");
                        AcceptObject(_heroik.TryGiveIngredient());
                        TurnOn();
                        StartCookingProcessAsync();
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
    
    private async void StartCookingProcessAsync()
    {
        await Task.Delay(1850);
        TakeToTheHall();
        TurnOff();
    }
    
    private void TakeToTheHall()
    {
        _currentDish.SetActive(false);
        checks.DeleteCheck(_currentDish);
        Destroy(_currentDish);
        _currentDish = null;
    }
    
}
