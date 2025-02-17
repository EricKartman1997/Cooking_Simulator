using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Distribution : MonoBehaviour
{ 
    private Animator _animator;
    private Animation _animation;
    //private bool _isAnimator = false;
    
    private Outline _outline;
    private bool _isWork = false;
    [SerializeField] private GameObject _food = null;
    
    [SerializeField] private GameObject[] cookedFood;
    [SerializeField] private Checks checks;
    
    private bool _onTrigger = false;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private float _timeCurrent = 0.17f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _outline = GetComponent<Outline>();
    }

    private void Update()
    {
        _timeCurrent += Time.deltaTime;
        if (_onTrigger)
        {
            if(Input.GetKeyDown(KeyCode.E))
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
                            if (IsCheckDistribution(_heroik._curentTakenObjects))
                            {
                                if (IsCheckOrder(_heroik._curentTakenObjects))
                                {
                                    Debug.Log("Это блюдо есть в чеках");
                                    _food = _heroik.GiveObjHands();
                                    _heroik._curentTakenObjects = null;
                                    _animator.Play("Distribution");
                                    AcceptFood();
                                    StopCookingProcessAsync();
                                }
                                else
                                {
                                    Debug.Log("Это блюдо не было заказано");
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            _onTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = null;
            _outline.OutlineWidth = 0f;
            _onTrigger = false;
            StopAnimatorProcessAsync();
        }
    }
    
    private async void StopAnimatorProcessAsync()
    {
        await Task.Delay(1850);
        _animator.Play("None");
    }
    
    private async void StopCookingProcessAsync()
    {
        await Task.Delay(1850);
        TakeToTheHall();
    }

    private bool IsCheckDistribution(GameObject obj)
    {
        foreach (var food in cookedFood)
        {
            if (food.name == obj.name)
            {
                return true;
            }
        }
        return false;
    }
    private bool IsCheckOrder(GameObject obj)
    {
        if (checks.FirstCheck != null)
        {
            if (checks.FirstCheck.name == obj.name + "(Clone)")
            {
                return true;
            }
        }
        if (checks.SecondCheck != null)
        {
            if (checks.SecondCheck.name == obj.name + "(Clone)")
            {
                return true;
            }
        }
        if (checks.ThirdCheck != null)
        {
            if (checks.ThirdCheck.name == obj.name + "(Clone)")
            {
                return true;
            }
        }
        else
        {
            return false;
        }
        return false;
    }

    private void AcceptFood()
    {
        foreach (var obj in cookedFood)
        {
            if (obj.name == _food.name)
            {
                _food = obj;
                _food.SetActive(true);
            }
        }
    }
    private void TakeToTheHall()
    {
        _food.SetActive(false);
        //Heroik.Score++;
        checks.DeleteCheck(_food);
        _food = null;
    }
}
