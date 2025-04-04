using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class Blender : IDisposable, IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn,IIsAllowDestroy,IHeroikIsTrigger,IFindReadyFood
{

     private Heroik _heroik = null; // только для объекта героя, а надо и другие...
     private BlenderPoints _blenderPoints;
     private BlenderView _blenderView;
     private ProductsContainer _productsContainer;
    
    private GameObject _ingredient1 = null;
    private GameObject _ingredient2 = null;
    private GameObject _ingredient3 = null;
    private GameObject _result = null;
    private bool _isWork = false;
    private bool _isHeroikTrigger = false;
    
    public Blender(Heroik heroik, BlenderPoints blenderPoints, BlenderView blenderView, ProductsContainer productsContainer)
    {
        _heroik = heroik;
        _blenderPoints = blenderPoints;
        _blenderView = blenderView;
        _productsContainer = productsContainer;
        
        EventBus.PressE += CookingProcess;
        Debug.Log("Создал объект: Blender");
    }
    
    public void Dispose()
    {
        EventBus.PressE -= CookingProcess;
        Debug.Log("У объекта вызван Dispose : Blender");
    }
    
    public GameObject GiveObj(ref GameObject giveObj) 
    {
        GameObject giveObjCopy = Object.Instantiate(giveObj);
        giveObjCopy.SetActive(false);
        giveObjCopy.name = giveObjCopy.name.Replace("(Clone)", "");
        DeleteObj(giveObj);
        return giveObjCopy;
    }

    public void AcceptObject(GameObject acceptObj)
    {
        if (_ingredient1 == null)
        {
            _ingredient1 = acceptObj;
            _ingredient1 = Object.Instantiate(_ingredient1, _blenderPoints.FirstPoint.transform.position, Quaternion.identity, _blenderPoints.ParentFood);
            _ingredient1.name = _ingredient1.name.Replace("(Clone)", "");
            _ingredient1.SetActive(true);
        }
        else if (_ingredient2 == null)
        {
            _ingredient2 = acceptObj;
            _ingredient2 = Object.Instantiate(_ingredient2, _blenderPoints.SecondPoint.transform.position, Quaternion.identity, _blenderPoints.ParentFood);
            _ingredient2.name = _ingredient2.name.Replace("(Clone)", "");
            _ingredient2.SetActive(true);
        }
        else if (_ingredient3 == null)
        {
            _ingredient3 = acceptObj;
            _ingredient3 = Object.Instantiate(_ingredient3, _blenderPoints.ThirdPoint.transform.position, Quaternion.identity, _blenderPoints.ParentFood);
            _ingredient3.name = _ingredient3.name.Replace("(Clone)", "");
            _ingredient3.SetActive(true);
        }
        else
        {
            Debug.LogWarning("В блендере нет места");
        }
        Object.Destroy(acceptObj);
    }
    
    public void CreateResult(GameObject obj)
    {
        _result = Object.Instantiate(obj, _blenderPoints.SecondPoint.transform.position, Quaternion.identity, _blenderPoints.ParentReadyFood);
        _result.name = _result.name.Replace("(Clone)", "");
        _result.SetActive(true);
    }

    public void TurnOn()
    {
        _ingredient1.SetActive(false);
        _ingredient2.SetActive(false);
        _ingredient3.SetActive(false);
        _isWork = true;
        _blenderView.TurnOn();
    }

    public void TurnOff()
    {
        _isWork = false;
        Object.Destroy(_ingredient1);
        Object.Destroy(_ingredient2);
        Object.Destroy(_ingredient3);
        _ingredient1 = null;
        _ingredient2 = null;
        _ingredient3 = null;
        _blenderView.TurnOff();
    }
    
    public bool IsAllowDestroy()
    {
        if (_ingredient1 == null && _ingredient2 == null && _ingredient3 == null && _result == null && !_isWork)
        {
            return true;
        }
        return false;
    }

    public void HeroikIsTrigger()
    {
        _isHeroikTrigger = !_isHeroikTrigger;
    }

    public GameObject FindReadyFood()
    {
        List<GameObject> currentFruits = new List<GameObject>(){_ingredient1,_ingredient2,_ingredient3};
        if (SuitableIngredients(currentFruits,_productsContainer.RequiredFreshnessCocktail))
        {
            return _productsContainer.FreshnessCocktail;
        }
        if(SuitableIngredients(currentFruits,_productsContainer.RequiredWildBerryCocktail))
        {
            return _productsContainer.WildBerryCocktail;
        }
        else
        {
            return _productsContainer.Rubbish;
        }
    }

    public bool SuitableIngredients(List<GameObject> currentFruits, List<GameObject> requiredFruits)
    {
        List<string> requiredFruitsNames = new List<string>();
        List<string> currentFruitNames = new List<string>();
        foreach (var fruit in currentFruits)
        {
            currentFruitNames.Add(fruit.name); // Используем имя объекта
        }
        foreach (var fruit in requiredFruits)
        {
            requiredFruitsNames.Add(fruit.name); // Используем имя объекта
        }
        foreach (string fruit in requiredFruitsNames)
        {
            if (!currentFruitNames.Contains(fruit))
            {
                return false;
            }
        }
        return true;
    }
    
    private async void StartCookingProcessAsync(GameObject obj)
    {
        await Task.Delay(4000);
        TurnOff();
        CreateResult(obj);
    }
    private void CookingProcess()
    {
        if (_isHeroikTrigger == true)
        {
            if(_heroik.IsBusyHands == false) // руки не заняты
            {
                if (_isWork)
                {
                    Debug.Log("ждите блендер готовится");
                }
                else
                {
                    if (_result == null)
                    {
                        if (_ingredient1 == null)
                        {
                            Debug.Log("Руки пусты ингредиентов нет");
                        }
                        else
                        {
                            if (_ingredient2 == null)
                            {
                                _heroik.ActiveObjHands(GiveObj(ref _ingredient1));
                                _ingredient1 = null;
                            }
                            else
                            {
                                _heroik.ActiveObjHands(GiveObj(ref _ingredient2));
                                _ingredient2 = null;
                            }
                        }
                    }
                    else
                    {
                        _heroik.ActiveObjHands(GiveObj(ref _result));
                    }
                        
                }
                    
            }
            else // руки заняты
            {
                if (_isWork)
                {
                    Debug.Log("ждите блендер готовится");
                }
                else
                {
                    if (_result == null)
                    {
                        if (_ingredient1 == null)
                        {
                            if(_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForBlender),typeof(Fruit)}))
                            {
                                AcceptObject(_heroik.GiveObjHands());
                                Debug.Log("Предмет первый положен в блендер");
                            }
                            else
                            {
                                Debug.Log("с предметом нельзя взаимодействовать");
                            }
                        }
                        else
                        {
                            if (_ingredient2 == null)
                            {
                                if(_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForBlender),typeof(Fruit)}))
                                {
                                    AcceptObject(_heroik.GiveObjHands());
                                    Debug.Log("Предмет второй положен в блендер");
                                }
                                else
                                {
                                    Debug.Log("с предметом нельзя взаимодействовать");
                                }
                            }
                            else
                            {
                                if(_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForBlender),typeof(Fruit)}))
                                {
                                    AcceptObject(_heroik.GiveObjHands());
                                    Debug.Log("Предмет третий положен в блендер");
                                    TurnOn(); 
                                    GameObject objdish = FindReadyFood();
                                    StartCookingProcessAsync(objdish);
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
    
    private void DeleteObj(GameObject obj)
    {
        obj.SetActive(false);
        Object.Destroy(obj);
        obj = null;
    }
    
}
