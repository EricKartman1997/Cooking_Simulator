using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Distribution : MonoBehaviour , IAcceptObject, ITurnOffOn, IIsAllowDestroy, IHeroikIsTrigger
{ 
    //Initialize
    private Animator _animator;
    private Heroik _heroik; // только для объекта героя, а надо и другие...
    private Transform _pointDish;
    private Checks _checks;
    
    private bool _isWork = false;
    private GameObject _currentDish;
    
    private bool _isHeroikTrigger = false;
    
    public void Initialize(Heroik heroik,Transform pointDish,Animator animator,Checks checks)
    {
        _heroik = heroik;
        _pointDish = pointDish;
        _animator = animator;
        _checks = checks;
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
        _currentDish = StaticManagerWithoutZenject.ProductsFactory.GetProduct(acceptObj, _pointDish, _pointDish,true);
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

    public bool IsAllowDestroy()
    {
        if (_currentDish == null)
        {
            return true;
        }

        return false;
    }

    public void HeroikIsTrigger()
    {
        _isHeroikTrigger = !_isHeroikTrigger;
    }
    
    private void CookingProcess()
    {
        if(_isHeroikTrigger == true)
        {
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
                    if (_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForDistribution)}))
                    {
                        if (_checks.CheckTheCheck(_heroik.CurrentTakenObjects))
                        {
                            Debug.Log("Это блюдо есть в чеках");
                            AcceptObject(_heroik.GiveObjHands());
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
        _checks.DeleteCheck(_currentDish);
        Destroy(_currentDish);
        _currentDish = null;
    }
    
}
