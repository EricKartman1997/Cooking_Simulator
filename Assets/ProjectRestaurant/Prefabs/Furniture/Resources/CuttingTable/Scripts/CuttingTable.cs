using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class CuttingTable : IDisposable,IGiveObj,IAcceptObject,ICreateResult,ITurnOffOn,IIsAllowDestroy,IHeroikIsTrigger,IFindReadyFood
{
    // Initialize поля
    private CuttingTablePoints _cuttingTablePoints;
    private CuttingTableView _cuttingTableView;
    private ProductsContainer _productsContainer;
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    
    // не Initialize поля
    private bool _isWork = false;
    private bool _isHeroikTrigger = false;
    private GameObject _ingredient1 = null;
    private GameObject _ingredient2 = null;
    private GameObject _result = null;

    public CuttingTable(CuttingTablePoints cuttingTablePoints, CuttingTableView cuttingTableView, ProductsContainer productsContainer, Heroik heroik)
    {
        _cuttingTablePoints = cuttingTablePoints;
        _cuttingTableView = cuttingTableView;
        _productsContainer = productsContainer;
        _heroik = heroik;
        
        EventBus.PressE += CookingProcess;
        Debug.Log("Создать объект: CuttingTable");
    }

    public void Dispose()
    {
        EventBus.PressE -= CookingProcess;
        Debug.Log("У объекта вызван Dispose : CuttingTable");
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
            _ingredient1 = Object.Instantiate(_ingredient1, _cuttingTablePoints.PositionIngredient1.position, Quaternion.identity, _cuttingTablePoints.ParentIngredient);
            _ingredient1.name = _ingredient1.name.Replace("(Clone)", "");
            _ingredient1.SetActive(true);
        }
        else if (_ingredient2 == null)
        {
            _ingredient2 = acceptObj;
            _ingredient2 = Object.Instantiate(_ingredient2, _cuttingTablePoints.PositionIngredient2.position, Quaternion.identity, _cuttingTablePoints.ParentIngredient);
            _ingredient2.name = _ingredient2.name.Replace("(Clone)", "");
            _ingredient2.SetActive(true);
        }
        else
        {
            Debug.LogWarning("На нарезочном столе нет места");
        }
        Object.Destroy(acceptObj);
    }
    
    public void CreateResult(GameObject obj)
    {
        _result = obj;
        _result = Object.Instantiate(_result, _cuttingTablePoints.PositionResult.position, Quaternion.identity, _cuttingTablePoints.ParentResult);
        _result.name = _result.name.Replace("(Clone)", "");
        _result.SetActive(true);
    }
    
    public void TurnOn()
    {
        _isWork = true;
        _ingredient1.SetActive(false);
        _ingredient2.SetActive(false);
        _cuttingTableView.TurnOn();
    }
    
    public void TurnOff()
    {
        _isWork = false;
        Object.Destroy(_ingredient1);
        Object.Destroy(_ingredient2);
        _ingredient1 = null;
        _ingredient2 = null;
        _cuttingTableView.TurnOff();
    }

    public bool IsAllowDestroy()
    {
        if (_ingredient1 == null && _ingredient2 == null && _result == null && !_isWork)
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
        List<GameObject> currentIngredient = new List<GameObject>(){_ingredient1,_ingredient2};
        if (SuitableIngredients(currentIngredient,_productsContainer.RequiredFruitSalad))
        {
            return _productsContainer.FruitSalad;
        }
        if(SuitableIngredients(currentIngredient,_productsContainer.RequiredMixBakedFruit))
        {
            return _productsContainer.MixBakedFruit;
        }
        else
        {
            return _productsContainer.Rubbish;
        }
    }

    public bool SuitableIngredients(List<GameObject> currentIngredients, List<GameObject> requiredFruits)
    {
        List<string> requiredFruitsNames = new List<string>();
        List<string> currentIngredientsNames = new List<string>();
        foreach (var ingredient in currentIngredients)
        {
            currentIngredientsNames.Add(ingredient.name); // Используем имя объекта
        }
        foreach (var ingredient in requiredFruits)
        {
            requiredFruitsNames.Add(ingredient.name); // Используем имя объекта
        }
        foreach (string ingredient in requiredFruitsNames)
        {
            if (!currentIngredientsNames.Contains(ingredient))
            {
                return false;
            }
        }
        return true;
    }
    
    private void CookingProcess()
    {
        if(_isHeroikTrigger == true )
        {
            if(_heroik.IsBusyHands == false) // руки не заняты
            {
                if (_isWork)
                {
                    Debug.Log("ждите блюдо готовится");
                }
                else
                {
                    if (_result == null)
                    {
                        if (_ingredient1 == null)
                        {
                            Debug.Log("У вас пустые руки и прилавок пуст");
                        }
                        else //есть первый ингредиент // забираете первый ингредиент 
                        {
                            _heroik.ActiveObjHands(GiveObj(ref _ingredient1));
                            _ingredient1 = null;
                        }
                    }
                    else //есть результат // забрать результат
                    {
                        _heroik.ActiveObjHands(GiveObj(ref _result));
                        _result = null;
                        Debug.Log("Вы забрали конечный продукт"); 
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
                        if (_ingredient1 == null)// ингредиентов нет
                        {
                            if(_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForCutting)}))
                            {
                                AcceptObject(_heroik.GiveObjHands());
                            }
                            else
                            {
                                Debug.Log("с предметом нельзя взаимодействовать");
                            }
                        }
                        else// есть первый ингредиент
                        {
                            if (_heroik.CheckObjForReturn(new List<Type>(){typeof(ObjsForCutting)}))
                            {
                                AcceptObject(_heroik.GiveObjHands());
                                //Debug.Log("Предмет второй положен на нарезочный стол");
                                TurnOn(); 
                                GameObject objdish = FindReadyFood();
                                StartCookingProcessAsync(objdish);
                            }
                            else
                            {
                                Debug.Log("Объект не подходит для слияния");
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Сначала уберите предмет из рук");
                    }
                        
                }
                    
            }
                
        }
    }
    
    private async void StartCookingProcessAsync(GameObject obj)
    {
        await Task.Delay(3000);
        TurnOff();
        CreateResult(obj);
    }
    
    private void DeleteObj(GameObject obj)
    {
        obj.SetActive(false);
        Object.Destroy(obj);
        obj = null;
    }
}
