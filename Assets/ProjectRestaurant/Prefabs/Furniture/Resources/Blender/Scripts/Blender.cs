using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Blender : MonoBehaviour, IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn,IIsAllowDestroy,IHeroikIsTrigger,IFindReadyFood
{
     private GameObject _timer;
     private Transform _timerPoint;
     private Transform _timerParent;
     
     private Animator _animator;
     private Heroik _heroik = null; // только для объекта героя, а надо и другие...
     private BlenderPoints _blenderPoints;
     private ProductsContainer _productsContainer;
    
    [SerializeField] private GameObject _ingredient1 = null;
    [SerializeField] private GameObject _ingredient2 = null;
    [SerializeField] private GameObject _ingredient3 = null;
    [SerializeField] private GameObject _result = null;
    private bool _isWork = false;
    private bool _isHeroikTrigger = false;
    
    
    public void Initialize(GameObject timer,Heroik heroik, Transform timerPoint,Transform timerParent,
        Animator animator,BlenderPoints blenderPoints, ProductsContainer productsContainer)
    {
        _timer = timer;
        _heroik = heroik;
        _timerPoint = timerPoint;
        _timerParent = timerParent;
        _animator = animator;
        _blenderPoints = blenderPoints;
        _productsContainer = productsContainer;
    }
    
    private void OnEnable()
    {
        EventBus.PressE += CookingProcess;
    }

    private void OnDisable()
    {
        EventBus.PressE -= CookingProcess;
    }
    
    public GameObject GiveObj(ref GameObject giveObj) 
    {
        GameObject giveObjCopy = Instantiate(giveObj);
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
            _ingredient1 = Instantiate(_ingredient1, _blenderPoints.FirstPoint.transform.position, Quaternion.identity, _blenderPoints.ParentFood);
            _ingredient1.name = _ingredient1.name.Replace("(Clone)", "");
            _ingredient1.SetActive(true);
        }
        else if (_ingredient2 == null)
        {
            _ingredient2 = acceptObj;
            _ingredient2 = Instantiate(_ingredient2, _blenderPoints.SecondPoint.transform.position, Quaternion.identity, _blenderPoints.ParentFood);
            _ingredient2.name = _ingredient2.name.Replace("(Clone)", "");
            _ingredient2.SetActive(true);
        }
        else if (_ingredient3 == null)
        {
            _ingredient3 = acceptObj;
            _ingredient3 = Instantiate(_ingredient3, _blenderPoints.ThirdPoint.transform.position, Quaternion.identity, _blenderPoints.ParentFood);
            _ingredient3.name = _ingredient3.name.Replace("(Clone)", "");
            _ingredient3.SetActive(true);
        }
        else
        {
            Debug.LogWarning("В блендере нет места");
        }
        Destroy(acceptObj);
    }
    
    public void CreateResult(GameObject obj)
    {
        _result = Instantiate(obj, _blenderPoints.SecondPoint.transform.position, Quaternion.identity, _blenderPoints.ParentReadyFood);
        _result.name = _result.name.Replace("(Clone)", "");
        _result.SetActive(true);
    }

    public void TurnOn()
    {
        _ingredient1.SetActive(false);
        _ingredient2.SetActive(false);
        _ingredient3.SetActive(false);
        _isWork = true;
        _animator.SetBool("Work", true);
        Instantiate(_timer, _timerPoint.position, Quaternion.identity,_timerParent);
    }

    public void TurnOff()
    {
        _isWork = false;
        Destroy(_ingredient1);
        Destroy(_ingredient2);
        Destroy(_ingredient3);
        _ingredient1 = null;
        _ingredient2 = null;
        _ingredient3 = null;
        _animator.SetBool("Work", false);
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
        Destroy(obj);
        obj = null;
    }
}
