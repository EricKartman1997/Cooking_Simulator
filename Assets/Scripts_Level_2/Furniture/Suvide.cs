using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Suvide : MonoBehaviour, IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn,IIsAllowDestroy,IHeroikIsTrigger
{
    // Initialize
    [SerializeField] private GameObject _waterPrefab;
    [SerializeField] private GameObject _switchTimePrefab;
    [SerializeField] private GameObject _switchTemperPrefab;
    [SerializeField] private Timer_Prefab _firstTimer;
    [SerializeField] private Timer_Prefab _secondTimer;
    [SerializeField] private Timer_Prefab _thirdTimer;
    [SerializeField]private SuvidePoints _suvidePoints;
    [SerializeField]private Animator _animator;
    [SerializeField]private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    private Dictionary<string, ReadyFood> _dictionaryRecipes;
    
    [SerializeField]private GameObject _result1 = null;
    [SerializeField]private GameObject _result2 = null;
    [SerializeField]private GameObject _result3 = null;
    //private GameObject _cloneResult1;
    //private GameObject _cloneResult2;
    //private GameObject _cloneResult3;
    [SerializeField]private GameObject _ingredient1 = null;
    [SerializeField]private GameObject _ingredient2 = null;
    [SerializeField]private GameObject _ingredient3 = null;
    [SerializeField]private GameObject _cloneIngredient1; // мб убрать
    [SerializeField]private GameObject _cloneIngredient2;
    [SerializeField]private GameObject _cloneIngredient3;
    [SerializeField]private bool _isCookedFirstResult = false;
    [SerializeField]private bool _isCookedSecondResult = false;
    [SerializeField]private bool _isCookedThirdResult = false;
    [SerializeField]private bool _readyResult1 = false; 
    [SerializeField]private bool _readyResult2 = false; 
    [SerializeField]private bool _readyResult3 = false; 
    
    private bool _isWork = false;
    private float _timeCurrent = 0.17f;
    private bool _heroikIsTrigger = false;

    public void Initialize(Animator animator,Heroik heroik,Timer_Prefab firstTimer,
        Timer_Prefab secondTimer, Timer_Prefab thirdTimer, GameObject switchTemperPrefab,GameObject switchTimePrefab,
        GameObject waterPrefab,SuvidePoints suvidePoints,Dictionary<string, ReadyFood> recipes)
    {
        _animator = animator;
        _heroik = heroik;
        _firstTimer = firstTimer;
        _secondTimer = secondTimer;
        _thirdTimer = thirdTimer;
        _switchTemperPrefab = switchTemperPrefab;
        _switchTimePrefab = switchTimePrefab;
        _waterPrefab = waterPrefab;
        _suvidePoints = suvidePoints;
        _dictionaryRecipes = recipes;
    }
    
    private void Update()
    {
        // переделать в отдельный метод
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
        
        _timeCurrent += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && _heroikIsTrigger)
        {
            if (_timeCurrent >= 0.17f)
            {
                if(!Heroik.IsBusyHands) // руки не заняты
                {
                    if (_result1 != null)
                    {
                        _heroik.ActiveObjHands(GiveObj(ref _result1));
                    }
                    else
                    {
                        if (_result2 != null)
                        {
                            _heroik.ActiveObjHands(GiveObj(ref _result2));
                        }
                        else
                        {
                            if (_result3 != null)
                            {
                                _heroik.ActiveObjHands(GiveObj(ref _result3));
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
                    // if (_isWork) // работает
                    // {
                    //     
                    // }
                    if (_result1 == null && !_isCookedFirstResult)
                    {
                        //проверка на подъодит ли предмет
                        if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
                        {
                            AcceptObject(_heroik.GiveObjHands());
                            TurnOn();
                            StartCookingProcessAsync(_ingredient1);
                        }
                        else
                        {
                            Debug.Log("продукт не подходит для сувида");
                        }
                    }
                    else if (_result1 != null || (_result1 == null && _isCookedFirstResult))
                    {
                        if (_result2 == null && !_isCookedSecondResult)
                        {
                            //проверка на подъодит ли предмет
                            if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
                            {
                                AcceptObject(_heroik.GiveObjHands());
                                TurnOn();
                                StartCookingProcessAsync(_ingredient2);
                            }
                            else
                            {
                                Debug.LogWarning("продукт не подходит для сувида");
                            }
                        }
                        else if (_result2 != null || (_result2 == null && _isCookedSecondResult))
                        {
                            if (_result3 == null && !_isCookedThirdResult)
                            {
                                //проверка на подъодит ли предмет
                                if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
                                {
                                    AcceptObject(_heroik.GiveObjHands());
                                    TurnOn();
                                    StartCookingProcessAsync(_ingredient3);
                                }
                                else
                                {
                                    Debug.LogWarning("продукт не подходит для сувида");
                                }
                            }
                            else if (_result3 != null || (_result3 == null && _isCookedThirdResult))
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
                    // else // не работает
                    // {
                    //     if (_result1 != null)
                    //     {
                    //         if (_result2 != null)
                    //         {
                    //             if (_result3 != null)
                    //             {
                    //                 Debug.LogWarning("Сувид полный заберите готовый продукт");
                    //             }
                    //             else
                    //             {
                    //                 //проверка на подъодит ли предмет
                    //                 if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
                    //                 {
                    //                     ToAcceptObjsFood(_heroik.GiveObjHands(),3);
                    //                     TurnOn(thirdTimer, out _isCookedThirdResult);
                    //                     StartCookingProcessAsync(3);
                    //                 }
                    //                 else
                    //                 {
                    //                     Debug.LogWarning("продукт не подходит для сувида");
                    //                 }
                    //             }
                    //         }
                    //         else
                    //         {
                    //             //проверка на подъодит ли предмет
                    //             if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
                    //             {
                    //                 ToAcceptObjsFood(_heroik.GiveObjHands(),2);
                    //                 TurnOn(secondTimer, out _isCookedSecondResult);
                    //                 StartCookingProcessAsync(2);
                    //             }
                    //             else
                    //             {
                    //                 Debug.LogWarning("продукт не подходит для сувида");
                    //             }
                    //         }
                    //     }
                    //     else
                    //     {
                    //         //проверка на подъодит ли предмет
                    //         if (_heroik._curentTakenObjects.GetComponent<ObjsForSuvide>() && _heroik._curentTakenObjects.GetComponent<Interactable>())
                    //         {
                    //             ToAcceptObjsFood(_heroik.GiveObjHands(),1);
                    //             TurnOn(firstTimer, out _isCookedFirstResult);
                    //             StartCookingProcessAsync(1);
                    //         }
                    //         else
                    //         {
                    //             Debug.LogWarning("продукт не подходит для сувида");
                    //         }
                    //     }
                    // }
                }
                _timeCurrent = 0f;
            }
            else
            {
                Debug.LogWarning("Ждите перезарядки кнопки");
            }
        }
    }
    
    // private async void StartCookingProcessAsync(byte number)
    // {
    //     await Task.Delay(10000);
    //     CreateResult(number);
    //     TurnOff(number);
    // }
    
    private async void StartCookingProcessAsync(GameObject obj)
    {
        await Task.Delay(10000);
        CreateResult(obj);
        TurnOff();
    }
    
    // private void GiveResult(byte numberObj)
    // {
    //     if (numberObj == 1)
    //     {
    //         if (_result1.name == "firstBakedMeat")
    //         {
    //             _result1.name = "BakedMeat";
    //             _heroik.ActiveObjHands(_result1);
    //             _result1.SetActive(false);
    //             _result1 = null;
    //         }
    //         else if(_result1.name == "firstBakedFish")
    //         {
    //             _result1.name = "BakedFish";
    //             _heroik.ActiveObjHands(_result1);
    //             _result1.SetActive(false);
    //             _result1 = null;
    //         }
    //         else
    //         {
    //             Debug.Log("Ошибка в названии результата");
    //         }
    //
    //     }
    //     if (numberObj == 2)
    //     {
    //         if (_result2.name == "secondBakedMeat")
    //         {
    //             _result2.name = "BakedMeat";
    //             _heroik.ActiveObjHands(_result2);
    //             _result2.SetActive(false);
    //             _result2 = null;
    //         }
    //         else if(_result2.name == "secondBakedFish")
    //         {
    //             _result2.name = "BakedFish";
    //             _heroik.ActiveObjHands(_result2);
    //             _result2.SetActive(false);
    //             _result2 = null;
    //         }
    //         else
    //         {
    //             Debug.LogError("Ошибка в названии результата");
    //         }
    //     }
    //     if (numberObj == 3)
    //     {
    //         if (_result3.name == "thirdBakedMeat")
    //         {
    //             _result3.name = "BakedMeat";
    //             _heroik.ActiveObjHands(_result3);
    //             _result3.SetActive(false);
    //             _result3 = null;
    //         }
    //         else if(_result3.name == "thirdBakedFish")
    //         {
    //             _result3.name = "BakedFish";
    //             _heroik.ActiveObjHands(_result3);
    //             _result3.SetActive(false);
    //             _result3 = null;
    //         }
    //         else
    //         {
    //             Debug.LogError("Ошибка в названии результата");
    //         }
    //     }
    //
    // }

    public GameObject GiveObj(ref GameObject obj) // переделанный
    {
        obj.SetActive(false);
        GameObject Cobj = obj;
        Destroy(obj);
        obj = null;
        return Cobj;
    }
    // private void TurnOn(Timer_Prefab timer,out bool isCooked)
    // {
    //     _isWork = true;
    //     isCooked = true;
    //     //_animator.SetBool("Work", true);
    //     Instantiate(timer.timer, timer.timerPoint.position, Quaternion.identity,timer.timerParent);
    // }
    
    public void TurnOn() // переделанный 
    {
        if (_ingredient1 != null && !_isCookedFirstResult)
        {
            //_isWork = true;
            _isCookedFirstResult = true;
            //_animator.SetBool("Work", true);
            Instantiate(_firstTimer.timer, _firstTimer.timerPoint.position, Quaternion.identity,_firstTimer.timerParent);
            _readyResult1 = true;
        }
        else if (_ingredient2 != null && !_isCookedSecondResult)
        {
            _isWork = true;
            _isCookedSecondResult = true;
            //_animator.SetBool("Work", true);
            Instantiate(_secondTimer.timer, _secondTimer.timerPoint.position, Quaternion.identity,_secondTimer.timerParent);
            _readyResult2 = true;
        }
        else if (_ingredient3 != null && !_isCookedThirdResult)
        {
            _isWork = true;
            _isCookedThirdResult = true;
            //_animator.SetBool("Work", true);
            Instantiate(_thirdTimer.timer, _thirdTimer.timerPoint.position, Quaternion.identity,_thirdTimer.timerParent);
            _readyResult3 = true;
        }
        else
        {
            Debug.LogError("Ошибка в TurnOn");
        }
    }
    
    // private void TurnOff(byte numberObj)
    // {
    //     switch (numberObj)
    //     {
    //         case 1:
    //             _isWork = false;
    //             _isCookedFirstResult = false;
    //             _ingredient1.SetActive(false);
    //             _ingredient1 = null;
    //             //_animator.SetBool("Work", false);
    //         break;
    //         case 2:
    //             _isWork = false;
    //             _isCookedSecondResult = false;
    //             _ingredient2.SetActive(false);
    //             _ingredient2 = null;
    //             //_animator.SetBool("Work", false);
    //             break;
    //         case 3:
    //             _isWork = false;
    //             _isCookedThirdResult = false;
    //             _ingredient3.SetActive(false);
    //             _ingredient3 = null;
    //             //_animator.SetBool("Work", false);
    //             break;
    //         default: Debug.LogError("НЕПРАВИЛЬНЫЙ ИНДЕКС");  break;
    //     }
    //
    // }

    public void TurnOff() // переделанный
    {
        if (_readyResult1)
        {
            _isWork = false;
            _isCookedFirstResult = false;
            _cloneIngredient1.SetActive(false);
            Destroy(_cloneIngredient1);
            _ingredient1 = null;
            //_animator.SetBool("Work", false);
            _readyResult1 = false;
            
        }
        else if (_readyResult2)
        {
            _isWork = false;
            _isCookedSecondResult = false;
            _cloneIngredient2.SetActive(false);
            Destroy(_cloneIngredient2);
            _ingredient2 = null;
            //_animator.SetBool("Work", false);
            _readyResult2 = false;
        }
        else if (_readyResult3)
        {
            _isWork = false;
            _isCookedThirdResult = false;
            _cloneIngredient3.SetActive(false);
            Destroy(_cloneIngredient3);
            _ingredient3 = null;
            //_animator.SetBool("Work", false);
            _readyResult3 = false;
        }
        else
        {
            Debug.LogError("Ошибка в TurnOff");
        }
    }
    // private void ToAcceptObjsFood(GameObject acceptObjFood, byte numberObj)
    // {
    //     if (numberObj == 1)
    //     {
    //         foreach (var obj in food)
    //         {
    //             if (obj.name == "first" + acceptObjFood.name )
    //             {
    //                 obj.SetActive(true);
    //                 _ingredient1 = obj;
    //             }
    //         }
    //     }
    //     else if (numberObj == 2)
    //     {
    //         foreach (var obj in food)
    //         {
    //             if (obj.name == "second" + acceptObjFood.name)
    //             {
    //                 obj.SetActive(true);
    //                 _ingredient2 = obj;
    //             }
    //         }
    //     }
    //     else if (numberObj == 3)
    //     {
    //         foreach (var obj in food)
    //         {
    //             if (obj.name == "third" + acceptObjFood.name)
    //             {
    //                 obj.SetActive(true);
    //                 _ingredient3 = obj;
    //             }
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogError("Ошибка: неправильное название ингридиента или неправильно выбрана настройка");
    //     }
    // }

    public void AcceptObject(GameObject acceptObj) // переделанный
    {
        if (_ingredient1 == null && _result1 == null)
        {

            _ingredient1 = acceptObj;
            _cloneIngredient1 = Instantiate(_ingredient1, _suvidePoints.GetFirstPointIngredient().position, Quaternion.identity, _suvidePoints.GetFirstPointIngredient());
            _cloneIngredient1.transform.localPosition = Vector3.zero;
            _cloneIngredient1.transform.localRotation = Quaternion.identity;
            _cloneIngredient1.name = _cloneIngredient1.name.Replace("(Clone)", "");
            _cloneIngredient1.SetActive(true);
        }
        else if (_ingredient2 == null && _result2 == null)
        {
            _ingredient2 = acceptObj;
            _cloneIngredient2 = Instantiate(_ingredient2, _suvidePoints.GetSecondPointIngredient().position, Quaternion.identity, _suvidePoints.GetSecondPointIngredient());
            _cloneIngredient2.transform.localPosition = Vector3.zero;
            _cloneIngredient2.transform.localRotation = Quaternion.identity;
            _cloneIngredient2.name = _cloneIngredient2.name.Replace("(Clone)", "");
            _cloneIngredient2.SetActive(true);
        }
        else if (_ingredient3== null && _result3 == null)
        {
            _ingredient3 = acceptObj;
            _cloneIngredient3 = Instantiate(_ingredient3, _suvidePoints.GetThirdPointIngredient().position, Quaternion.identity, _suvidePoints.GetThirdPointIngredient());
            _cloneIngredient3.transform.localPosition = Vector3.zero;
            _cloneIngredient3.transform.localRotation = Quaternion.identity;
            _cloneIngredient3.name = _cloneIngredient3.name.Replace("(Clone)", "");
            _cloneIngredient3.SetActive(true);
        }
        else
        {
            Debug.Log("место под ингредиенты нет");
        }
    }
    
    // private void CreateResult(byte numberObj)
    // {
    //     string ingredientName = GetIngredientName(numberObj);
    //     string bakedName = GetBakedName(numberObj);
    //     
    //     if (string.IsNullOrEmpty(ingredientName) || string.IsNullOrEmpty(bakedName))
    //     {
    //         Debug.LogError($"Missing ingredient or baked name for number {numberObj}");
    //         return;
    //     }
    //
    //     GameObject result = FindResultObject(readyFood, bakedName);
    //
    //     if (result != null)
    //     {
    //         AssignResult(numberObj, result);
    //         result.SetActive(true);
    //     }
    //     else
    //     {
    //         Debug.LogWarning($"Could not find {bakedName} in readyFood list.");
    //     }
    // }

    public void CreateResult(GameObject obj)
    {
        try
        {
            if (_readyResult1)
            {
                _dictionaryRecipes.TryGetValue(obj.name, out ReadyFood readyObj);
                if (readyObj != null)
                {
                    _result1 = readyObj.gameObject;
                    _result1 = Instantiate(_result1, _suvidePoints.GetFirstPointResult().position , Quaternion.identity, _suvidePoints.GetFirstPointResult());
                    _result1.transform.localPosition = Vector3.zero;
                    _result1.transform.localRotation = Quaternion.identity;
                    _result1.name = _result1.name.Replace("(Clone)", "");
                    _result1.SetActive(true);
                    //_readyResult1 = false;
                }
                else
                {
                    Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                }
                
            }
            else if (_readyResult2)
            {
                _dictionaryRecipes.TryGetValue(obj.name, out ReadyFood readyObj);
                if (readyObj != null)
                {
                    _result2 = readyObj.gameObject;
                    _result2 = Instantiate(_result2, _suvidePoints.GetSecondPointResult().position , Quaternion.identity, _suvidePoints.GetSecondPointResult());
                    _result2.transform.localPosition = Vector3.zero;
                    _result2.transform.localRotation = Quaternion.identity;
                    _result2.name = _result2.name.Replace("(Clone)", "");
                    _result2.SetActive(true);
                    //_readyResult2 = false;
                }
                else
                {
                    Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                }

            }
            else if (_readyResult3)
            {
                _dictionaryRecipes.TryGetValue(obj.name, out ReadyFood readyObj);
                if (readyObj != null)
                {
                    _result3 = readyObj.gameObject;
                    _result3 = Instantiate(_result3, _suvidePoints.GetThirdPointResult().position , Quaternion.identity, _suvidePoints.GetThirdPointResult());
                    _result3.transform.localPosition = Vector3.zero;
                    _result3.transform.localRotation = Quaternion.identity;
                    _result3.name = _result3.name.Replace("(Clone)", "");
                    _result3.SetActive(true);
                    //_readyResult3 = false;
                }
                else
                {
                    Debug.LogError("Ошибка в CreateResult, такого ключа нет");
                }
                
            }
            else
            {
                Debug.LogError("Ошибка в CreateResult / переменная _readyResult");
            }
        }
        catch (Exception e)
        {
            Debug.Log("ошибка приготовления в сувиде" + e);
        }
    }
    private string GetIngredientName(byte numberObj) 
    {
        switch (numberObj)
        {
            case 1: return _ingredient1.name;
            case 2: return _ingredient2.name;
            case 3: return _ingredient3.name;
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
            case 1: ingredientName = _ingredient1.name; break;
            case 2: ingredientName = _ingredient2.name; break;
            case 3: ingredientName = _ingredient3.name; break;
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
                _result1 = result;
                break;
            case 2:
                _result2 = result;
                break;
            case 3:
                _result3 = result;
                break;
            default:
                Debug.LogError($"Invalid numberObj: {numberObj}");
                break;
        }
    }

    private void WorkingSuvide()
    {
        _waterPrefab.SetActive(true);
        _switchTemperPrefab.transform.rotation = Quaternion.Euler(45, -90, -90);
        _switchTimePrefab.transform.rotation = Quaternion.Euler(175, -90, -90);
    }
    private void NotWorkingSuvide()
    {
        _waterPrefab.SetActive(false);
        _switchTemperPrefab.transform.rotation = Quaternion.Euler(90, -90, -90);
        _switchTimePrefab.transform.rotation = Quaternion.Euler(90, -90, -90);
    }
    
    public bool IsAllowDestroy()
    {
        if (_ingredient1 == null && _ingredient2 == null && _ingredient3 == null && _result1 == null &&
            _result2 == null && _result3 == null && !_isWork)
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


