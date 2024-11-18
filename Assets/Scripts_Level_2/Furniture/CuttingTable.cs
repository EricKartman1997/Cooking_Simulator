using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingTable : MonoBehaviour
{
    private Animator _animator;
    
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    
    [SerializeField] private GameObject[] objectOnTheTable;
    [SerializeField] private GameObject[] readyFoods;
    private GameObject _firstFood = null;
    private GameObject _secondFood = null;
    
    private Outline _outline;
    private GameObject _result = null;
    private bool _isWork = false;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Work", false);
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
                    if (_isWork)
                    {
                        Debug.Log("ждите блюдо готовится");
                    }
                    else
                    {
                        if (_result == null)
                        {
                            if (ActiveObjectsOnTheTable() == 1) //один активный объект
                            {
                                other.GetComponent<Heroik>().ActiveObjHands(_firstFood);
                                _firstFood.SetActive(false);
                                _firstFood = null;
                            }
                            else// активного объекта нет
                            {
                                Debug.Log("У вас пустые руки и прилавок пуст");
                            }
                        }
                        else // забрать предмет результат
                        {
                            other.GetComponent<Heroik>().ActiveObjHands(GiveResult());
                            _result = null;
                            //Debug.Log("Вы забрали конечный продукт"); 
                        }
                    }
                }
                else // заняты
                {
                    if (_isWork)
                    {
                        Debug.Log("ждите блюдо готовится");
                    }
                    else
                    {
                        if (_result == null)
                        {
                            if (ActiveObjectsOnTheTable() == 1 )//один активный объект
                            {
                                var nameBolud = _firstFood.GetComponent<Interactable>().IsMerge(other.GetComponent<Heroik>()._curentTakenObjects.GetComponent<Interactable>()) ;
                                if (nameBolud != "None")
                                {
                                    ToAcceptObjsFood(other.GetComponent<Heroik>().GiveObjHands(), 2);
                                    other.GetComponent<Heroik>()._curentTakenObjects = null;
                                    TurnOnCuttingTable(); 
                                    yield return new WaitForSeconds(3f);
                                    TurnOffCuttingTable(); 
                                    CreatResultObj(nameBolud);
                                }
                                else
                                {
                                    Debug.Log("Объект не подъходит для слияния");
                                }
                            }
                            else// активного объекта нет
                            {
                                if(other.GetComponent<Heroik>()._curentTakenObjects.GetComponent<Interactable>() && other.GetComponent<Heroik>()._curentTakenObjects.GetComponent<ObjsForCutting>())
                                {
                                    ToAcceptObjsFood(other.GetComponent<Heroik>().GiveObjHands(), 1);
                                    other.GetComponent<Heroik>()._curentTakenObjects = null;
                                }
                                else
                                {
                                    Debug.Log("с предметом нельзя взаимодействовать");
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Сначала уберите предемет из рук");
                        }
                        
                    }
                    
                }
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _outline.OutlineWidth = 0f;
        }
    }
    private byte ActiveObjectsOnTheTable()
    {
        if (_firstFood == null && _secondFood == null)
        {
            return 2;
        }
        else if (_firstFood == null && _secondFood != null || _firstFood != null && _secondFood == null)
        {
            return 1;
        }
        return 0; //  ошибка
    }
    private void ToAcceptObjsFood(GameObject acceptObjFood, byte numberObj)
    {
        if (numberObj == 1)
        {
            foreach (var obj in objectOnTheTable)
            {
                if (obj.name == acceptObjFood.name)
                {
                    obj.SetActive(true);
                    _firstFood = obj;
                }
            }
        }
        else if (numberObj == 2)
        {
            foreach (var obj in objectOnTheTable)
            {
                if (obj.name == acceptObjFood.name)
                {
                    obj.SetActive(true);
                    _secondFood = obj;
                }
            }
        }
        else
        {
            Debug.Log("Ошибка");
        }
    }
    private void CreatResultObj(string nameBolud)
    {
        foreach (var obj in readyFoods)
        {
            if (obj.name == nameBolud)
            {
                obj.SetActive(true);
                _result = obj;
            }
        }
    }
    private GameObject GiveResult()
    {
        _result.SetActive(false);
        return _result;
        // не забудь _result = null;
    }
    private void TurnOnCuttingTable()
    {
        _isWork = true;
        _firstFood.SetActive(false);
        _secondFood.SetActive(false);
        _animator.SetBool("Work", true);
        Instantiate(timer, timerPoint.position, Quaternion.identity,timerParent);
    }
    public void TurnOffCuttingTable()
    {
        _isWork = false;
        _firstFood = null;
        _secondFood = null;
        _animator.SetBool("Work", false);
    }
    public bool GetIsWork()
    {
        return _isWork;
    }
}
