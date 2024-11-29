using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        _animator = GetComponent<Animator>();
        _outline = GetComponent<Outline>();
    }

    private IEnumerator OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _outline.OutlineWidth = 2f;
            if(Input.GetKeyDown(KeyCode.E))
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
                        if (IsCheckDistribution(other.GetComponent<Heroik>()._curentTakenObjects))
                        {
                            if (IsCheckOrder(other.GetComponent<Heroik>()._curentTakenObjects))
                            {
                                Debug.Log("Это блюдо есть в чеках");
                                _food = other.GetComponent<Heroik>().GiveObjHands();
                                other.GetComponent<Heroik>()._curentTakenObjects = null;
                                _animator.Play("Distribution");
                                AcceptFood();
                                yield return new WaitForSeconds(1.85f);
                                TakeToTheHall();
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
            }
        }
    }
    private IEnumerator OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _outline.OutlineWidth = 0f;
            yield return new WaitForSeconds(1.85f);
            _animator.Play("None");
        }
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
