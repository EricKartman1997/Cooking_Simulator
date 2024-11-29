using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suvide : MonoBehaviour
{
    private Animator _animator;
    private Outline _outline;
    
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private GameObject switchTimePrefab;
    [SerializeField] private GameObject switchTemperPrefab;
    
    [SerializeField] private Timer_Prefab firstTimer;
    [SerializeField] private Timer_Prefab secondTimer;
    [SerializeField] private Timer_Prefab thirdTimer;
    
    [SerializeField] private GameObject[] food;
    [SerializeField] private GameObject[] readyFood;
    
    private bool _isWork = false;
    
    private GameObject _firstResult = null;
    private GameObject _secondResult = null;
    private GameObject _thirdResult = null;
    
    private GameObject _ingedient_1 = null;
    private GameObject _ingedient_2 = null;
    private GameObject _ingedient_3 = null;
    
    private bool _isCookedFirstResult = false;
    private bool _isCookedSecondResult = false;
    private bool _isCookedThirdResult = false;
    
    private Heroik _heroik;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        //_animator.SetBool("Work", false);
        _outline = GetComponent<Outline>();
    }

    private void Update()
    {
        if (_isCookedFirstResult || _isCookedSecondResult || _isCookedThirdResult)
        {
            _isWork = true;
            WorkingSuvide();
        }
        else
        {
            _isWork = false;
            NotWorkingSuvide();
        }
    }

    private IEnumerator OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _heroik = other.GetComponent<Heroik>();
            _outline.OutlineWidth = 2f;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(!Heroik.IsBusyHands) // руки не заняты
                {
                    if (_firstResult != null)
                    {
                        GiveResult(1);
                    }
                    else
                    {
                        if (_secondResult != null)
                        {
                            GiveResult(2);
                        }
                        else
                        {
                            if (_thirdResult != null)
                            {
                                GiveResult(3);
                            }
                            else
                            {
                                Debug.Log("Сувид пуст руки тоже");
                            }
                        }
                    }
                }
                else // руки заняты
                {
                    if (_isWork) // работает
                    {
                        if (_firstResult == null && !_isCookedFirstResult)
                        {
                            //проверка на подъодит ли предмет
                            if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
                            {
                                ToAcceptObjsFood(_heroik.GiveObjHands(),1);
                                _heroik._curentTakenObjects = null;
                                TurnOnSuvide(firstTimer, out _isCookedFirstResult);
                                yield return new WaitForSeconds(10);
                                CreatResult(1);
                                TurnOffSuvide(1);
                            }
                            else
                            {
                                Debug.Log("продукт не подходит для сувида");
                            }
                        }
                        else if (_firstResult != null || (_firstResult == null && _isCookedFirstResult))
                        {
                            if (_secondResult == null && !_isCookedSecondResult)
                            {
                                //проверка на подъодит ли предмет
                                if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
                                {
                                    ToAcceptObjsFood(_heroik.GiveObjHands(),2);
                                    _heroik._curentTakenObjects = null;
                                    TurnOnSuvide(secondTimer, out _isCookedSecondResult);
                                    yield return new WaitForSeconds(10);
                                    CreatResult(2);
                                    TurnOffSuvide(2);
                                }
                                else
                                {
                                    Debug.LogWarning("продукт не подходит для сувида");
                                }
                            }
                            else if (_secondResult != null || (_secondResult == null && _isCookedSecondResult))
                            {
                                if (_thirdResult == null && !_isCookedThirdResult)
                                {
                                    //проверка на подъодит ли предмет
                                    if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
                                    {
                                        ToAcceptObjsFood(_heroik.GiveObjHands(),3);
                                        _heroik._curentTakenObjects = null;
                                        TurnOnSuvide(thirdTimer, out _isCookedThirdResult);
                                        yield return new WaitForSeconds(10);
                                        CreatResult(3);
                                        TurnOffSuvide(3);
                                    }
                                    else
                                    {
                                        Debug.LogWarning("продукт не подходит для сувида");
                                    }
                                }
                                else if (_thirdResult != null || (_thirdResult == null && _isCookedThirdResult))
                                {
                                    Debug.LogWarning("сувид заполнен");
                                }
                                else
                                {
                                    Debug.LogError("Условие не сработало");
                                }
                            }
                            else
                            {
                                Debug.LogError("Условие не сработало");
                            }
                        }
                        else
                        {
                            Debug.LogError("Условие не сработало");
                        }
                    }
                    else // не работает
                    {
                        if (_firstResult != null)
                        {
                            if (_secondResult != null)
                            {
                                if (_thirdResult != null)
                                {
                                    Debug.LogWarning("Сувид полный заберите готовый продукт");
                                }
                                else
                                {
                                    //проверка на подъодит ли предмет
                                    if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
                                    {
                                        ToAcceptObjsFood(_heroik.GiveObjHands(),3);
                                        _heroik._curentTakenObjects = null;
                                        TurnOnSuvide(thirdTimer, out _isCookedThirdResult);
                                        yield return new WaitForSeconds(10);
                                        CreatResult(3);
                                        TurnOffSuvide(3);
                                    }
                                    else
                                    {
                                        Debug.LogWarning("продукт не подходит для сувида");
                                    }
                                }
                            }
                            else
                            {
                                //проверка на подъодит ли предмет
                                if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
                                {
                                    ToAcceptObjsFood(_heroik.GiveObjHands(),2);
                                    _heroik._curentTakenObjects = null;
                                    TurnOnSuvide(secondTimer, out _isCookedSecondResult);
                                    yield return new WaitForSeconds(10);
                                    CreatResult(2);
                                    TurnOffSuvide(2);
                                }
                                else
                                {
                                    Debug.LogWarning("продукт не подходит для сувида");
                                }
                            }
                        }
                        else
                        {
                            //проверка на подъодит ли предмет
                            if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
                            {
                                ToAcceptObjsFood(_heroik.GiveObjHands(),1);
                                _heroik._curentTakenObjects = null;
                                TurnOnSuvide(firstTimer, out _isCookedFirstResult);
                                yield return new WaitForSeconds(10);
                                CreatResult(1);
                                TurnOffSuvide(1);
                            }
                            else
                            {
                                Debug.LogWarning("продукт не подходит для сувида");
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
            _heroik = null;
        }
    }
    private void GiveResult(byte numberObj)
    {
        if (numberObj == 1)
        {
            if (_firstResult.name == "firstBakedMeat")
            {
                _firstResult.name = "BakedMeat";
                _heroik.ActiveObjHands(_firstResult);
                _firstResult.SetActive(false);
                _firstResult = null;
            }
            else if(_firstResult.name == "firstBakedFish")
            {
                _firstResult.name = "BakedFish";
                _heroik.ActiveObjHands(_firstResult);
                _firstResult.SetActive(false);
                _firstResult = null;
            }
            else
            {
                Debug.Log("Ошибка в названии результата");
            }

        }
        if (numberObj == 2)
        {
            if (_secondResult.name == "secondBakedMeat")
            {
                _secondResult.name = "BakedMeat";
                _heroik.ActiveObjHands(_secondResult);
                _secondResult.SetActive(false);
                _secondResult = null;
            }
            else if(_secondResult.name == "secondBakedFish")
            {
                _secondResult.name = "BakedFish";
                _heroik.ActiveObjHands(_secondResult);
                _secondResult.SetActive(false);
                _secondResult = null;
            }
            else
            {
                Debug.LogError("Ошибка в названии результата");
            }
        }
        if (numberObj == 3)
        {
            if (_thirdResult.name == "thirdBakedMeat")
            {
                _thirdResult.name = "BakedMeat";
                _heroik.ActiveObjHands(_thirdResult);
                _thirdResult.SetActive(false);
                _thirdResult = null;
            }
            else if(_thirdResult.name == "thirdBakedFish")
            {
                _thirdResult.name = "BakedFish";
                _heroik.ActiveObjHands(_thirdResult);
                _thirdResult.SetActive(false);
                _thirdResult = null;
            }
            else
            {
                Debug.LogError("Ошибка в названии результата");
            }
        }

    }
    // private void CreatResult(byte numberObj)
    // {
    //     if (numberObj == 1)
    //     {
    //         if (_ingedient_1.name == "firstMeat")
    //         {
    //             foreach (var obj in readyFood)
    //             {
    //                 if (obj.name == "firstBakedMeat")
    //                 {
    //                     _firstResult = obj;
    //                     _firstResult.SetActive(true);
    //                 }
    //             }
    //         }
    //         if (_ingedient_1.name == "firstFish")
    //         {
    //             foreach (var obj in readyFood)
    //             {
    //                 if (obj.name == "firstBakedFish")
    //                 {
    //                     _firstResult = obj;
    //                     _firstResult.SetActive(true);
    //                 }
    //             }
    //         }
    //     }
    //     if (numberObj == 2)
    //     {
    //         if (_ingedient_2.name == "secondMeat")
    //         {
    //             foreach (var obj in readyFood)
    //             {
    //                 if (obj.name == "secondBakedMeat")
    //                 {
    //                     _secondResult = obj;
    //                     _secondResult.SetActive(true);
    //                 }
    //             }
    //         }
    //         if (_ingedient_2.name == "secondFish")
    //         {
    //             foreach (var obj in readyFood)
    //             {
    //                 if (obj.name == "secondBakedFish")
    //                 {
    //                     _secondResult = obj;
    //                     _secondResult.SetActive(true);
    //                 }
    //             }
    //         }
    //     }
    //     if (numberObj == 3)
    //     {
    //         if (_ingedient_3.name == "thirdMeat")
    //         {
    //             foreach (var obj in readyFood)
    //             {
    //                 if (obj.name == "thirdBakedMeat")
    //                 {
    //                     _thirdResult = obj;
    //                     _thirdResult.SetActive(true);
    //                 }
    //             }
    //         }
    //         if (_ingedient_3.name == "thirdFish")
    //         {
    //             foreach (var obj in readyFood)
    //             {
    //                 if (obj.name == "thirdBakedFish")
    //                 {
    //                     _thirdResult = obj;
    //                     _thirdResult.SetActive(true);
    //                 }
    //             }
    //         }
    //     }
    //
    // }
    private void TurnOnSuvide(Timer_Prefab timer,out bool isCooked)
    {
        _isWork = true;
        isCooked = true;
        //_animator.SetBool("Work", true);
        Instantiate(timer.timer, timer.timerPoint.position, Quaternion.identity,timer.timerParent);
    }
    private void TurnOffSuvide(byte numberObj)
    {
        switch (numberObj)
        {
            case 1:
                _isWork = false;
                _isCookedFirstResult = false;
                _ingedient_1.SetActive(false);
                _ingedient_1 = null;
                //_animator.SetBool("Work", false);
            break;
            case 2:
                _isWork = false;
                _isCookedSecondResult = false;
                _ingedient_2.SetActive(false);
                _ingedient_2 = null;
                //_animator.SetBool("Work", false);
                break;
            case 3:
                _isWork = false;
                _isCookedThirdResult = false;
                _ingedient_3.SetActive(false);
                _ingedient_3 = null;
                //_animator.SetBool("Work", false);
                break;
            default: Debug.LogError("НЕПРАВИЛЬНЫЙ ИНДЕКС");  break;
        }

    }
    private void ToAcceptObjsFood(GameObject acceptObjFood, byte numberObj)
    {
        if (numberObj == 1)
        {
            foreach (var obj in food)
            {
                if (obj.name == "first" + acceptObjFood.name )
                {
                    obj.SetActive(true);
                    _ingedient_1 = obj;
                }
            }
        }
        else if (numberObj == 2)
        {
            foreach (var obj in food)
            {
                if (obj.name == "second" + acceptObjFood.name)
                {
                    obj.SetActive(true);
                    _ingedient_2 = obj;
                }
            }
        }
        else if (numberObj == 3)
        {
            foreach (var obj in food)
            {
                if (obj.name == "third" + acceptObjFood.name)
                {
                    obj.SetActive(true);
                    _ingedient_3 = obj;
                }
            }
        }
        else
        {
            Debug.LogError("Ошибка");
        }
    }
    
    private void CreatResult(byte numberObj)
{
    string ingredientName = GetIngredientName(numberObj);
    string bakedName = GetBakedName(numberObj);


    if (string.IsNullOrEmpty(ingredientName) || string.IsNullOrEmpty(bakedName))
    {
        Debug.LogError($"Missing ingredient or baked name for number {numberObj}");
        return;
    }

    GameObject result = FindResultObject(readyFood, bakedName);

    if (result != null)
    {
        AssignResult(numberObj, result);
        result.SetActive(true);
    }
    else
    {
        Debug.LogWarning($"Could not find {bakedName} in readyFood list.");
    }
}
    private string GetIngredientName(byte numberObj) 
    {
        switch (numberObj)
        {
            case 1: return _ingedient_1.name;
            case 2: return _ingedient_2.name;
            case 3: return _ingedient_3.name;
            default:
                Debug.LogError($"Invalid numberObj: {numberObj}");
                return null;
        }
    }
    private string GetBakedName(byte numberObj)
    {
        string prefix = "";
        switch (numberObj)
        {
            case 1: prefix = "first"; break;
            case 2: prefix = "second"; break;
            case 3: prefix = "third"; break;
            default:
                Debug.LogError($"Invalid numberObj: {numberObj}");
                return null; // Возвращаем null для некорректного номера
        }

        string ingredientType = GetIngredientType(numberObj); // Новая функция, см. ниже

        if (!string.IsNullOrEmpty(ingredientType))
        {
            return prefix + "Baked" + ingredientType;
        }
        else
        {
            return null;
        }
    }
    private string GetIngredientType(byte numberObj)
    {
        string ingredientName = null;

        switch (numberObj)
        {
            case 1: ingredientName = _ingedient_1.name; break;
            case 2: ingredientName = _ingedient_2.name; break;
            case 3: ingredientName = _ingedient_3.name; break;
            default:
                Debug.LogError($"Invalid numberObj: {numberObj}");
                return null;
        }

        if (string.IsNullOrEmpty(ingredientName)) return null; // Дополнительная проверка

        if (ingredientName.Contains("Meat"))
        {
            return "Meat";
        }
        else if (ingredientName.Contains("Fish"))
        {
            return "Fish";
        }
        else
        {
            Debug.LogWarning($"Unknown ingredient type: {ingredientName}");
            return null; // Или можно вернуть "" - зависит от вашей логики
        }
    }
    private GameObject FindResultObject(GameObject[] objects, string name)
    {
        foreach (var obj in objects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }
    private void AssignResult(byte numberObj, GameObject result)
    {
        switch (numberObj)
        {
            case 1:
                _firstResult = result;
                break;
            case 2:
                _secondResult = result;
                break;
            case 3:
                _thirdResult = result;
                break;
            default:
                Debug.LogError($"Invalid numberObj: {numberObj}");
                break;
        }
    }

    private void WorkingSuvide()
    {
        waterPrefab.SetActive(true);
        switchTemperPrefab.transform.rotation = Quaternion.Euler(45, -90, -90);
        switchTimePrefab.transform.rotation = Quaternion.Euler(175, -90, -90);
    }
    private void NotWorkingSuvide()
    {
        waterPrefab.SetActive(false);
        switchTemperPrefab.transform.rotation = Quaternion.Euler(90, -90, -90);
        switchTimePrefab.transform.rotation = Quaternion.Euler(90, -90, -90);
    }

}


