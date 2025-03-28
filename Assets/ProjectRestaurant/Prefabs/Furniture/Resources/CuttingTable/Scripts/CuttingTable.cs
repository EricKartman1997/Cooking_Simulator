using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CuttingTable : MonoBehaviour,IGiveObj,IAcceptObject,ICreateResult,ITurnOffOn,IIsAllowDestroy,IHeroikIsTrigger,IFindReadyFood
{
    // Initialize поля
    private Animator _animator;
    private GameObject _timer;
    private Transform _timerPoint;
    private Transform _timerParent;
    private ObjectsAndRecipes _objectsAndRecipes;
    private Transform _positionIngredient1; // сделать отдельный класс
    private Transform _positionIngredient2; // сделать отдельный класс
    private Transform _parentIngredient;    // сделать отдельный класс
    private Transform _positionResult;      // сделать отдельный класс
    private Transform _parentResult;        // сделать отдельный класс
    private Heroik _heroik = null; // только для объекта героя, а надо и другие...
    
    // не Initialize поля
    private bool _isWork = false;
    private bool _heroikIsTrigger = false;
    private GameObject _ingredient1 = null;
    private GameObject _ingredient2 = null;
    private GameObject _result = null;
    
    public void Initialize(Animator animator,Heroik heroik,GameObject timer,Transform timerPoint,Transform timerParent,Transform positionIngredient1,Transform positionIngredient2,Transform parentIngredient,Transform positionResult,Transform parentResult,ObjectsAndRecipes objectsAndRecipes)
    {
        _animator = animator;
        _heroik = heroik;
        _timer = timer;
        _timerPoint = timerPoint;
        _timerParent = timerParent;
        _positionIngredient1 = positionIngredient1;
        _positionIngredient2 = positionIngredient2;
        _parentIngredient = parentIngredient;
        _positionResult = positionResult;
        _parentResult = parentResult;
        _objectsAndRecipes = objectsAndRecipes;
    }
    
    private void OnEnable()
    {
        EventBus.PressE += CookingProcess;
    }

    private void OnDisable()
    {
        EventBus.PressE -= CookingProcess;
    }
    
    public GameObject GiveObj(ref GameObject obj)
    {
        obj.SetActive(false);
        GameObject Cobj = obj;
        Destroy(obj);
        return Cobj;
    }
    
    public void AcceptObject(GameObject acceptObj)
    {
        if (_ingredient1 == null)
        {
            _ingredient1 = acceptObj;
            _ingredient1 = Instantiate(_ingredient1, _positionIngredient1.position, Quaternion.identity, _parentIngredient);
            _ingredient1.name = _ingredient1.name.Replace("(Clone)", "");
            _ingredient1.SetActive(true);
        }
        else if (_ingredient2 == null)
        {
            _ingredient2 = acceptObj;
            _ingredient2 = Instantiate(_ingredient2, _positionIngredient2.position, Quaternion.identity, _parentIngredient);
            _ingredient2.name = _ingredient2.name.Replace("(Clone)", "");
            _ingredient2.SetActive(true);
        }
        else
        {
            Debug.LogWarning("На нарезочном столе нет места");
        }
    }
    
    public void CreateResult(GameObject obj)
    {
        _result = obj;
        _result = Instantiate(_result, _positionResult.position, Quaternion.identity, _parentResult);
        _result.name = _result.name.Replace("(Clone)", "");
        _result.SetActive(true);
    }
    
    public void TurnOn()
    {
        _isWork = true;
        _ingredient1.SetActive(false);
        _ingredient2.SetActive(false);
        Destroy(_ingredient1);
        Destroy(_ingredient2);
        _animator.SetBool("Work", true);
        Instantiate(_timer, _timerPoint.position, Quaternion.identity,_timerParent);
    }
    
    public void TurnOff()
    {
        _isWork = false;
        _ingredient1 = null;
        _ingredient2 = null;
        _animator.SetBool("Work", false);
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
        _heroikIsTrigger = !_heroikIsTrigger;
    }

    public GameObject FindReadyFood()
    {
        List<GameObject> currentIngredient = new List<GameObject>(){_ingredient1,_ingredient2};
        if (SuitableIngredients(currentIngredient,_objectsAndRecipes.RequiredFruitSalad))
        {
            return _objectsAndRecipes.FruitSalad;
        }
        if(SuitableIngredients(currentIngredient,_objectsAndRecipes.RequiredMixBakedFruit))
        {
            return _objectsAndRecipes.MixBakedFruit;
        }
        else
        {
            return _objectsAndRecipes.Rubbish;
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
        if(_heroikIsTrigger == true )
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
                                Debug.Log("Предмет второй положен на нарезочный стол");
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
    
}
