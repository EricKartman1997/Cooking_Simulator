using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Distribution : MonoBehaviour , IAcceptObject, ITurnOffOn, IIsAllowDestroy, IHeroikIsTrigger
{ 
    //Initialize
    private Animator _animator;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private Transform _pointDish;
    
    private bool _isWork = false;
    [SerializeField] private GameObject _currentDish = null;
    [SerializeField] private Checks _checks;
    
    private bool _heroikIsTrigger = false;
    private float _timeCurrent = 0.17f;
    
    public void Initialize(Heroik heroik,Transform pointDish,Animator animator,Checks checks)
    {
        _heroik = heroik;
        _pointDish = pointDish;
        _animator = animator;
        _checks = checks;
    }
    
    private void Update()
    {
        _timeCurrent += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.E) && _heroikIsTrigger)
        {
            if (_timeCurrent >= 0.17f)
            {
                if(!Heroik.IsBusyHands) // руки не заняты
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
                            if (_checks.CheckTheCheck(_heroik.GetCurentTakenObjects()))
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
                _timeCurrent = 0f;
            }
            else
            {
                Debug.LogWarning("Ждите перезарядки кнопки");
            }
        }
    }
    
    private async void StartCookingProcessAsync()
    {
        await Task.Delay(1850);
        TakeToTheHall();
    }
    
    private void TakeToTheHall()
    {
        _currentDish.SetActive(false);
        _checks.DeleteCheck(_currentDish);
        Destroy(_currentDish);
        TurnOff();
    }

    public void AcceptObject(GameObject acceptObj)
    {
        _currentDish = acceptObj;
        _currentDish = Instantiate(_currentDish, _pointDish.position, Quaternion.identity, _pointDish);
        _currentDish.name = _currentDish.name.Replace("(Clone)", "");
        _currentDish.SetActive(true);
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
        _heroikIsTrigger = !_heroikIsTrigger;
    }
    
}
