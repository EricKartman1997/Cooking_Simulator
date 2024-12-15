using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : FurnitureAbstact
{
    private Animator _animator;
    
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    
    private Outline _outline;
    
    [SerializeField] private GameObject[] objectOnTheTable;
    [SerializeField] private GameObject[] readyFoods;
    
    private GameObject _ingedient_1 = null;
    private GameObject _ingedient_2 = null;
    private GameObject _ingedient_3 = null;
    private GameObject _result = null;
    private bool _isWork = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        //_animator.SetBool("Work", false);
        _outline = GetComponent<Outline>();
    }

    private IEnumerator OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            var heroik = other.GetComponent<Heroik>();
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
                                    heroik.ActiveObjHands(GiveObj(ref _ingedient_1));
                                }
                                else
                                {
                                    heroik.ActiveObjHands(GiveObj(ref _ingedient_2));
                                }
                            }
                        }
                        else
                        {
                            heroik.ActiveObjHands(GiveObj(ref _result));
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
                                if(heroik._curentTakenObjects.GetComponent<Interactable>() && heroik._curentTakenObjects.GetComponent<Fruit>())
                                {
                                    AcceptObject(heroik.GiveObjHands(), 1);
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
                                    if(heroik._curentTakenObjects.GetComponent<Interactable>() && heroik._curentTakenObjects.GetComponent<Fruit>())
                                    {
                                        AcceptObject(heroik.GiveObjHands(), 2);
                                        Debug.Log("Предмет второй положен в блендер");
                                    }
                                    else
                                    {
                                        Debug.Log("с предметом нельзя взаимодействовать");
                                    }
                                }
                                else
                                {
                                    if(heroik._curentTakenObjects.GetComponent<Interactable>() && heroik._curentTakenObjects.GetComponent<Fruit>())
                                    {
                                        AcceptObject(heroik.GiveObjHands(), 3);
                                        Debug.Log("Предмет третий положен в блендер");
                                        TurnOn(); 
                                        var objdish = FindReadyFood(_ingedient_1,_ingedient_2,_ingedient_3);
                                        yield return new WaitForSeconds(4f);
                                        TurnOff();
                                        CreateResult(objdish.name);
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
        foreach (var obj in readyFoods)
        {
            if (obj.name == "Rubbish")
            {
                return obj; 
            }
        } 
        return null;
    }
    protected override GameObject GiveObj(ref GameObject obj)
    {
        obj.SetActive(false);
        var cObj = obj;
        obj = null;
        return cObj;
    }

    protected override void AcceptObject(GameObject acceptObj, byte numberObj)
    {
        if (numberObj == 1)
        {
            foreach (var obj in objectOnTheTable)
            {
                if (obj.name == acceptObj.name)
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
                if (obj.name == acceptObj.name)
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
                if (obj.name == acceptObj.name)
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

    protected override void CreateResult(string nameBolud)
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

    protected override void TurnOn()
    {
        _isWork = true;
        _ingedient_1.SetActive(false);
        _ingedient_2.SetActive(false);
        _ingedient_3.SetActive(false);
        _animator.SetBool("Work", true);
        Instantiate(timer, timerPoint.position, Quaternion.identity,timerParent);
    }

    protected override void TurnOff()
    {
        _isWork = false;
        _ingedient_1 = null;
        _ingedient_2 = null;
        _ingedient_3 = null;
        _animator.SetBool("Work", false);
    }
}
