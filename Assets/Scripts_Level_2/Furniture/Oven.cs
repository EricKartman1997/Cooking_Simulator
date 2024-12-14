using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Oven : FurnitureAbstact
{
    [SerializeField] private GameObject glassOn;
    [SerializeField] private GameObject glassOff;
    [SerializeField] private GameObject switchFirst;
    [SerializeField] private GameObject switchSecond;
    [SerializeField] private GameObject[] foodOnTheOver;
    [SerializeField] private GameObject[] cookedFoodOnTheOver;
    
    [SerializeField] private GameObject timer;
    [SerializeField] private Transform timerPoint;
    [SerializeField] private Transform timerParent;
    
    private Outline _outline;
    private bool _isWork = false;
    private GameObject _result;


    void Start()
    {
        _outline = GetComponent<Outline>();
    }

    private IEnumerator OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            var heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (!Heroik.IsBusyHands) // руки не заняты
                {
                    if (_isWork)
                    {
                        Debug.Log("ждите печка работает");
                    }
                    else
                    {
                        if (_result != null)
                        {
                            heroik.ActiveObjHands(GiveObj());
                        }
                        else
                        {
                            Debug.Log("печка пуста руки тоже");
                        }
                    }
                }
                else // заняты
                {
                    if (_isWork)
                    {
                        Debug.Log("ждите печка работает");
                    }
                    else
                    {
                        if (_result != null)
                        {
                            Debug.Log("Сначала заберите предмет");
                        }
                        else
                        {
                            int count = 0;
                            foreach (GameObject obj in foodOnTheOver)
                            {
                                if (heroik.GetCurentTakenObjects().name == obj.name)
                                {
                                    TurnOn();
                                    AcceptObject(heroik.GiveObjHands(),0);
                                    yield return new WaitForSeconds(5f);
                                    TurnOff();
                                    CreateResult(obj.name);
                                    break;
                                }
                                if(heroik.GetCurentTakenObjects().name != obj.name)
                                {
                                    count++;
                                    if (count == foodOnTheOver.Length)
                                    {
                                        Debug.LogError($"Из этого объекта {heroik.GetCurentTakenObjects().name} ничего нельзя пригоовить в духовке");
                                    }
                                }
                            }
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


    // private void TurnOnOven()
    // {
    //     _isWork = true;
    //     glassOff.SetActive(false);
    //     glassOn.SetActive(true);
    //     switchFirst.transform.rotation = Quaternion.Euler(0, 0, -90);
    //     switchSecond.transform.rotation = Quaternion.Euler(0, 0, -135);
    //     Instantiate(timer, timerPoint.position, Quaternion.identity,timerParent);
    // }
    // private void TurnOffOven()
    // {
    //     _isWork = false;
    //     glassOff.SetActive(true);
    //     glassOn.SetActive(false);
    //     switchFirst.transform.rotation = Quaternion.Euler(0, 0, 0);
    //     switchSecond.transform.rotation = Quaternion.Euler(0, 0, 0);
    // }

    // private GameObject IssuanceOfTheResult(GameObject obj)
    // {
    //     switch (obj.name)
    //     {
    //         case "Apple":
    //             foreach (var cookedFood in cookedFoodOnTheOver)
    //             {
    //                 if (cookedFood.name == "BakedApple")
    //                 {
    //                     cookedFood.SetActive(true);
    //                     return cookedFood;
    //                 }
    //                 else
    //                 {
    //                     Debug.Log("ошибка");
    //                 }
    //             }
    //             break;
    //         case "Orange":
    //             foreach (var cookedFood in cookedFoodOnTheOver)
    //             {
    //                 if (cookedFood.name == "BakedOrange")
    //                 {
    //                     cookedFood.SetActive(true);
    //                     return cookedFood;
    //                 }
    //                 else
    //                 {
    //                     Debug.Log("ошибка");
    //                 }
    //             } 
    //             break;
    //         
    //     }
    //     Debug.Log($"из этого {obj.name} продукта ничего не приготовить //Ошибка");
    //     return null;
    // }
    private GameObject IssuanceOfTheResult(GameObject obj)
    {
        foreach (var cookedFood in cookedFoodOnTheOver)
        {
            if (obj.name == "Apple")
            {
                if (cookedFood.name == "BakedApple")
                {
                    cookedFood.SetActive(true);
                    return cookedFood;
                }
            }
            else if (obj.name == "Orange")
            {
                if (cookedFood.name == "BakedOrange")
                {
                    cookedFood.SetActive(true);
                    return cookedFood;
                }
            }
            else if (obj.name == "Fish")
            {
                if (cookedFood.name == "BakedFish")
                {
                    cookedFood.SetActive(true);
                    return cookedFood;
                }
            }
            else if (obj.name == "Meat")
            {
                if (cookedFood.name == "BakedMeat")
                {
                    cookedFood.SetActive(true);
                    return cookedFood;
                }
            }
        }
        Debug.Log($"из этого {obj.name} продукта ничего не приготовить //Ошибка");
        return null;
    }

    protected override GameObject GiveObj()
    {
        Debug.Log("забираем предмет");
        _result.SetActive(false);
        var obj = _result;
        _result = null;
        return obj;
    }

    protected override void AcceptObject(GameObject obj, byte numberObj)
    {

    }

    protected override void CreateResult(string obj)
    {
        if (obj == "Apple")
        {
            FindObject("BakedApple");
        }
        else if (obj == "Orange")
        {
            FindObject("BakedOrange");
        }
        else if (obj == "Fish")
        {
            FindObject("BakedFish");
        }
        else if (obj == "Meat")
        {
            FindObject("BakedMeat");
        }
        else
        {
            Debug.Log($"из этого {obj} продукта ничего не приготовить //Ошибка");
        }
    }

    protected override void TurnOn()
    {
        _isWork = true;
        glassOff.SetActive(false);
        glassOn.SetActive(true);
        switchFirst.transform.rotation = Quaternion.Euler(0, 0, -90);
        switchSecond.transform.rotation = Quaternion.Euler(0, 0, -135);
        Instantiate(timer, timerPoint.position, Quaternion.identity,timerParent);
    }

    protected override void TurnOff()
    {
        _isWork = false;
        glassOff.SetActive(true);
        glassOn.SetActive(false);
        switchFirst.transform.rotation = Quaternion.Euler(0, 0, 0);
        switchSecond.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    private void FindObject(string obj)
    {
        foreach (var cookedFood in cookedFoodOnTheOver)
        {
            if (cookedFood.name == obj)
            {
                cookedFood.SetActive(true);
                _result = cookedFood;
            }
        }
    }
}
