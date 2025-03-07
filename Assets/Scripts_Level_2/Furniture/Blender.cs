using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Blender : MonoBehaviour, IGiveObj, IAcceptObject, ICreateResult, ITurnOffOn,IIsAllowDestroy,IHeroikIsTrigger,IFindReadyFood
{
     private GameObject _timer;
     private Transform _timerPoint;
     private Transform _timerParent;
     private GameObject[] objectOnTheTable;
     private GameObject[] readyFoods;
    
     private Animator _animator;
     private Heroik _heroik = null; // только для объекта героя, а надо и другие...
     private BlenderPoints _blenderPoints;
     private BlenderRecipes _blenderRecipes;
    
    [SerializeField] private GameObject _ingredient1 = null;
    [SerializeField] private GameObject _ingredient2 = null;
    [SerializeField] private GameObject _ingredient3 = null;
    [SerializeField] private GameObject _result = null;
    private bool _isWork = false;
    private float _timeCurrent = 0.17f;
    private bool _heroikIsTrigger = false;
    private Transform _parentFood;

    // для удаления визуала объекта над блендером
    [SerializeField]private GameObject _cloneIngridient1; 
    [SerializeField]private GameObject _cloneIngridient2;
    [SerializeField]private GameObject _cloneIngridient3;

    public void Initialize(GameObject _timer,Heroik _heroik, Transform _timerPoint,Transform _timerParent,
        GameObject[] objectOnTheTable,GameObject[] readyFoods,Animator _animator,BlenderPoints _blenderPoints,
        Transform _parentFood,BlenderRecipes _blenderRecipes)
    {
        this._timer = _timer;
        this._heroik = _heroik;
        this._timerPoint = _timerPoint;
        this._timerParent = _timerParent;
        this.objectOnTheTable = objectOnTheTable;
        this.readyFoods = readyFoods;
        this._animator = _animator;
        this._blenderPoints = _blenderPoints;
        this._parentFood = _parentFood;
        this._blenderRecipes = _blenderRecipes;
    }
    private void Update()
    {
        _timeCurrent += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && _heroikIsTrigger)
        {
            if (_timeCurrent >= 0.17f)
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
                            if (_ingredient1 == null)
                            {
                                Debug.Log("Руки пусты ингридиентов нет");
                            }
                            else
                            {
                                if (_ingredient2 == null)
                                {
                                    _heroik.ActiveObjHands(GiveObj(ref _cloneIngridient1));
                                    _ingredient1 = null;
                                }
                                else
                                {
                                    _heroik.ActiveObjHands(GiveObj(ref _cloneIngridient2));
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
                        Debug.Log("ждите блэндер готовится");
                    }
                    else
                    {
                        if (_result == null)
                        {
                            if (_ingredient1 == null)
                            {
                                if(_heroik._curentTakenObjects.GetComponent<Interactable>() && _heroik._curentTakenObjects.GetComponent<Fruit>())
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
                                    if(_heroik._curentTakenObjects.GetComponent<Interactable>() && _heroik._curentTakenObjects.GetComponent<Fruit>())
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
                                    if(_heroik._curentTakenObjects.GetComponent<Interactable>() && _heroik._curentTakenObjects.GetComponent<Fruit>())
                                    {
                                        AcceptObject(_heroik.GiveObjHands());
                                        Debug.Log("Предмет третий положен в блендер");
                                        TurnOn(); 
                                        GameObject objdish = FindReadyFood();
                                        StartCookingProcessAsync(objdish);
                                        //yield return new WaitForSeconds(4f);
                                        //TurnOff();
                                        //CreateResult(objdish.name);
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
                _timeCurrent = 0f;
            }
            else
            {
                Debug.LogWarning("Ждите перезарядки кнопки");
            }
        }
    }

    private async void StartCookingProcessAsync(GameObject obj)
    {
        await Task.Delay(4000);
        TurnOff();
        CreateResult(obj);
    }
    
    public GameObject GiveObj(ref GameObject obj) 
    {
        obj.SetActive(false);
        var cObj = obj;
        Destroy(obj);
        return cObj;
    }

    public void AcceptObject(GameObject acceptObj)
    {
        if (_ingredient1 == null)
        {
            _ingredient1 = acceptObj;
            _cloneIngridient1 = Instantiate(_ingredient1, _blenderPoints.GetFirstPoint(), Quaternion.identity, _parentFood);
            _cloneIngridient1.name = _cloneIngridient1.name.Replace("(Clone)", "");
            _cloneIngridient1.SetActive(true);
        }
        else if (_ingredient2 == null)
        {
            _ingredient2 = acceptObj;
            _cloneIngridient2 = Instantiate(_ingredient2, _blenderPoints.GetSecondPoint(), Quaternion.identity, _parentFood);
            _cloneIngridient2.name = _cloneIngridient2.name.Replace("(Clone)", "");
            _cloneIngridient2.SetActive(true);
        }
        else if (_ingredient3 == null)
        {
            _ingredient3 = acceptObj;
            _cloneIngridient3 = Instantiate(_ingredient3, _blenderPoints.GetThirdPoint(), Quaternion.identity, _parentFood);
            _cloneIngridient3.name = _cloneIngridient3.name.Replace("(Clone)", "");
            _cloneIngridient3.SetActive(true);
        }
        else
        {
            Debug.LogWarning("В блендере нет места");
        }
    }
    
    public void CreateResult(GameObject obj)
    {
        obj.SetActive(true);
        _result = obj;
    }

    public void TurnOn()
    {
        _isWork = true;
        Destroy(_cloneIngridient1);
        Destroy(_cloneIngridient2);
        Destroy(_cloneIngridient3);
        _animator.SetBool("Work", true);
        Instantiate(_timer, _timerPoint.position, Quaternion.identity,_timerParent);
    }

    public void TurnOff()
    {
        _isWork = false;
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
        _heroikIsTrigger = !_heroikIsTrigger;
    }

    public GameObject FindReadyFood()
    {
        List<GameObject> currentFruits = new List<GameObject>(){_ingredient1,_ingredient2,_ingredient3};
        if (SuitableIngredients(currentFruits,_blenderRecipes.GetRequiredFreshnessCocktail()))
        {
            return _blenderRecipes.GetFreshnessCocktail();
        }
        if(SuitableIngredients(currentFruits,_blenderRecipes.GetRequiredWildBerryCocktail()))
        {
            return _blenderRecipes.GetWildBerryCocktail();
        }
        else
        {
            return _blenderRecipes.GetRubbish();
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
    
}
