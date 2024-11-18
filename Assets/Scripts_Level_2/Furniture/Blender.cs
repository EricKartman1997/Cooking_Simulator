using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    private Animator _animator;
    
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    
    private Outline _outline;
    
    [SerializeField] private GameObject[] objectOnTheTable;
    [SerializeField] private GameObject[] readyFoods;
    [SerializeField] private GameObject rubbish;
    
    private GameObject _ingedient_1 = null;
    private GameObject _ingedient_2 = null;
    private GameObject _ingedient_3 = null;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(!Heroik.IsBusyHands) // руки не заняты
                {
                    if (_isWork)
                    {
                        Debug.Log("ждите блэндер готовится");
                    }
                    else
                    {
                        if (_result == null)
                        {
                            if (_ingedient_1 == null)
                            {
                                Debug.Log("Руки пусты ингридиентов нет");
                            }
                            else
                            {
                                if (_ingedient_2 == null)
                                {
                                    other.GetComponent<Heroik>().ActiveObjHands(_ingedient_1);
                                    _ingedient_1.SetActive(false);
                                    _ingedient_1 = null;
                                }
                                else
                                {
                                    other.GetComponent<Heroik>().ActiveObjHands(_ingedient_2);
                                    _ingedient_2.SetActive(false);
                                    _ingedient_2 = null;
                                }
                            }
                        }
                        else
                        {
                            other.GetComponent<Heroik>().ActiveObjHands(GiveResult());
                            _result = null;
                        }
                    }
                }
                else // руки заняты
                {
                    if (_isWork)
                    {
                        Debug.Log("ждите блэндер готовится");
                    }
                    else
                    {
                        if (_result == null)
                        {
                            if (_ingedient_1 == null)
                            {
                                if(other.GetComponent<Heroik>()._curentTakenObjects.GetComponent<Interactable>() && other.GetComponent<Heroik>()._curentTakenObjects.GetComponent<Fruit>())
                                {
                                    ToAcceptObjsFood(other.GetComponent<Heroik>().GiveObjHands(), 1);
                                    other.GetComponent<Heroik>()._curentTakenObjects = null;
                                    Debug.Log("Предмет первый положен в блендер");
                                }
                                else
                                {
                                    Debug.Log("с предметом нельзя взаимодействовать");
                                }
                            }
                            else
                            {
                                if (_ingedient_2 == null)
                                {
                                    if(other.GetComponent<Heroik>()._curentTakenObjects.GetComponent<Interactable>() && other.GetComponent<Heroik>()._curentTakenObjects.GetComponent<Fruit>())
                                    {
                                        ToAcceptObjsFood(other.GetComponent<Heroik>().GiveObjHands(), 2);
                                        other.GetComponent<Heroik>()._curentTakenObjects = null;
                                        Debug.Log("Предмет второй положен в блендер");
                                    }
                                    else
                                    {
                                        Debug.Log("с предметом нельзя взаимодействовать");
                                    }
                                }
                                else
                                {
                                    if(other.GetComponent<Heroik>()._curentTakenObjects.GetComponent<Interactable>() && other.GetComponent<Heroik>()._curentTakenObjects.GetComponent<Fruit>())
                                    {
                                        ToAcceptObjsFood(other.GetComponent<Heroik>().GiveObjHands(), 3);
                                        other.GetComponent<Heroik>()._curentTakenObjects = null;
                                        Debug.Log("Предмет третий положен в блендер");
                                        TurnOnBlender(); 
                                        var objdish = FindReadyFood(_ingedient_1,_ingedient_2,_ingedient_3);
                                        yield return new WaitForSeconds(4f);
                                        TurnOffBlender();
                                        CreatResultObj(objdish);
                                    }
                                    else
                                    {
                                        Debug.Log("с предметом нельзя взаимодействовать");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Руки полные уберите предмет");
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
    
    private GameObject GiveResult()
    {
        _result.SetActive(false);
        return _result;
        // не забудь _result = null;
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
                    _ingedient_1 = obj;
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
                    _ingedient_2 = obj;
                }
            }
        }
        else if (numberObj == 3)
        {
            foreach (var obj in objectOnTheTable)
            {
                if (obj.name == acceptObjFood.name)
                {
                    obj.SetActive(true);
                    _ingedient_3 = obj;
                }
            }
        }
        else
        {
            Debug.Log("Ошибка");
        }
    }

    private GameObject FindReadyFood(GameObject ingedient_1, GameObject ingedient_2, GameObject ingedient_3)
    {
        string Apple = "Apple";
        string Orange = "Orange";
        string Cherry = "Cherry";
        string Strawberry = "Strawberry";
        string Lime = "Lime";
        string Blueberry = "Blueberry";
        
        if (ingedient_1.name == Apple)
        {
            if (ingedient_2.name == Lime)
            {
                if (ingedient_3.name == Strawberry)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "FreshnessCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
            if (ingedient_2.name == Strawberry)
            {
                if (ingedient_3.name == Lime)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "FreshnessCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
        }
        else if (ingedient_1.name == Orange)
        {
            if (ingedient_2.name == Cherry)
            {
                if (ingedient_3.name == Blueberry)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "WildBerryCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
            if (ingedient_2.name == Blueberry)
            {
                if (ingedient_3.name == Cherry)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "WildBerryCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
        }
        else if (ingedient_1.name == Cherry)
        {
            if (ingedient_2.name == Orange)
            {
                if (ingedient_3.name == Blueberry)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "WildBerryCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
            if (ingedient_2.name == Blueberry)
            {
                if (ingedient_3.name == Orange)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "WildBerryCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
        }
        else if (ingedient_1.name == Strawberry)
        {
            if (ingedient_2.name == Lime)
            {
                if (ingedient_3.name == Apple)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "FreshnessCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
            if (ingedient_2.name == Apple)
            {
                if (ingedient_3.name == Lime)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "FreshnessCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
        }
        else if (ingedient_1.name == Lime)
        {
            if (ingedient_2.name == Strawberry)
            {
                if (ingedient_3.name == Apple)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "FreshnessCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
            if (ingedient_2.name == Apple)
            {
                if (ingedient_3.name == Strawberry)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "FreshnessCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
        }
        else if (ingedient_1.name == Blueberry)
        {
            if (ingedient_2.name == Orange)
            {
                if (ingedient_3.name == Cherry)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "WildBerryCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
            if (ingedient_2.name == Cherry)
            {
                if (ingedient_3.name == Orange)
                {
                    foreach (var obj in readyFoods)
                    {
                        if (obj.name == "WildBerryCocktail")
                        {
                            return obj; //свежесть
                        }
                    }
                }
            }
        }
        else
        {
            return rubbish;
        }
        return rubbish;
    }
    private void CreatResultObj(GameObject obj)
    {
        if (obj != null)
        {
            obj.SetActive(true);
            _result = obj;
        }
    }
    private void TurnOnBlender()
    {
        _isWork = true;
        _ingedient_1.SetActive(false);
        _ingedient_2.SetActive(false);
        _ingedient_3.SetActive(false);
        _animator.SetBool("Work", true);
        Instantiate(timer, timerPoint.position, Quaternion.identity,timerParent);
    }
    public void TurnOffBlender()
    {
        _isWork = false;
        _ingedient_1 = null;
        _ingedient_2 = null;
        _ingedient_3 = null;
        _animator.SetBool("Work", false);
    }
}
